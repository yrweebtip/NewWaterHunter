using UnityEngine;
using UnityEngine.UI; // Wajib jika menggunakan Text biasa
// using TMPro; // Hilangkan garis miring di depan jika menggunakan TextMeshPro

public class ItemCounter: MonoBehaviour
{
    public static ItemCounter Instance;

    [Header("UI Reference")]
    public Text counterText; 

    [Header("Pengaturan Jumlah Barang")]
    public int targetBarang = 5; 
    private int jumlahSekarang = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        UpdateTampilanUI();
    }

    public void TambahBarang()
    {
        jumlahSekarang++;
        
        if (jumlahSekarang > targetBarang) 
        {
            jumlahSekarang = targetBarang;
        }

        UpdateTampilanUI();
    }

    private void UpdateTampilanUI()
    {
        if (counterText != null)
        {
            counterText.text = jumlahSekarang + " / " + targetBarang;
        }
    }
}