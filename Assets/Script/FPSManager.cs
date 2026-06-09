using UnityEngine;

public class FPSManager : MonoBehaviour
{
    // Membuat variabel statis untuk mengecek apakah manajer ini sudah ada
    private static FPSManager instance;

    private void Awake()
    {
        // Mengecek apakah sudah ada FPSManager lain di dalam game
        if (instance == null)
        {
            instance = this;

            // JADIKAN OBJEK INI ABADI (TIDAK HANCUR SAAT PINDAH SCENE)
            DontDestroyOnLoad(gameObject);

            // Terapkan pengaturan FPS
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
        else
        {
            // Jika pemain kembali ke Main Menu dan menemukan FPSManager ganda, hancurkan yang baru
            Destroy(gameObject);
        }
    }
}