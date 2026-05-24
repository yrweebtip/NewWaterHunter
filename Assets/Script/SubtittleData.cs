using UnityEngine;

public class SubtitleData : MonoBehaviour
{
    [Header("Isi Subtitle & Suara")]
    public string[] subtitles;
    public float[] subtitleDurations;
    public AudioClip[] voiceClips;

    // Fungsi ini bisa kamu panggil dari EVENT, TOMBOL UI, atau SCRIPT LAIN
    public void TriggerThisSubtitle()
    {
        // Memanggil fungsi dari SubtitleManager tanpa perlu FindObjectOfType!
        if (SubtitleManager.Instance != null)
        {
            SubtitleManager.Instance.PlaySubtitleSequence(subtitles, subtitleDurations, voiceClips);
        }
        else
        {
            Debug.LogWarning("Subtitle Manager belum ada di Scene!");
        }
    }
}