using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private Transform _torsoTransform;
    [SerializeField] private Transform _legsTransform;

    public void RotateTorso(float rotationYAngle)
    {
        _torsoTransform.Rotate(0, rotationYAngle, 0);
    }

    public void RotateTorso(Vector3 lookDirection)
    {
        //Vector3 lookVector = new Vector3(lookDirection.x, 0, lookDirection.y);
        lookDirection.y = 0;
        _torsoTransform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void RotateLegs(float rotationYAngle)
    {
        _legsTransform.Rotate(0, rotationYAngle, 0);
    }

    public void LookAt(Vector3 lookAtPosition)
    {
        lookAtPosition.y = 1;
        _torsoTransform.LookAt(lookAtPosition);
    }

    private void Start()
    {
        //_torsoTransform = transform;
    }
}
