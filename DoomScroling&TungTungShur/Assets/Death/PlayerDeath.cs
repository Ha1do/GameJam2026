using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathScreenCanvas;
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
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}