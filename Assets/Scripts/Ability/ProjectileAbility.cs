using System.Collections;
using UnityEngine;

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
    [SerializeField] private MeleeAttack _meleeAttack;

    private Vector3 _targetPosition;

    public override IEnumerator UseAbility(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;

        //fire immediately
        if (!_hasMeleeHitbox || !_doesFireOnMeleeSwing)
        {
            StartCoroutine(FireProjectilesOverTime());
        }
        //fire on melee swing
        else
        {
            _meleeAttack.OnAttackSwing.AddListener(FireProjectiles);

            yield return StartCoroutine(_meleeAttack.PerformAttack(_meleeAttackData));

            if (_meleeAttack.OnAttackSwing != null)
            {
                _meleeAttack.OnAttackSwing.RemoveListener(FireProjectiles);
            }
        }

        yield return null;
    }

    private void FireProjectiles()
    {
        for (int i = 0; i < _rangedData.ProjectileAmount; i++)
        {
            Vector3 direction = _targetPosition - _shootOrigin.position;
            _rangedData.TargetDirection = direction;
            //direction.Normalize();
            //Vector2 dir = new Vector2(direction.x, direction.z);
            //_rangedData.ProjectileData.FireDirection = dir;
            StartCoroutine(_rangedAttack.PerformAttack(_rangedData));
        }
    }

    private IEnumerator FireProjectilesOverTime()
    {
        for (int i = 0; i < _rangedData.ProjectileAmount; i++)
        {
            Vector3 direction = _targetPosition - _shootOrigin.position;
            _rangedData.TargetDirection = direction;
            //direction.Normalize();
            //Vector2 dir = new Vector2(direction.x, direction.z);
            //_rangedData.ProjectileData.FireDirection = dir;
            //Debug.Log($"Firing projectile {i + 1}/{_rangedData.ProjectileAmount} towards {dir}");
            StartCoroutine(_rangedAttack.PerformAttack(_rangedData));
            yield return new WaitForSeconds(_rangedData.DelayBetweenShots);
        }
    }
}