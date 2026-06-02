using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ditambahkan untuk memuat ulang level

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public HealthSlider healthSlider; // Pastikan script HealthSlider milikmu merespon ini
    public float currentHealth = 100f;
    public GameObject gameoverUI;

    private void Start()
    {
        gameoverUI.SetActive(false);
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        healthSlider.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameoverUI.SetActive(true);
        Time.timeScale = 0f; // Game berhenti

        Debug.Log("Player has died.");
    }

    // ==========================================
    // FUNGSI BARU UNTUK TOMBOL RETRY
    // ==========================================
    public void RetryLevel()
    {
        // 1. Kembalikan waktu agar berjalan normal kembali (sangat penting!)
        Time.timeScale = 1f;

        // 2. Dapatkan nama scene yang sedang dimainkan saat ini, lalu muat ulang
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}