using UnityEngine;
using UnityEngine.EventSystems;

public class TouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Vector2 touchDelta;

    private bool isPressed;
    private int pointerId;
    private Vector2 previousPosition;

    void Update()
    {
        if (isPressed)
        {
            // Cek jika input berasal dari layar sentuh (Mobile)
            if (pointerId >= 0 && pointerId < Input.touches.Length)
            {
                Vector2 currentPosition = Input.touches[pointerId].position;
                touchDelta = currentPosition - previousPosition;
                previousPosition = currentPosition;
            }
            // Fallback jika dites di Unity Editor menggunakan Mouse
            else
            {
                Vector2 currentPosition = Input.mousePosition;
                touchDelta = currentPosition - previousPosition;
                previousPosition = currentPosition;
            }
        }
        else
        {
            touchDelta = Vector2.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        pointerId = eventData.pointerId;
        previousPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        touchDelta = Vector2.zero;
    }
}