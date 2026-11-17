using System.Collections;
using UnityEngine;

public class SpinAbility : UsableAbility
{
    [SerializeField] private float _spinSpeed;

    [Header("Melle Attack Settings")]
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private MeleeAttackData _attackData;

    [SerializeField] private Transform _placeholderTransform;
    [SerializeField] private Transform _spinCenter;

    public override IEnumerator UseAbility(Vector3 targetPosition)
    {
        float spinAngle = 0f;
        float spinTimer = 0f;

        StartCoroutine(_meleeAttack.PerformAttack(_attackData));

        while (spinTimer < _attackData.ActiveTime)
        {
            spinAngle = _spinSpeed * Time.deltaTime;
            
            _placeholderTransform.RotateAround(_spinCenter.position, Vector3.up, spinAngle);
            _attackData.AttackPositionOffset = _placeholderTransform.localPosition;
            _attackData.HitboxRotation = _placeholderTransform.localEulerAngles;
            
            spinTimer += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
