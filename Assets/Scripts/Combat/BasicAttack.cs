using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicAttack : MonoBehaviour
{
    [Header("Melee Settings")]
    [SerializeField] private bool _hasMeleeAttack;
    [SerializeField] private MeleeAttackData _meleeData;

    [Header("Reference")]
    [SerializeField] private MeleeAttack _meleeAttack;

    [Header("Ranged Settings")]
    [SerializeField] private bool _hasRangedAttack;
    [SerializeField] private RangedAttackData _rangedData;

    [Header("Reference")]
    [SerializeField] private RangedAttack _rangedAttack;

    [Header("Other References")]
    [SerializeField] private MousePosition _mousePosProvider;
    [SerializeField] private CharacterRotation _charRotation;
    [SerializeField] private Transform _torsoTransform;

    private bool _isAttacking = true;

    public void OnAttackingToggled(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isAttacking = !_isAttacking;
            if (_isAttacking)
                StartBasicAttacks();
        }
    }

    public void PauseBasicAttacks()
    {
        StopAllCoroutines();
        _meleeAttack.CancelAttack();
        _rangedAttack.CancelAttack();
        _isAttacking = false;
    }

    public void ResumeBasicAttacks()
    {
        _isAttacking = true;
        StartBasicAttacks();
    }

    public void StartBasicAttacks()
    {
        if (_hasMeleeAttack)
            StartCoroutine(MeleeAttackLoop());

        if (_hasRangedAttack)
            StartCoroutine(RangedAttackLoop());
    }

    private Vector3 GetMouseDirection()
    {
        Vector3 mouseDirection = _mousePosProvider.GetMouseDirection(_torsoTransform.position);
        _charRotation.RotateTorso(mouseDirection);
        return mouseDirection;
    }

    private IEnumerator MeleeAttackLoop()
    {
        while (_isAttacking)
        {
            yield return new WaitForSeconds(_meleeData.AttackCooldown);
            GetMouseDirection();
            yield return StartCoroutine(_meleeAttack.PerformAttack(_meleeData));
        }
    }

    private IEnumerator RangedAttackLoop()
    {
        while (_isAttacking)
        {
            yield return new WaitForSeconds(_rangedData.AttackCooldown);
            _rangedData.TargetDirection = GetMouseDirection();
            yield return StartCoroutine(_rangedAttack.PerformAttack(_rangedData));
        }
    }

    private void Awake()
    {
        if (_torsoTransform == null)
        {
            _torsoTransform = transform;
            Debug.LogWarning($"No character transform specified on basic attack of {gameObject.name}. Using base transform {_torsoTransform}");
        }
    }
}
