using UnityEngine;
using System.Collections.Generic;
public class CraftingTableInteraction : MonoBehaviour

{

    [Header("Crafting UI Panel")]

    public GameObject craftingCanvas;



    [Header("Mobile UI (Joystick & Camera Area)")]

    public GameObject mobileControlsUI;



    [Header("Tombol Interaksi")]

    // Tarik tombol UI "Buka Meja" milikmu ke sini

    public GameObject tombolBukaCrafting;



    [Header("Syarat Item untuk Meja Ini")]

    public List<string> requiredItems = new List<string>();



    private void Start()

    {

        if (craftingCanvas != null) craftingCanvas.SetActive(false);



        // Pastikan tombol interaksi disembunyikan saat game baru dimulai

        if (tombolBukaCrafting != null) tombolBukaCrafting.SetActive(false);

    }



    private void OnTriggerEnter(Collider other)

    {

        if (other.CompareTag("Player"))
        {
            if (AllItemsCollected())
            {
                if (tombolBukaCrafting != null) tombolBukaCrafting.SetActive(true);
            }
            else
            {
                Debug.Log("Bahan belum lengkap! Kumpulkan bahan sesuai level ini dulu.");
            }

        }

    }
    private void OnTriggerExit(Collider other)

    {

        if (other.CompareTag("Player"))

        {

            if (tombolBukaCrafting != null) tombolBukaCrafting.SetActive(false);

        }

    }

    private bool AllItemsCollected()

    {

        if (requiredItems.Count == 0) return true;



        foreach (string reqItem in requiredItems)

        {

            if (!CollectItem.inventoryPlayer.Contains(reqItem))

            {

                return false;

            }

        }

        return true;

    }

    public void BukaCrafting()

    {

        if (craftingCanvas != null) craftingCanvas.SetActive(true);

        if (mobileControlsUI != null) mobileControlsUI.SetActive(false);

        if (tombolBukaCrafting != null) tombolBukaCrafting.SetActive(false);

        Time.timeScale = 0f;

    }



    public void TutupCrafting()

    {

        if (craftingCanvas != null) craftingCanvas.SetActive(false);

        if (mobileControlsUI != null) mobileControlsUI.SetActive(true);



        // Munculkan lagi tombol interaksinya saat pemain menutup menu

        // (karena posisi pemain masih berdiri di dekat meja)

        if (tombolBukaCrafting != null) tombolBukaCrafting.SetActive(true);



        Time.timeScale = 1f;

    }

}