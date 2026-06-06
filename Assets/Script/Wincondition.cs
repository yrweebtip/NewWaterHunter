using UnityEngine;

public class Wincondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah objek yang masuk ke dalam area ini memiliki tag "BotolFiltrasi"
        if (other.CompareTag("ItemHasil"))
        {
            if (WinManager.Instance != null)
            {
                // Panggil layar kemenangan dan putar video dari WinManager
                WinManager.Instance.ShowWinScreen();
                
            }
            else
            {
                Debug.LogWarning("WinManager tidak ditemukan di scene ini!");
            }
        }
    }
}