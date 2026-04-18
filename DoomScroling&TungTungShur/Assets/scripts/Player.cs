using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Counter")]
    public int maxValue = 100;
    public float currentValue = 0f;
    public int increaseStep = 10;
    public float increaseInterval = 6f;

    [Header("UI")]
    public TMP_Text counterText;
    public Image stressBarFill;

    [Header("Bar Animation")]
    public float barSmoothSpeed = 3f;

    [Header("Settings")]
    public string enemyTag = "Monster";

    [Header("Phone Stress Reduce")]
    public PhoneToggle phoneToggle;
    public float reduceMultiplier = 2f;

    private float timer;
    private bool isDead = false;

    private float targetFill = 1f;
    private float currentFill = 1f;

    private PlayerDeath playerDeath;

    void Start()
    {
        currentValue = 0f;
        timer = increaseInterval;

        playerDeath = GetComponent<PlayerDeath>();

        if (phoneToggle == null)
            phoneToggle = GetComponent<PhoneToggle>();

        UpdateUIInstant();
    }

    void Update()
    {
        if (isDead) return;

        if (phoneToggle != null && phoneToggle.IsPhoneOpen)
        {
            ReduceStressWhilePhoneOpen();
        }
        else
        {
            IncreaseStressOverTime();
        }

        SmoothBar();
    }

    void IncreaseStressOverTime()
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

    void ReduceStressWhilePhoneOpen()
    {
        float increasePerSecond = increaseStep / increaseInterval;
        float reducePerSecond = increasePerSecond * reduceMultiplier;

        currentValue -= reducePerSecond * Time.deltaTime;

        if (currentValue < 0f)
            currentValue = 0f;

        timer = increaseInterval;
        UpdateUI();
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

    void AddStress(float amount)
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
            counterText.text = "Stress: " + Mathf.RoundToInt(currentValue);

        targetFill = 1f - (currentValue / maxValue);
    }

    void UpdateUIInstant()
    {
        if (counterText != null)
            counterText.text = "Stress: " + Mathf.RoundToInt(currentValue);

        targetFill = 1f - (currentValue / maxValue);
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

        if (playerDeath != null)
            playerDeath.Die();
        else
            Debug.LogWarning("PlayerDeath не найден на объекте игрока");
    }
}