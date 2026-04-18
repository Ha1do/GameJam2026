using UnityEngine;
using UnityEngine.AI;

public class Jort_brain : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Chase,
        Search
    }

    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform eyesPoint;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    [Header("Detection")]
    [SerializeField] private float alertRadius = 6f;
    [SerializeField] private bool alertRequiresLineOfSight = true;
    [SerializeField] private float viewDistance = 12f;
    [SerializeField] [Range(0f, 360f)] private float viewAngle = 90f;
    [SerializeField] private LayerMask wallMask;

    [Header("Search")]
    [SerializeField] private float searchDuration = 8f;
    [SerializeField] private float searchRadius = 6f;
    [SerializeField] private int maxSearchPointAttempts = 10;
    [SerializeField] private float searchPointReachDistance = 1.2f;
    [SerializeField] private float waitAtSearchPointTime = 1f;

    [Header("Movement")]
    [SerializeField] private float stoppingDistance = 1.5f;

    private EnemyState currentState = EnemyState.Idle;

    private Vector3 lastKnownPlayerPosition;
    private Vector3 currentSearchPoint;

    private bool reachedLastKnownPosition = false;
    private bool hasSearchPoint = false;

    private float searchTimer = 0f;
    private float waitTimer = 0f;
    private float nextTeleportAllowedTime = 0f;

    public bool IsTargetingPlayer => currentState == EnemyState.Chase || currentState == EnemyState.Search;
    public bool CanUseTeleport => Time.time >= nextTeleportAllowedTime;

    private void Reset()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();

        if (eyesPoint == null)
            eyesPoint = transform;

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        if (agent != null)
            agent.stoppingDistance = stoppingDistance;
    }

    private void Update()
    {
        if (player == null || agent == null || !agent.enabled || !agent.isOnNavMesh)
            return;

        bool canSeePlayer = CanSeePlayer();
        bool canDetectPlayerInAlertRadius = CanDetectPlayerInAlertRadius();

        switch (currentState)
        {
            case EnemyState.Idle:
                if (canSeePlayer || canDetectPlayerInAlertRadius)
                {
                    EnterChase();
                }
                else
                {
                    UpdateIdle();
                }
                break;

            case EnemyState.Chase:
                UpdateChase(canSeePlayer, canDetectPlayerInAlertRadius);
                break;

            case EnemyState.Search:
                UpdateSearch(canSeePlayer, canDetectPlayerInAlertRadius);
                break;
        }

        UpdateAnimation();
    }

    private void UpdateIdle()
    {
        if (!agent.isStopped)
            agent.isStopped = true;

        if (agent.hasPath)
            agent.ResetPath();
    }

    private void EnterChase()
    {
        currentState = EnemyState.Chase;
        agent.isStopped = false;
        agent.stoppingDistance = stoppingDistance;

        lastKnownPlayerPosition = player.position;

        reachedLastKnownPosition = false;
        hasSearchPoint = false;
        waitTimer = 0f;
        searchTimer = searchDuration;
    }

    private void UpdateChase(bool canSeePlayer, bool canDetectPlayerInAlertRadius)
    {
        if (canSeePlayer || canDetectPlayerInAlertRadius)
        {
            lastKnownPlayerPosition = player.position;
            agent.isStopped = false;
            agent.stoppingDistance = stoppingDistance;
            agent.SetDestination(player.position);
        }
        else
        {
            StartSearch();
        }
    }

    private void StartSearch()
    {
        currentState = EnemyState.Search;
        searchTimer = searchDuration;
        waitTimer = 0f;
        reachedLastKnownPosition = false;
        hasSearchPoint = false;

        agent.isStopped = false;
        agent.stoppingDistance = 0f;
        agent.SetDestination(lastKnownPlayerPosition);
    }

    private void UpdateSearch(bool canSeePlayer, bool canDetectPlayerInAlertRadius)
    {
        if (canSeePlayer || canDetectPlayerInAlertRadius)
        {
            EnterChase();
            return;
        }

        searchTimer -= Time.deltaTime;

        if (searchTimer <= 0f)
        {
            EnterIdle();
            return;
        }

        agent.isStopped = false;
        agent.stoppingDistance = 0f;

        if (!reachedLastKnownPosition)
        {
            agent.SetDestination(lastKnownPlayerPosition);

            if (HasReachedPoint(lastKnownPlayerPosition))
            {
                reachedLastKnownPosition = true;
                waitTimer = waitAtSearchPointTime;
                agent.ResetPath();
            }

            return;
        }

        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        if (!hasSearchPoint)
        {
            if (TryGetSearchPoint(out currentSearchPoint))
            {
                hasSearchPoint = true;
                agent.SetDestination(currentSearchPoint);
            }
            else
            {
                waitTimer = waitAtSearchPointTime;
            }

            return;
        }

        if (HasReachedPoint(currentSearchPoint))
        {
            hasSearchPoint = false;
            waitTimer = waitAtSearchPointTime;
            agent.ResetPath();
        }
    }

    private void EnterIdle()
    {
        currentState = EnemyState.Idle;
        reachedLastKnownPosition = false;
        hasSearchPoint = false;
        waitTimer = 0f;

        agent.isStopped = true;
        agent.ResetPath();
    }

    private bool TryGetSearchPoint(out Vector3 result)
    {
        for (int i = 0; i < maxSearchPointAttempts; i++)
        {
            Vector2 random2D = Random.insideUnitCircle * searchRadius;
            Vector3 wantedPoint = lastKnownPlayerPosition + new Vector3(random2D.x, 0f, random2D.y);

            if (!NavMesh.SamplePosition(wantedPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                continue;

            NavMeshPath path = new NavMeshPath();

            if (!agent.CalculatePath(hit.position, path))
                continue;

            if (path.status != NavMeshPathStatus.PathComplete)
                continue;

            result = hit.position;
            return true;
        }

        result = transform.position;
        return false;
    }

    private bool HasReachedPoint(Vector3 point)
    {
        Vector3 a = transform.position;
        Vector3 b = point;

        a.y = 0f;
        b.y = 0f;

        return Vector3.Distance(a, b) <= searchPointReachDistance;
    }

    private bool CanDetectPlayerInAlertRadius()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > alertRadius)
            return false;

        if (!alertRequiresLineOfSight)
            return true;

        return HasClearLineToPlayer();
    }

    private bool CanSeePlayer()
    {
        Vector3 start = eyesPoint != null ? eyesPoint.position : transform.position + Vector3.up * 1.5f;
        Vector3 end = player.position + Vector3.up * 1.0f;
        Vector3 dir = end - start;

        float distance = dir.magnitude;

        if (distance > viewDistance)
            return false;

        float angle = Vector3.Angle(transform.forward, dir.normalized);
        if (angle > viewAngle * 0.5f)
            return false;

        return HasClearLineToPlayer();
    }

    private bool HasClearLineToPlayer()
    {
        Vector3 start = eyesPoint != null ? eyesPoint.position : transform.position + Vector3.up * 1.5f;
        Vector3 end = player.position + Vector3.up * 1.0f;
        Vector3 dir = end - start;
        float distance = dir.magnitude;

        if (wallMask == 0)
            return true;

        return !Physics.Raycast(
            start,
            dir.normalized,
            distance,
            wallMask,
            QueryTriggerInteraction.Ignore
        );
    }

    private void UpdateAnimation()
    {
        if (animator == null)
            return;

        animator.SetFloat("Speed", agent.velocity.magnitude);
        animator.SetBool("IsChasing", currentState == EnemyState.Chase);
    }

    public void NotifyTeleported(float cooldown)
    {
        nextTeleportAllowedTime = Time.time + cooldown;
    }

    public void TeleportTo(Vector3 targetPosition)
    {
        if (agent == null || !agent.enabled)
        {
            transform.position = targetPosition;
            return;
        }

        if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            if (agent.isOnNavMesh)
                agent.Warp(hit.position);
            else
                transform.position = hit.position;
        }
        else
        {
            transform.position = targetPosition;
        }

        agent.ResetPath();

        if (currentState == EnemyState.Chase)
        {
            agent.SetDestination(player.position);
        }
        else if (currentState == EnemyState.Search)
        {
            reachedLastKnownPosition = false;
            hasSearchPoint = false;
            waitTimer = 0f;
            agent.SetDestination(lastKnownPlayerPosition);
        }
    }

    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(lastKnownPlayerPosition, searchRadius);
    }

    public void ForceAlertToPlayer()
    {
        if (player == null || agent == null || !agent.enabled)
            return;

        currentState = EnemyState.Chase;
        agent.isStopped = false;
        agent.stoppingDistance = stoppingDistance;

        lastKnownPlayerPosition = player.position;
        reachedLastKnownPosition = false;
        hasSearchPoint = false;
        waitTimer = 0f;
        searchTimer = searchDuration;

        agent.SetDestination(player.position);
    }

    public void ForceAlertToPosition(Vector3 worldPosition)
    {
        if (agent == null || !agent.enabled)
            return;

        currentState = EnemyState.Search;
        agent.isStopped = false;
        agent.stoppingDistance = 0f;

        lastKnownPlayerPosition = worldPosition;
        reachedLastKnownPosition = false;
        hasSearchPoint = false;
        waitTimer = 0f;
        searchTimer = searchDuration;

        agent.SetDestination(lastKnownPlayerPosition);
    }
}