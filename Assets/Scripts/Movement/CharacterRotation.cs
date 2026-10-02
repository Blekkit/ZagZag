using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    private Transform _tf;

    public void Rotate(float rotationYAngle)
    {
        _tf.Rotate(0, rotationYAngle, 0);
    }

    public void Rotate(Vector3 lookAtPosition)
    {
        lookAtPosition.y = 1;
        _tf.LookAt(lookAtPosition);
    }

    public void Rotate(Vector2 lookDirection)
    {
        Vector3 lookVector = new Vector3(lookDirection.x, 0, lookDirection.y);
        transform.rotation = Quaternion.LookRotation(lookVector);
    }

    private void Start()
    {
        _tf = transform;
    }
}
