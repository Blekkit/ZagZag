using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFiring : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _playerShootOrigin;
    [SerializeField] private Transform _projectileParent;
    [SerializeField] private CharacterRotation _playerRotation;

    [Header("Settings")]
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _projectileSpeed;

    private bool _isFiring = false;
    private float _attackTimer;
    private float _attackDelay;

    public void OnFiringToggled(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isFiring = !_isFiring;
        }
    }

    private void Fire()
    {
        Projectile projectile = Instantiate(_projectilePrefab, _playerShootOrigin.position, Quaternion.identity, _projectileParent);
        projectile.Fire(GetMousePosition(), _projectileSpeed);
    }

    private Vector2 GetMousePosition()
    {
        Vector2 fireDirection = new Vector2(1f, 1f);
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPos = hit.point;
            Vector3 mouseDirection = hitPos - _playerShootOrigin.position;
            mouseDirection.Normalize();
            fireDirection.x = mouseDirection.x;
            fireDirection.y = mouseDirection.z;
            _playerRotation.Rotate(hitPos);
        }

        return fireDirection;
    }

    private void Start()
    {
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
            if (_isFiring)
            {
                Fire();
            }

            _attackTimer += _attackDelay;
        }
    }
}
