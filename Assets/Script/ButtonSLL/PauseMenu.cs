using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuCanvas;

    private void Start()
    {
        // Pastikan panel pause mati dan waktu berjalan normal saat game mulai
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    // ==========================================
    // HUBUNGKAN KE TOMBOL UI PAUSE DI LAYAR (⏸)
    // ==========================================
    public void PauseGame()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(true);
        }

        // Hentikan waktu game
        Time.timeScale = 0f;
    }

    // ==========================================
    // HUBUNGKAN KE TOMBOL UI RESUME DI DALAM PANEL (▶)
    // ==========================================
    public void ResumeGame()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }

        // Lanjutkan waktu game
        Time.timeScale = 1f;
    }
}
