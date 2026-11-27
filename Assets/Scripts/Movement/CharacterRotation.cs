using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [SerializeField] private Transform _torsoTransform;
    [SerializeField] private Transform _legsTransform;
    [SerializeField] private Transform _weaponTransform;

    /// <summary>
    /// Rotates a body part by an angle around the Y-axis.
    /// </summary>
    /// <param name="rotationYAngle">the y rotation angle</param>
    /// <param name="bodyPart">the body part to rotate</param>
    public void RotateBodyPart(float rotationYAngle, EPlayerBodyPart bodyPart)
    {
        Transform targetTransform = GetTransformByBodyPart(bodyPart);
        targetTransform.Rotate(0, rotationYAngle, 0);
    }

    /// <summary>
    /// rotates a body part to look in a direction
    /// </summary>
    /// <param name="lookDirection">the direction to look in</param>
    /// <param name="bodyPart">the body part to rotate</param>
    public void RotateBodyPart(Vector3 lookDirection, EPlayerBodyPart bodyPart)
    {
        Transform targetTransform = GetTransformByBodyPart(bodyPart);
        lookDirection.y = 0;
        targetTransform.rotation = Quaternion.LookRotation(lookDirection);
    }

    public Vector3 GetBodyPartForward(EPlayerBodyPart bodyPart)
    {
        Transform targetTransform = GetTransformByBodyPart(bodyPart);
        Vector3 forward = targetTransform.forward;
        return forward;
    }

    //public void LookAt(Vector3 lookAtPosition)
    //{
    //    lookAtPosition.y = 1;
    //    _torsoTransform.LookAt(lookAtPosition);
    //}

    /// <summary>
    /// gets the transform of the specified body part
    /// </summary>
    /// <param name="bodyPart">the body part to get the transform of</param>
    /// <returns>the transform of the specified body part</returns>
    private Transform GetTransformByBodyPart(EPlayerBodyPart bodyPart)
    {
        return bodyPart switch
        {
            EPlayerBodyPart.legs => _legsTransform,
            EPlayerBodyPart.torso => _torsoTransform,
            EPlayerBodyPart.weapon => _weaponTransform,
            _ => _legsTransform,
        };
    }
}
