using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    // ==========================================
    // SINGLETON: Agar bisa dipanggil dari script mana saja!
    // ==========================================
    public static SubtitleManager Instance;

    [Header("UI & Audio References")]
    public Text subtitleText;
    private AudioSource audioSource;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        // Setup Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Otomatis menambahkan AudioSource jika belum ada
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (subtitleText != null)
        {
            subtitleText.text = "";
            subtitleText.enabled = false;
        }
    }

    // ==========================================
    // FUNGSI UNIVERSAL UNTUK MEMUTAR SUBTITLE
    // ==========================================
    public void PlaySubtitleSequence(string[] messages, float[] durations, AudioClip[] clips = null)
    {
        // Hentikan subtitle & suara sebelumnya jika ada yang bertabrakan
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        audioSource.Stop();

        // Mulai yang baru
        currentCoroutine = StartCoroutine(SubtitleSequence(messages, durations, clips));
    }

    private IEnumerator SubtitleSequence(string[] messages, float[] durations, AudioClip[] clips)
    {
        subtitleText.enabled = true;

        for (int i = 0; i < messages.Length; i++)
        {
            // Tampilkan Teks
            subtitleText.text = messages[i];

            // Mainkan Suara (jika ada)
            if (clips != null && i < clips.Length && clips[i] != null)
            {
                audioSource.clip = clips[i];
                audioSource.Play();
            }

            // Tunggu sesuai durasi yang ditentukan
            yield return new WaitForSeconds(durations[i]);
        }

        // Bersihkan layar setelah selesai
        subtitleText.text = "";
        subtitleText.enabled = false;
    }
}