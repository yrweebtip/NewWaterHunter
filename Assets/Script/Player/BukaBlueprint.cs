using UnityEngine;

public class BukaBlueprint : MonoBehaviour
{
    [Header("UI Blueprint")]
    public GameObject blueprintPanel; // Panel blueprint yang akan dibuka

    [Header("UI Kontrol Layar (Sembunyikan saat buka)")]
    public GameObject mobileControlsUI; // Tarik Canvas/Panel Joystick & Tombol lain ke sini

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
        if (blueprintPanel != null) blueprintPanel.SetActive(false);

        // Munculkan kembali UI joystick dan tombol lainnya
        if (mobileControlsUI != null) mobileControlsUI.SetActive(true);

        Time.timeScale = 1f; // Lanjutkan permainan
    }
}