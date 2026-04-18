using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Counter")]
    public int maxValue = 100;
    public int currentValue = 100;
    public int decreaseStep = 10;
    public float decreaseInterval = 5f;

    [Header("UI")]
    public TMP_Text counterText;
    public GameObject deathPanel;

    [Header("Settings")]
    public string enemyTag = "Enemy";

    private float timer;
    private bool isDead = false;

    void Start()
    {
        currentValue = maxValue;
        timer = decreaseInterval;
        UpdateUI();

        if (deathPanel != null)
            deathPanel.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            currentValue -= decreaseStep;
            timer = decreaseInterval;

            if (currentValue < 0)
                currentValue = 0;

            UpdateUI();

            if (currentValue <= 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag(enemyTag))
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag(enemyTag))
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = "Stress: " + currentValue;
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player died");

        if (deathPanel != null)
            deathPanel.SetActive(true);

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script != this)
                script.enabled = false;
        }


    }
}