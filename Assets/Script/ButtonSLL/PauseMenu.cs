using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuCanvas;

    [Header("Pengaturan Audio UI")]
    public AudioSource uiAudioSource; // Sumber suara khusus UI
    public AudioClip suaraPause;      // Suara saat panel pause terbuka
    public AudioClip suaraResume;     // Suara saat kembali bermain
    public AudioClip suaraKeluar;     // Suara saat menekan tombol ke Main Menu

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
        // Putar suara pause
        if (uiAudioSource != null && suaraPause != null)
        {
            uiAudioSource.PlayOneShot(suaraPause);
        }

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
        // Putar suara resume
        if (uiAudioSource != null && suaraResume != null)
        {
            uiAudioSource.PlayOneShot(suaraResume);
        }

        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }

        // Lanjutkan waktu game
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        // Putar suara keluar (Opsional: mungkin tidak sempat terdengar penuh jika scene langsung berganti, 
        // tapi PlayOneShot akan mencoba memutarnya di frame terakhir sebelum pindah)
        if (uiAudioSource != null && suaraKeluar != null)
        {
            uiAudioSource.PlayOneShot(suaraKeluar);
        }

        // 1. Wajib normalkan waktu dulu sebelum pindah scene
        Time.timeScale = 1f;

        // 2. Pindah ke scene tampilan awal
        SceneManager.LoadScene("Tampilan Awal Scene");
    }
}