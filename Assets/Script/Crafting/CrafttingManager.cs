using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrafttingManager : MonoBehaviour
{
    private Item currentitem;
    public Image customcusor;
    public Slot[] craftingSlots;

    [Header("UI System References")]
    public GameObject mainCraftingCanvas; // Tarik Canvas Crafting paling luar ke sini
    public GameObject mobileControlsUI;   // Tarik UI Joystick & Kamera ke sini

    [Header("Recipe Settings")]
    public List<Item> itemList;
    public string[] recipes;        // Syarat urutan nama item
    public Item[] reciperesult;     // Ikon UI hasil

    [Header("3D Spawner Settings")]
    // Objek 3D (Prefab) yang akan muncul di dunia nyata
    public GameObject[] resultPrefabs;
    // Titik lokasi munculnya item (misal: objek kosong di atas meja)
    public Transform spawnPoint;

    [Header("UI Buttons & Result")]
    public Slot resultslot;
    public Button craftButton; // Tombol untuk mengeksekusi rakitan
    public Button closeButton; // Tombol untuk sekadar menutup Canvas

    // Variabel internal untuk mengingat resep mana yang sedang terbentuk
    private int matchedRecipeIndex = -1;

    private void Start()
    {
        craftButton.gameObject.SetActive(false);
        itemList = new List<Item>(craftingSlots.Length);

        for (int i = 0; i < craftingSlots.Length; i++)
        {
            itemList.Add(null);
        }

        ResetUISlots();
    }

    private void Update()
    {
        bool isInputUp = false;
        Vector2 inputPosition = Vector2.zero;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                isInputUp = true;
                inputPosition = touch.position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isInputUp = true;
            inputPosition = Input.mousePosition;
        }

        if (isInputUp)
        {
            if (currentitem != null)
            {
                customcusor.gameObject.SetActive(false);
                Slot nearestslot = null;
                float shortestdistance = float.MaxValue;

                foreach (Slot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(inputPosition, slot.transform.position);

                    if (dist < shortestdistance)
                    {
                        shortestdistance = dist;
                        nearestslot = slot;
                    }
                }

                if (nearestslot != null)
                {
                    nearestslot.gameObject.SetActive(true);
                    nearestslot.GetComponent<Image>().sprite = currentitem.GetComponent<Image>().sprite;
                    nearestslot.item = currentitem;
                    itemList[nearestslot.Index] = currentitem;
                    currentitem = null;

                    CheckForCreatedRecipes();
                }
            }
        }
    }

    void CheckForCreatedRecipes()
    {
        resultslot.gameObject.SetActive(false);
        resultslot.item = null;
        craftButton.gameObject.SetActive(false);
        matchedRecipeIndex = -1; // Reset index

        string currentRecipeString = "";
        foreach (Item item in itemList)
        {
            currentRecipeString += item != null ? item.itemName : "null";
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if (currentRecipeString == recipes[i])
            {
                resultslot.gameObject.SetActive(true);
                resultslot.GetComponent<Image>().sprite = reciperesult[i].GetComponent<Image>().sprite;
                resultslot.item = reciperesult[i];

                craftButton.gameObject.SetActive(true); // Nyalakan tombol Craft
                matchedRecipeIndex = i; // Simpan index resep yang cocok
                return;
            }
        }
    }

    // ==========================================
    // FUNGSI TOMBOL CRAFT (RAKIT)
    // ==========================================
    public void OnClickCraftButton()
    {
        if (matchedRecipeIndex != -1 && matchedRecipeIndex < resultPrefabs.Length)
        {
            // 1. Munculkan Objek 3D di Dunia
            if (spawnPoint != null && resultPrefabs[matchedRecipeIndex] != null)
            {
                Instantiate(resultPrefabs[matchedRecipeIndex], spawnPoint.position, spawnPoint.rotation);
                Debug.Log("Item hasil crafting berhasil dimunculkan!");
            }
            else
            {
                Debug.LogWarning("Spawn Point atau Result Prefab 3D belum diatur di Inspector!");
            }

            // 2. Kosongkan "Tas" karena bahan-bahannya sudah terpakai
            CollectItem.inventoryPlayer.Clear();

            // 3. Tutup UI Crafting
            TutupCanvasCrafting();
        }
    }

    // ==========================================
    // FUNGSI TOMBOL CLOSE (X / KEMBALI)
    // ==========================================
    public void OnClickCloseButton()
    {
        TutupCanvasCrafting();
    }

    // Fungsi internal untuk membersihkan UI dan mematikan Canvas
    // Fungsi internal untuk membersihkan UI dan mematikan Canvas
    private void TutupCanvasCrafting()
    {
        // Batalkan jika ada item yang sedang dipegang (nempel di kursor)
        if (currentitem != null)
        {
            currentitem = null;
            customcusor.gameObject.SetActive(false);
        }

        ResetUISlots();

        // Normalkan waktu game
        Time.timeScale = 1f;

        // 1. MATIKAN CANVAS UTAMA, BUKAN HANYA MANAGER-NYA
        if (mainCraftingCanvas != null)
        {
            mainCraftingCanvas.SetActive(false);
        }

        // 2. NYALAKAN KEMBALI KONTROL MOBILE
        if (mobileControlsUI != null)
        {
            mobileControlsUI.SetActive(true);
        }
    }

    private void ResetUISlots()
    {
        for (int i = 0; i < craftingSlots.Length; i++)
        {
            craftingSlots[i].item = null;
            if (craftingSlots[i].GetComponent<Image>() != null)
            {
                craftingSlots[i].GetComponent<Image>().sprite = null;
            }
            craftingSlots[i].gameObject.SetActive(false);
            itemList[i] = null;
        }

        resultslot.item = null;
        resultslot.gameObject.SetActive(false);
        craftButton.gameObject.SetActive(false);
        matchedRecipeIndex = -1;
    }

    public void OnCkickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.Index] = null;
        slot.GetComponent<Image>().sprite = null;
        slot.gameObject.SetActive(false);
        CheckForCreatedRecipes();
    }

    public void OnMouseDownItem(Item item)
    {
        if (currentitem == null)
        {
            currentitem = item;
            customcusor.gameObject.SetActive(true);
            customcusor.sprite = currentitem.GetComponent<Image>().sprite;
        }
    }
}