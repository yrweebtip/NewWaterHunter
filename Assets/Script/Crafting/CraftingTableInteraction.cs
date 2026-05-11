using UnityEngine;
using System.Collections.Generic; // Wajib ditambahkan untuk menggunakan List

public class CraftingTableInteraction : MonoBehaviour
{
    [Header("Crafting UI Panel")]
    public GameObject craftingCanvas;

    [Header("Syarat Item untuk Meja Ini")]
    // List ini akan muncul di Inspector Unity!
    // Kamu bisa menambah, mengurangi, dan mengganti nama item syaratnya dari sana.
    [Header("Mobile UI (Joystick & Camera Area)")]
    public GameObject mobileControlsUI;

    public List<string> requiredItems = new List<string>();

    private void Start()
    {
        if (craftingCanvas != null)
        {
            craftingCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (AllItemsCollected())
            {
                BukaCrafting();
            }
            else
            {
                Debug.Log("Bahan belum lengkap! Kumpulkan bahan sesuai level ini dulu.");
            }
        }
    }

    private bool AllItemsCollected()
    {
        // Jika tidak ada syarat item di Inspector, langsung lolos
        if (requiredItems.Count == 0) return true;

        // Script akan mengecek satu per satu item yang kamu tulis di Inspector
        foreach (string reqItem in requiredItems)
        {
            // Jika pemain TIDAK PUNYA salah satu item tersebut di dalam tasnya
            if (!CollectItem.inventoryPlayer.Contains(reqItem))
            {
                return false; // Langsung tolak, kembalikan nilai false
            }
        }

        // Jika loop selesai dan tidak ada yang kurang, berarti item lengkap!
        return true;
    }

    // ==========================================
    // FUNGSI UNTUK MEMBUKA & MENUTUP UI
    // ==========================================

    public void BukaCrafting()
    {
        if (craftingCanvas != null) craftingCanvas.SetActive(true);
        if (mobileControlsUI != null) mobileControlsUI.SetActive(false); // Sembunyikan kontrol
        Time.timeScale = 0f;
    }

    public void TutupCrafting()
    {
        if (craftingCanvas != null) craftingCanvas.SetActive(false);
        if (mobileControlsUI != null) mobileControlsUI.SetActive(true); // Munculkan kontrol lagi
        Time.timeScale = 1f;
    }
}