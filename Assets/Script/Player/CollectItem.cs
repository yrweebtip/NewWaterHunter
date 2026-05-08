using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public static bool hasKerikil, hasArang, hasSpons, hasIjuk, hasBotol;
    public string itemName;

    public static CollectItem itemTerdekat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemTerdekat = this;

            // Nyalakan tombol ambil dari referensi statis di PlayerMovement
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

                // Matikan tombol UI karena player menjauh
                if (PlayerMovement.tombolAmbilStatic != null)
                {
                    PlayerMovement.tombolAmbilStatic.SetActive(false);
                }
            }
        }
    }

    public void Collect()
    {
        switch (itemName)
        {
            case "Kerikil":
                hasKerikil = true;
                break;
            case "Arang":
                hasArang = true;
                break;
            case "Spons":
                hasSpons = true;
                break;
            case "Ijuk":
                hasIjuk = true;
                break;
            case "Botol":
                hasBotol = true;
                break;
        }

        Debug.Log($"{itemName} telah diambil!");

        // Wajib matikan tombol UI sebelum item dihancurkan
        if (PlayerMovement.tombolAmbilStatic != null)
        {
            PlayerMovement.tombolAmbilStatic.SetActive(false);
        }

        itemTerdekat = null;
        Destroy(gameObject);
    }
}