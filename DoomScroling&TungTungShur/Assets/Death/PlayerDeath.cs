using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    [Header("UI")]
    public GameObject deathScreenCanvas;
    public Image blackOverlay;          // Чёрный фон который затемняет
    public TextMeshProUGUI deadText;    // Текст "Dead"

    [Header("Настройки анимации")]
    public float fadeDuration = 1.5f;   // Время затемнения экрана
    public float textFadeDelay = 0.5f;  // Задержка перед появлением текста
    public float textFadeDuration = 1f; // Время появления текста

    [Header("Настройки")]
    public string monsterTag = "Monster";
    public bool freezeOnDeath = true;

    private bool _isDead = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(monsterTag)) Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(monsterTag)) Die();
    }

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        deathScreenCanvas.SetActive(true);

        // Стартуем с нулевой прозрачности
        SetAlpha(blackOverlay, 0f);
        SetTextAlpha(deadText, 0f);

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // Шаг 1: затемняем экран
        yield return StartCoroutine(FadeImage(blackOverlay, 0f, 1f, fadeDuration));

        // Шаг 2: замораживаем игру после затемнения
        if (freezeOnDeath) Time.timeScale = 0f;

        // Шаг 3: небольшая пауза
        yield return new WaitForSecondsRealtime(textFadeDelay);

        // Шаг 4: проявляем текст
        yield return StartCoroutine(FadeText(deadText, 0f, 1f, textFadeDuration));
    }

    private IEnumerator FadeImage(Image img, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = img.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            img.color = c;
            yield return null;
        }

        c.a = to;
        img.color = c;
    }

    private IEnumerator FadeText(TextMeshProUGUI txt, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = txt.color;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // unscaled — работает при timeScale = 0
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            txt.color = c;
            yield return null;
        }

        c.a = to;
        txt.color = c;
    }

    private void SetAlpha(Image img, float a)
    {
        Color c = img.color;
        c.a = a;
        img.color = c;
    }

    private void SetTextAlpha(TextMeshProUGUI txt, float a)
    {
        Color c = txt.color;
        c.a = a;
        txt.color = c;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}