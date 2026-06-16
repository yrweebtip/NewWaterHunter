using UnityEngine;

public class Wincondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemHasil"))
        {
            if (WinManager.Instance != null)
            {
                WinManager.Instance.ShowWinScreen();
                
            }
            else
            {
                Debug.LogWarning("WinManager tidak ditemukan di scene ini!");
            }
        }
    }
}