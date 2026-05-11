using UnityEngine;
using System.Collections.Generic;

public class CollectItem : MonoBehaviour
{
    // Ini adalah "Tas" digital pemain. Semua item masuk ke sini.
    public static List<string> inventoryPlayer = new List<string>();

    [Header("Identitas Item")]
    // Tulis nama item di Inspector Unity! (Misal: Baskom, Pipa, Kerikil)
    public string itemName;

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
        }

        Debug.Log($"{itemName} berhasil dikumpulkan!");

        // Matikan tombol UI
        if (PlayerMovement.tombolAmbilStatic != null)
        {
            PlayerMovement.tombolAmbilStatic.SetActive(false);
        }

        itemTerdekat = null;
        Destroy(gameObject); // Hapus objek 3D dari map
    }
}