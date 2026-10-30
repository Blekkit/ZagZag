using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileAbility : UsableAbility
{
    [Header("Shot Settings")]
    [SerializeField] private RangedAttackData _rangedData;

    [Header("Melee Settings")]
    [SerializeField] private bool _hasMeleeHitbox = false;
    [SerializeField] private bool _doesFireOnMeleeSwing = true;
    [SerializeField] private MeleeAttackData _meleeAttackData;

    [Header("References")]
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private RangedAttack _rangedAttack;
    //[SerializeField] private ProjectileFiring _firing;
    [SerializeField] private MeleeAttack _meleeAttack;

    private Vector3 _targetPosition;

    public override IEnumerator UseAbility(Vector3 targetPosition)
    {
        _isUsingAbility = true;
        _targetPosition = targetPosition;

        //melee windup
        if (!_hasMeleeHitbox)
        {
            StartCoroutine(FireProjectilesOverTime());
        }
        else
        {
            if (!_doesFireOnMeleeSwing)
            {
                StartCoroutine(FireProjectilesOverTime());
            }
            else
            {
                _meleeAttack.OnAttackSwing.AddListener(FireProjectiles);
            }

            yield return StartCoroutine(_meleeAttack.PerformAttack(_meleeAttackData));

            if (_meleeAttack.OnAttackSwing != null)
            {
                _meleeAttack.OnAttackSwing.RemoveListener(FireProjectiles);
            }
        }

        _isUsingAbility = false;
        yield return null;
    }

    private void FireProjectiles()
    {
        for (int i = 0; i < _rangedData.ProjectileAmount; i++)
        {
            Vector3 direction = _targetPosition - _shootOrigin.position;
            _rangedData.TargetDirection = direction.normalized;
            StartCoroutine(_rangedAttack.PerformAttack(_rangedData));
        }
    }

    private IEnumerator FireProjectilesOverTime()
    {
        for (int i = 0; i < _rangedData.ProjectileAmount; i++)
        {
            Vector3 direction = _targetPosition - _shootOrigin.position;
            direction.Normalize();
            StartCoroutine(_rangedAttack.PerformAttack(_rangedData));
            yield return new WaitForSeconds(_rangedData.DelayBetweenShots);
        }
    }
}