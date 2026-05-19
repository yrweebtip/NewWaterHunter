using UnityEngine;

public class TextFollowCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        // Mencari kamera utama di dalam scene secara otomatis
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("Kamera utama tidak ditemukan! Pastikan kamera memiliki tag 'MainCamera'.");
        }
    }

    // Menggunakan LateUpdate agar teks berputar SETELAH kamera bergerak
    private void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Rumus ini membuat UI menghadap kamera dengan sempurna 
            // tanpa membuat teksnya terbalik (mirrored)
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}