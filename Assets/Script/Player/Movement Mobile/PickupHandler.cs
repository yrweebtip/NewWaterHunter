using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Transform holdPoint;          // Titik tempat item dipegang
    public float pickupRange = 3.0f;     // Jarak maksimal raycast
    public Transform raycastOrigin;      // Titik asal sinar (Biasanya kamera atau dada karakter)

    [Header("Mobile UI")]
    public GameObject actionButton;

    [Header("Audio Settings")]
    public AudioSource playerAudioSource; // Audio Source pada karakter
    public AudioClip suaraAmbil;          // Suara saat barang diambil
    public AudioClip suaraBuang;          // Suara saat barang dilempar/dilepas

    private GameObject heldItem;
    private GameObject targetItem;

    private void Start()
    {
        if (actionButton != null)
        {
            actionButton.SetActive(false);
        }
    }

    private void Update()
    {
        if (heldItem == null)
        {
            PerformRaycast();
        }
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
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, pickupRange))
        {
            if (hit.collider.CompareTag("ItemHasil"))
            {
                targetItem = hit.collider.gameObject;

                if (actionButton != null) actionButton.SetActive(true);
                return;
            }
        }

        targetItem = null;
        if (actionButton != null) actionButton.SetActive(false);
    }

    // ==========================================
    // FUNGSI UNTUK TOMBOL MOBILE (ON CLICK)
    // ==========================================
    public void OnActionButtonPressed()
    {
        if (heldItem == null)
        {
            if (targetItem != null)
            {
                PickupItem(targetItem);
            }
        }
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

        // ==========================================
        // PUTAR AUDIO AMBIL BARANG
        // ==========================================
        if (playerAudioSource != null && suaraAmbil != null)
        {
            playerAudioSource.PlayOneShot(suaraAmbil);
        }

        // ==========================================
        // FITUR BARU: TRIGGER SUBTITLE DARI ITEM
        // ==========================================
        SubtitleData subData = item.GetComponent<SubtitleData>();
        if (subData != null)
        {
            subData.TriggerThisSubtitle();
        }
        // ==========================================

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

            // ==========================================
            // PUTAR AUDIO BUANG BARANG
            // ==========================================
            if (playerAudioSource != null && suaraBuang != null)
            {
                playerAudioSource.PlayOneShot(suaraBuang);
            }

            heldItem = null;
        }
    }

    public bool IsHoldingItem(GameObject item)
    {
        return heldItem == item;
    }
}