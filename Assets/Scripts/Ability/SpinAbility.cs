using System.Collections;
using UnityEngine;

public class SpinAbility : UsableAbility
{
    [SerializeField] private float _spinRadius;
    [SerializeField] private float _spinSpeed;
    [SerializeField] private float _spinAmount;
    [SerializeField] private int _spinDamage;

    [Header("References")]
    [SerializeField] private Transform _hitboxTransform;
    [SerializeField] private Transform _spinCenter;
    [SerializeField] private AttackHitbox _spinHitbox;

    public override void UseAbility(Vector3 targetPosition)
    {
        StartCoroutine(DoSpinAttack());
    }

    private IEnumerator DoSpinAttack()
    {
        float spinAngle = 0f;
        float spinCounter = 0f;

        _hitboxTransform.gameObject.SetActive(true);

        while (spinCounter / 360 < _spinAmount)
        {
            spinAngle += _spinSpeed * Time.deltaTime;
            _hitboxTransform.RotateAround(_spinCenter.position, Vector3.up, spinAngle);
            spinCounter += spinAngle;
            yield return null;
        }

        _hitboxTransform.gameObject.SetActive(false);
        //yield return null;
    }

    private void Awake()
    {
        _hitboxTransform.localScale = new Vector3(_hitboxTransform.localScale.x, _hitboxTransform.localScale.y, _spinRadius);
        _spinHitbox.SetDamage(_spinDamage);
    }
}
