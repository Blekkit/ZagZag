using System.Collections;
using UnityEngine;

public class JesterUlt : UsableAbility
{
    [Header("Charge Settings")]
    [SerializeField] private float _minDmage;
    [SerializeField] private float _maxDamage;
    [SerializeField] private float _minRadius;
    [SerializeField] private float _maxRadius;
    [SerializeField] private float _chargingTime;
    [SerializeField] private float _chargeMoveSpeed = 0.1f;

    [Header("Attack Settings")]
    [SerializeField] private float _windupTime;
    [SerializeField] private float _activeTime;
    [SerializeField] private float _cooldownTime;

    [Header("References")]
    [SerializeField] private AttackHitbox _hitbox;
    [SerializeField] private Transform _attackParent;
    [SerializeField] private GameObject _indicator;
    [SerializeField] private Move _characterMovement;

    public override IEnumerator UseAbility(Vector3 targetPosition)
    {
        _isUsingAbility = true;
        _isHoldingButton = true;

        float chargeTimer = 0f;
        _indicator.SetActive(true);
        _characterMovement.SetTempSpeed(_chargeMoveSpeed);
        //charging loop
        while (_isHoldingButton)
        {
            float chargeRatio = Mathf.Clamp01(chargeTimer / _chargingTime);
            float currentDamage = Mathf.Lerp(_minDmage, _maxDamage, chargeRatio);
            float currentRadius = Mathf.Lerp(_minRadius, _maxRadius, chargeRatio);
            _hitbox.SetDamage((int)currentDamage);
            _attackParent.localScale = new Vector3(currentRadius, 1, currentRadius);
            chargeTimer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(_windupTime);
        _indicator.SetActive(false);
        _hitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(_activeTime);
        _hitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(_cooldownTime);
        _attackParent.localScale = new Vector3(_minRadius, 1, _minRadius);
        _isUsingAbility = false;
        _characterMovement.ResetSpeed();
    }
}
