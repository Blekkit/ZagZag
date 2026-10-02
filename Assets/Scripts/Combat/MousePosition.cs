using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    public Vector3 GetMousePosition()
    {
        Vector3 hitPos = Vector3.zero;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out hit))
        {
            hitPos = hit.point;
        }

        return hitPos;
    }

    public Vector3 GetMouseDirection(Vector3 originPosition)
    {
        Vector3 mouseDirection = GetMousePosition() - originPosition;
        mouseDirection.y = 1f;
        mouseDirection.Normalize();
        return mouseDirection;
    }
}
