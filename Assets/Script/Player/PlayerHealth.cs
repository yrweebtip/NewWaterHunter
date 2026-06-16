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
    public AudioSource playerAudioSource; 
    public AudioClip gameOverClip;      

   
    public AudioSource bgmAudioSource;    

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

     
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }

        if (playerAudioSource != null && gameOverClip != null)
        {
            playerAudioSource.PlayOneShot(gameOverClip);
        }

        Time.timeScale = 0f; 
        Debug.Log("Player has died.");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}