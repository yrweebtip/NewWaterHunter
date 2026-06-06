using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public HealthSlider healthSlider;
    public float currentHealth = 100f;
    public GameObject gameoverUI;

    [Header("Audio Pengaturan")]
    public AudioSource playerAudioSource; // Sumber suara di karakter
    public AudioClip gameOverClip;        // File suara Game Over

    // ==========================================
    // VARIABEL BARU UNTUK BGM
    // ==========================================
    public AudioSource bgmAudioSource;    // Tarik Empty Object BGM ke sini!

    private bool isDead = false;

    private void Start()
    {
        gameoverUI.SetActive(false);
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        healthSlider.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        gameoverUI.SetActive(true);

        // ==========================================
        // 1. MATIKAN MUSIK LATAR (BGM)
        // ==========================================
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }

        // ==========================================
        // 2. PUTAR AUDIO GAME OVER
        // ==========================================
        if (playerAudioSource != null && gameOverClip != null)
        {
            playerAudioSource.PlayOneShot(gameOverClip);
        }

        Time.timeScale = 0f; // Game berhenti
        Debug.Log("Player has died.");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}