using System.Collections;
using UnityEngine;

public class JesterUlt : UsableAbility
{
    [Header("Charge Settings")]
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _chargingTime;
    [SerializeField] private float _chargeMoveSpeed = 0.1f;

    [Header("Attack Settings")]
    [SerializeField] private float _windupTime;
    [SerializeField] private float _activeTime;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private EHitboxShape _hitboxShape = EHitboxShape.circle;
    [SerializeField] private Vector3 _hitboxPositionOffset = Vector3.zero;

    [Header("References")]
    [SerializeField] private MeleeAttack _attack;
    [SerializeField] private GameObject _hitboxIndicator;
    [SerializeField] private Transform _idicatorParent;
    [SerializeField] private Move _characterMovement;

    private MeleeAttackData _attackData;

    public override IEnumerator UseAbility(Vector3 targetPosition)
    {
        //IsUsingAbility = true;
        _isHoldingButton = true;

        float chargeTimer = 0f;
        _hitboxIndicator.SetActive(true);
        _characterMovement.SetTempSpeed(_chargeMoveSpeed);
        
        float currentRadius = _minRadius;
        float currentDamage = _minDamage;
        float chargeRatio = 0f;

        //charging loop
        while (_isHoldingButton)
        {
            chargeRatio = Mathf.Clamp01(chargeTimer / _chargingTime);
            currentDamage = Mathf.Lerp(_minDamage, _maxDamage, chargeRatio);
            currentRadius = Mathf.Lerp(_minRadius, _maxRadius, chargeRatio);
            _idicatorParent.localScale = new Vector3(currentRadius, 1, currentRadius);
            chargeTimer += Time.deltaTime;
            yield return null;
        }

        //attack
        _attackData.Damage = (int)currentDamage;
        _attackData.HitboxScale = new Vector2(currentRadius, currentRadius);
        _hitboxIndicator.SetActive(false);
        yield return StartCoroutine(_attack.PerformAttack(_attackData));

        //_hitboxIndicator.transform.localScale = new Vector3(1, 1, 1);
        _characterMovement.ResetSpeed();
        //IsUsingAbility = false;
    }

    private void Start()
    {
        _attackData = new MeleeAttackData();
        _attackData.WindupTime = _windupTime;
        _attackData.ActiveTime = _activeTime;
        _attackData.WinddownTime = _cooldownTime;
        _attackData.HitboxShape = _hitboxShape;
        _attackData.AttackPositionOffset = _hitboxPositionOffset;
    }
}