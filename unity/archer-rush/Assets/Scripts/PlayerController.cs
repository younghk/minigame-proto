using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float sensitivity = 0.04f;
    public float minX = -4f;
    public float maxX = 4f;

    Vector2 lastPointer;
    bool dragging;

    void Update()
    {
        var pointer = Pointer.current;
        if (pointer == null) return;

        Vector2 current = pointer.position.ReadValue();

        if (pointer.press.wasPressedThisFrame)
        {
            lastPointer = current;
            dragging = true;
        }
        if (pointer.press.wasReleasedThisFrame)
        {
            dragging = false;
        }

        if (dragging && pointer.press.isPressed)
        {
            Vector2 delta = current - lastPointer;
            lastPointer = current;

            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x + delta.x * sensitivity, minX, maxX);
            transform.position = pos;
        }
    }
}
