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

    private void Start()
    {
        _tf = transform;
    }
}
