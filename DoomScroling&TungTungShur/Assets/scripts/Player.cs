using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Counter")]
    public int maxValue = 100;
    public int currentValue = 0;
    public int increaseStep = 10;
    public float increaseInterval = 6f;

    [Header("UI")]
    public TMP_Text counterText;
    public Image stressBarFill;
    public GameObject deathPanel;

    [Header("Bar Animation")]
    public float barSmoothSpeed = 3f;

    [Header("Settings")]
    public string enemyTag = "Enemy";

    private float timer;
    private bool isDead = false;

    private float targetFill = 1f;
    private float currentFill = 1f;

    void Start()
    {
        currentValue = 0;
        timer = increaseInterval;

        if (deathPanel != null)
            deathPanel.SetActive(false);

        UpdateUIInstant();
    }

    void Update()
    {
        if (!isDead)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                currentValue += increaseStep;

                if (currentValue > maxValue)
                    currentValue = maxValue;

                timer = increaseInterval;

                UpdateUI();

                if (currentValue >= maxValue)
                    Die();
            }
        }

        SmoothBar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag(enemyTag))
            AddStress(20);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag(enemyTag))
            AddStress(20);
    }

    void AddStress(int amount)
    {
        currentValue += amount;

        if (currentValue > maxValue)
            currentValue = maxValue;

        UpdateUI();

        if (currentValue >= maxValue)
            Die();
    }

    void UpdateUI()
    {
        if (counterText != null)
            counterText.text = "Stress: " + currentValue;

        targetFill = 1f - ((float)currentValue / maxValue);
    }

    void UpdateUIInstant()
    {
        if (counterText != null)
            counterText.text = "Stress: " + currentValue;

        targetFill = 1f - ((float)currentValue / maxValue);
        currentFill = targetFill;

        if (stressBarFill != null)
            stressBarFill.fillAmount = currentFill;
    }

    void SmoothBar()
    {
        if (stressBarFill == null) return;

        currentFill = Mathf.Lerp(currentFill, targetFill, Time.deltaTime * barSmoothSpeed);

        if (Mathf.Abs(currentFill - targetFill) < 0.001f)
            currentFill = targetFill;

        stressBarFill.fillAmount = currentFill;
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

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}