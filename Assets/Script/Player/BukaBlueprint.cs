using UnityEngine;

public class BukaBlueprint : MonoBehaviour
{
    [Header("UI Blueprint")]
    public GameObject blueprintPanel; // Panel blueprint yang akan dibuka

    [Header("UI Kontrol Layar (Sembunyikan saat buka)")]
    public GameObject mobileControlsUI; // Tarik Canvas/Panel Joystick & Tombol lain ke sini

    [Header("Pengaturan Audio UI")]
    public AudioSource uiAudioSource; // Sumber suara
    public AudioClip suaraBuka;       // Suara saat blueprint dibuka
    public AudioClip suaraTutup;      // Suara saat blueprint ditutup

    private void Start()
    {
        // Pastikan blueprint tertutup dan kontrol aktif saat mulai
        if (blueprintPanel != null) blueprintPanel.SetActive(false);
        if (mobileControlsUI != null) mobileControlsUI.SetActive(true);
    }

    // ==========================================
    // FUNGSI UNTUK TOMBOL BUKA (LAYAR UTAMA)
    // ==========================================
    public void BukaPanelBlueprint()
    {
        // Putar suara buka (jika file audionya sudah dimasukkan)
        if (uiAudioSource != null && suaraBuka != null)
        {
            uiAudioSource.PlayOneShot(suaraBuka);
        }

        if (blueprintPanel != null) blueprintPanel.SetActive(true);

        // Sembunyikan UI joystick dan tombol lainnya
        if (mobileControlsUI != null) mobileControlsUI.SetActive(false);

        Time.timeScale = 0f; // Hentikan waktu
    }

    // ==========================================
    // FUNGSI UNTUK TOMBOL "X" / KEMBALI
    // ==========================================
    public void TutupPanelBlueprint()
    {
        // Putar suara tutup (jika file audionya sudah dimasukkan)
        if (uiAudioSource != null && suaraTutup != null)
        {
            uiAudioSource.PlayOneShot(suaraTutup);
        }

        if (blueprintPanel != null) blueprintPanel.SetActive(false);

        // Munculkan kembali UI joystick dan tombol lainnya
        if (mobileControlsUI != null) mobileControlsUI.SetActive(true);

        Time.timeScale = 1f; // Lanjutkan permainan
    }
}