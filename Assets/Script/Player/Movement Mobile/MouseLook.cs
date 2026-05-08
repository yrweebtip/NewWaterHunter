using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Mobile Inputs")]
    public TouchArea touchArea; // Hubungkan Panel UI transparan ke sini

    [Header("Camera Settings")]
    public float touchSensitivity = 10f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Update()
    {
        // Mengambil input delta (pergerakan) dari TouchArea
        float lookX = touchArea.touchDelta.x * touchSensitivity * Time.deltaTime;
        float lookY = touchArea.touchDelta.y * touchSensitivity * Time.deltaTime;

        // Mengatur rotasi vertikal (Atas/Bawah)
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Mengatur rotasi horizontal pada body player (Kiri/Kanan)
        playerBody.Rotate(Vector3.up * lookX);
    }

    // Fungsi debugging visual yang dikembalikan
    void OnDrawGizmos()
    {
        // Pengecekan agar tidak error di Editor jika kamera belum siap
        if (Camera.main != null)
        {
            // Input.mousePosition akan membaca kursor mouse di PC atau sentuhan pertama di Mobile
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(ray.origin, hit.point);
            }
        }
    }
}