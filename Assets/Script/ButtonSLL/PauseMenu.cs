using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuCanvas;

    [Header("Pengaturan Audio UI")]
    public AudioSource uiAudioSource; 
    public AudioClip suaraPause;      
    public AudioClip suaraResume;     
    public AudioClip suaraKeluar;    

    private void Start()
    {
        
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
        Time.timeScale = 1f;
    }

   
    public void PauseGame()
    {
        // Putar suara pause
        if (uiAudioSource != null && suaraPause != null)
        {
            uiAudioSource.PlayOneShot(suaraPause);
        }

        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void ResumeGame()
    {
        // Putar suara resume
        if (uiAudioSource != null && suaraResume != null)
        {
            uiAudioSource.PlayOneShot(suaraResume);
        }

        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }


        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        if (uiAudioSource != null && suaraKeluar != null)
        {
            uiAudioSource.PlayOneShot(suaraKeluar);
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tampilan Awal Scene");
    }
}