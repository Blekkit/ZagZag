using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _attackSpeed = 1f;

    [Header("References")]
    [SerializeField] private CharacterRotation _playerRotation;
    [SerializeField] private MousePosition _mousePosProvider;

    Transform _characterTransform;
    private Attacking _characterAttack;
    private bool _isAttacking = true;
    private float _attackDelay;
    private float _attackTimer;

    public void OnAttackingToggled(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isAttacking = !_isAttacking;
        }
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mouseDirection = _mousePosProvider.GetMouseDirection(_characterTransform.position);
        Vector2 fireDirection = new Vector2(mouseDirection.x, mouseDirection.z);

        _playerRotation.Rotate(fireDirection);

        return fireDirection;
    }

    private void Attack()
    {
        Vector2 attackDirection = GetMouseDirection();
        _characterAttack.PerformAttack(attackDirection);
    }

    private void Start()
    {
        _characterTransform = transform;
        _characterAttack = gameObject.GetComponent<Attacking>();

        if (_attackSpeed == 0f)
            _attackDelay = 1;
        else
            _attackDelay = 1 / _attackSpeed;
        _attackTimer = _attackDelay;
    }

    private void Update()
    {
        _attackTimer -= Time.deltaTime;

        if (_attackTimer <= 0)
        {
            if (_isAttacking)
            {
                Attack();
            }

            _attackTimer += _attackDelay;
        }
    }
}
