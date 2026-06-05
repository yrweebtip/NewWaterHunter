using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Transform holdPoint;          // Titik tempat item dipegang
    public float pickupRange = 3.0f;     // Jarak maksimal raycast
    public Transform raycastOrigin;      // Titik asal sinar (Biasanya kamera atau dada karakter)

    [Header("Mobile UI")]
    // Tarik UI Button (Tombol Ambil/Buang) milikmu ke sini
    public GameObject actionButton;

    private GameObject heldItem;
    private GameObject targetItem;       // Item yang sedang ditatap (terkena raycast)

    private void Start()
    {
        // Pastikan tombol disembunyikan saat game baru dimulai
        if (actionButton != null)
        {
            actionButton.SetActive(false);
        }
    }

    private void Update()
    {
        // 1. Jika tangan KOSONG, lakukan Raycast untuk mencari item
        if (heldItem == null)
        {
            PerformRaycast();
        }
        // 2. Jika SEDANG MEMEGANG item, tombol harus selalu menyala (untuk membuang)
        else
        {
            if (actionButton != null && !actionButton.activeSelf)
            {
                actionButton.SetActive(true);
            }
        }
    }

    private void PerformRaycast()
    {
        // Menembakkan sinar (ray) lurus ke depan dari posisi origin
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, pickupRange))
        {
            // Mengecek apakah objek yang tertabrak sinar memiliki tag "Pickup"
            if (hit.collider.CompareTag("ItemHasil"))
            {
                targetItem = hit.collider.gameObject;

                // Nyalakan tombol UI karena player sedang melihat item
                if (actionButton != null) actionButton.SetActive(true);
                return;
            }
        }

        // Jika sinar meleset atau tidak mengenai item dengan tag "Pickup"
        targetItem = null;
        if (actionButton != null) actionButton.SetActive(false);
    }

    // ==========================================
    // FUNGSI UNTUK TOMBOL MOBILE (ON CLICK)
    // ==========================================
    public void OnActionButtonPressed()
    {
        // Jika tangan kosong dan ada target item -> AMBIL
        if (heldItem == null)
        {
            if (targetItem != null)
            {
                PickupItem(targetItem);
            }
        }
        // Jika sedang memegang sesuatu -> BUANG
        else
        {
            DropItem();
        }
    }

    private void PickupItem(GameObject item)
    {
        heldItem = item;
        heldItem.transform.SetParent(holdPoint);
        heldItem.transform.localPosition = Vector3.zero;
        heldItem.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Debug.Log("Item diambil: " + heldItem.name);

        // Kosongkan target setelah diambil
        targetItem = null;
    }

    private void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.transform.SetParent(null);
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Debug.Log("Item dilepaskan: " + heldItem.name);
            heldItem = null;
        }
    }

    public bool IsHoldingItem(GameObject item)
    {
        return heldItem == item;
    }
}