using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinTrigger : MonoBehaviour
{
    public GameObject winCanvas;
    public TMP_Text restartText;
    public float restartDelay = 5f;

    private bool triggered = false;

    private void Start()
    {
        if (winCanvas != null)
            winCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            if (winCanvas != null)
                winCanvas.SetActive(true);

            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StartCoroutine(RestartCountdown(restartDelay));
        }
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

    void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}