using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video; // Wajib ditambahkan untuk memutar video

public class WinManager : MonoBehaviour
{
    // Singleton agar mudah dipanggil dari script PickupHandler
    public static WinManager Instance;

    [Header("UI Panels")]
    public GameObject winCanvas;
    public GameObject mobileControlsUI; // Untuk mematikan joystick saat menang

    [Header("Video Settings")]
    public VideoPlayer winVideoPlayer;

    [Header("Scene Transition")]
    public string nextLevelName;
    public string mainMenuName = "TampilanAwalGame";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Pastikan Canvas Win disembunyikan di awal game
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }

    // Fungsi ini akan dipanggil otomatis saat Botol Filtrasi dipegang
    public void ShowWinScreen()
    {
        // 1. Matikan kontrol pergerakan pemain
        if (mobileControlsUI != null) mobileControlsUI.SetActive(false);

        // 2. Nyalakan Canvas Kemenangan
        if (winCanvas != null) winCanvas.SetActive(true);

        // 3. Putar Video Edukasi/Kemenangan
        if (winVideoPlayer != null)
        {
            winVideoPlayer.Play();
        }
    }

    // ==========================================
    // FUNGSI UNTUK TOMBOL DI WIN CANVAS
    // ==========================================
    public void OnClickNextLevel()
    {
        // Kosongkan tas pemain agar tidak terbawa ke level berikutnya
        CollectItem.inventoryPlayer.Clear();
        SceneManager.LoadScene(nextLevelName);
    }

    public void OnClickMainMenu()
    {
        CollectItem.inventoryPlayer.Clear();
        SceneManager.LoadScene(mainMenuName);
    }
}