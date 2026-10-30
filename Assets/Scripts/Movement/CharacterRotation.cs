using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    private Transform _tf;

    public void Rotate(float rotationYAngle)
    {
        _tf.Rotate(0, rotationYAngle, 0);
    }

    public void Rotate(Vector3 lookDirection)
    {
        //Vector3 lookVector = new Vector3(lookDirection.x, 0, lookDirection.y);
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void LookAt(Vector3 lookAtPosition)
    {
        lookAtPosition.y = 1;
        _tf.LookAt(lookAtPosition);
    }

    private void Start()
    {
        _tf = transform;
    }
}
