using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private void Update()
    {
        // 1. Jika ada jari yang menyentuh layar (Mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            transform.position = touch.position;
        }
        // 2. Jika tidak ada sentuhan, gunakan posisi mouse (Untuk tes di Unity Editor PC)
        else
        {
            transform.position = Input.mousePosition;
        }
    }
}
