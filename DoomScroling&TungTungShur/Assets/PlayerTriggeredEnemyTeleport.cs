using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTriggeredEnemyTeleport : MonoBehaviour
{
    [SerializeField] private Jort_brain enemy;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private bool onlyIfEnemyIsTargetingPlayer = false;
    [SerializeField] private bool triggerOnlyOnce = true;
    [SerializeField] private float cooldown = 1f;

    private bool hasTriggered = false;

    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered && triggerOnlyOnce)
            return;

        if (!other.CompareTag("Player"))
            return;

        if (enemy == null || teleportPoint == null)
            return;

        if (onlyIfEnemyIsTargetingPlayer && !enemy.IsTargetingPlayer)
            return;

        if (!enemy.CanUseTeleport)
            return;

        enemy.NotifyTeleported(cooldown);
        enemy.TeleportTo(teleportPoint.position);

        if (triggerOnlyOnce)
            hasTriggered = true;
    }
}