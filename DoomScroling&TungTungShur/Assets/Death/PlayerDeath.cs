using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathScreenCanvas;
    public TMP_Text restartText;
    public string monsterTag = "Monster";

    private bool _isDead = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(monsterTag)) Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(monsterTag)) Die();
    }

    public void Die()
    {
        if (_isDead) return;
        _isDead = true;

        deathScreenCanvas.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(RestartCountdown(5f));
    }

    IEnumerator RestartCountdown(float time)
    {
        float remaining = time;

        while (remaining > 0f)
        {
            if (restartText != null)
                restartText.text = "Restarting in " + Mathf.CeilToInt(remaining);

            yield return new WaitForSecondsRealtime(1f);
            remaining -= 1f;
        }

        Restart();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}