using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;

    [Header("UI & Audio References")]
    public Text subtitleText;
    private AudioSource audioSource;

    private Coroutine currentCoroutine;

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

    public void PlaySubtitleSequence(string[] messages, float[] durations, AudioClip[] clips = null)
    {
   
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        audioSource.Stop();

        currentCoroutine = StartCoroutine(SubtitleSequence(messages, durations, clips));
    }

    private IEnumerator SubtitleSequence(string[] messages, float[] durations, AudioClip[] clips)
    {
        subtitleText.enabled = true;

        for (int i = 0; i < messages.Length; i++)
        {

            subtitleText.text = messages[i];

            if (clips != null && i < clips.Length && clips[i] != null)
            {
                audioSource.clip = clips[i];
                audioSource.Play();
            }

            yield return new WaitForSeconds(durations[i]);
        }

        subtitleText.text = "";
        subtitleText.enabled = false;
    }
}