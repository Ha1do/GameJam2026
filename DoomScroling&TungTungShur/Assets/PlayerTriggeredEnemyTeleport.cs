using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerTriggeredEnemyTeleport : MonoBehaviour
{
    private enum AlertMode
    {
        None,
        PlayerPosition,
        TriggerPosition,
        CustomPoint
    }

    [Header("Teleport")]
    [SerializeField] private Jort_brain enemy;
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private bool triggerOnlyOnce = true;

    [Header("This Trigger Cooldown Only")]
    [SerializeField] private float cooldown = 1f;

    [Header("Alert After Teleport")]
    [SerializeField] private AlertMode alertMode = AlertMode.PlayerPosition;
    [SerializeField] private Transform customAlertPoint;

    private bool hasTriggered = false;
    private float nextAllowedTime = 0f;

    private void Reset()
    {
        Collider col = GetComponent<Collider>();
        if (col != null)
            col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (triggerOnlyOnce && hasTriggered)
            return;

        if (Time.time < nextAllowedTime)
            return;

        if (enemy == null || teleportPoint == null)
            return;

        nextAllowedTime = Time.time + cooldown;

        enemy.TeleportTo(teleportPoint.position);

        switch (alertMode)
        {
            case AlertMode.PlayerPosition:
                enemy.ForceAlertToPlayer();
                break;

            case AlertMode.TriggerPosition:
                enemy.ForceAlertToPosition(transform.position);
                break;

            case AlertMode.CustomPoint:
                if (customAlertPoint != null)
                    enemy.ForceAlertToPosition(customAlertPoint.position);
                break;
        }

        if (triggerOnlyOnce)
            hasTriggered = true;
    }
}