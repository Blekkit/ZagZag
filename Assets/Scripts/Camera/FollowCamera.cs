using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _followTransform;

    [Header("Settings")]
    [SerializeField, Range(0f, 1f)] private float _followPercentage = 0.5f;

    private Transform _target;
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target == null || _followTransform == null)
            return;

        Vector3 desiredPosition = Vector3.Lerp(_followTransform.position, _target.position, _followPercentage);
        desiredPosition.y = 0; 
        
        _followTransform.position = desiredPosition;
    }
}
