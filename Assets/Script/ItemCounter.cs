using UnityEngine;
using UnityEngine.UI; // Wajib jika menggunakan Text biasa
// using TMPro; // Hilangkan garis miring di depan jika menggunakan TextMeshPro

public class ItemCounter: MonoBehaviour
{
    public static ItemCounter Instance;

    [Header("UI Reference")]
    public Text counterText; // Ganti jadi public TextMeshProUGUI jika pakai TMP

    [Header("Pengaturan Jumlah Barang")]
    public int targetBarang = 5; // Jumlah total barang yang harus dicari di level ini
    private int jumlahSekarang = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // Set tampilan awal saat game dimulai (misal: 0 / 5)
        UpdateTampilanUI();
    }

    // Fungsi untuk menambah hitungan barang
    public void TambahBarang()
    {
        jumlahSekarang++;
        
        // Memastikan angka tidak melebihi target
        if (jumlahSekarang > targetBarang) 
        {
            jumlahSekarang = targetBarang;
        }

        UpdateTampilanUI();
    }

    // Fungsi internal untuk memperbarui teks di layar
    private void UpdateTampilanUI()
    {
        if (counterText != null)
        {
            counterText.text = jumlahSekarang + " / " + targetBarang;
        }
    }
}