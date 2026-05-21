using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    [Header("Wave Settings")]
    public float waveHeight = 0.2f;       // Tinggi rendahnya gelombang
    public float waveFrequency = 0.5f;    // Jarak antar puncak gelombang (kerapatan)
    public float waveSpeed = 2.0f;        // Kecepatan gerak gelombang

    private MeshFilter meshFilter;
    private Mesh mesh;
    private Vector3[] baseVertices;       // Menyimpan posisi asli vertex agar tidak rusak

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.LogError("Script ini harus dipasang pada objek yang memiliki Mesh Filter (seperti Plane)!");
            return;
        }

        // Duplikat mesh asli agar tidak merusak aset master di project
        mesh = meshFilter.mesh;

        // OPTIMASI UNITY: Beritahu engine bahwa mesh ini akan sering diubah fungsinya setiap frame
        mesh.MarkDynamic();

        // Simpan posisi koordinat awal semua vertex
        baseVertices = mesh.vertices;
    }

    void Update()
    {
        if (baseVertices == null) return;

        // Buat array baru untuk menampung posisi vertex yang sudah diubah
        Vector3[] vertices = new Vector3[baseVertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            // RUMUS FISIKA GELOMBANG (Sine Wave)
            // Menghitung tinggi gelombang berdasarkan posisi global X, Z dan waktu berjalan
            float waveX = Mathf.Sin(Time.time * waveSpeed + (vertex.x * waveFrequency));
            float waveZ = Mathf.Cos(Time.time * waveSpeed + (vertex.z * waveFrequency));

            // Masukkan hasil gelombang ke posisi Y (tinggi) si vertex
            vertex.y = (waveX + waveZ) * waveHeight;

            // Masukkan kembali ke daftar array
            vertices[i] = vertex;
        }

        // Terapkan posisi vertex baru ke mesh air
        mesh.vertices = vertices;

        // Wajib dipanggil agar pencahayaan, pantulan material, dan bayangan di air ikut bergerak mulus
        mesh.RecalculateNormals();
    }
}