using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WinManager : MonoBehaviour
{
    public static WinManager Instance;

    [Header("UI Panels")]
    public GameObject winCanvas;
    public GameObject mobileControlsUI;

    [Header("Video & Audio Settings")]
    public VideoPlayer winVideoPlayer;

    // ==========================================
    // VARIABEL BARU UNTUK BGM
    // ==========================================
    public AudioSource bgmAudioSource; // Tarik Empty Object BGM ke sini!

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
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }

    public void ShowWinScreen()
    {
        if (mobileControlsUI != null) mobileControlsUI.SetActive(false);

        if (winCanvas != null) winCanvas.SetActive(true);

        // ==========================================
        // MATIKAN MUSIK LATAR (BGM)
        // ==========================================
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Stop();
        }

        // Menghentikan waktu game
        Time.timeScale = 0f;

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
        Time.timeScale = 1f;

        CollectItem.inventoryPlayer.Clear();
        SceneManager.LoadScene(nextLevelName);
    }

    public void OnClickMainMenu()
    {
        Time.timeScale = 1f;

        CollectItem.inventoryPlayer.Clear();
        SceneManager.LoadScene(mainMenuName);
    }
}