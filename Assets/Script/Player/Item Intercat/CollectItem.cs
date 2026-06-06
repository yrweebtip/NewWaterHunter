using UnityEngine;
using System.Collections.Generic;

public class CollectItem : MonoBehaviour
{
    // Ini adalah "Tas" digital pemain. Semua item masuk ke sini.
    public static List<string> inventoryPlayer = new List<string>();

    [Header("Identitas Item")]
    public string itemName;

    [Header("Audio Pengambilan")]
    public AudioClip suaraAmbil; // Tarik efek suara MP3/WAV ke sini

    public static CollectItem itemTerdekat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemTerdekat = this;

            if (PlayerMovement.tombolAmbilStatic != null)
            {
                PlayerMovement.tombolAmbilStatic.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemTerdekat == this)
            {
                itemTerdekat = null;

                if (PlayerMovement.tombolAmbilStatic != null)
                {
                    PlayerMovement.tombolAmbilStatic.SetActive(false);
                }
            }
        }
    }

    public void Collect()
    {
        // Masukkan NAMA ITEM ke dalam tas pemain
        if (!inventoryPlayer.Contains(itemName))
        {
            inventoryPlayer.Add(itemName);

            if (ItemCounter.Instance != null)
            {
                ItemCounter.Instance.TambahBarang();
            }
        }

        Debug.Log($"{itemName} berhasil dikumpulkan!");

        // ==========================================
        // FITUR BARU: PUTAR AUDIO SEBELUM HANCUR
        // ==========================================
        if (suaraAmbil != null)
        {
            // Kita putar persis di posisi Kamera agar suaranya terdengar jelas (2D) 
            // dan tidak terpotong saat objek item ini dihancurkan
            AudioSource.PlayClipAtPoint(suaraAmbil, Camera.main.transform.position);
        }
        // ==========================================

        SubtitleData subData = GetComponent<SubtitleData>();
        if (subData != null)
        {
            subData.TriggerThisSubtitle();
        }

        // Matikan tombol UI
        if (PlayerMovement.tombolAmbilStatic != null)
        {
            PlayerMovement.tombolAmbilStatic.SetActive(false);
        }

        itemTerdekat = null;
        Destroy(gameObject); // Hapus objek 3D dari map
    }
}