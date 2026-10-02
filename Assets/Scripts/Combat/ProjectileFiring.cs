using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileFiring : MonoBehaviour, Attacking
{
    [Header("References")]
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private Transform _projectileParent;

    [Header("Settings")]
    [SerializeField] private float _projectileSpeed;

    public void PerformAttack(Vector2 direction)
    {
        Projectile projectile = Instantiate(_projectilePrefab, _shootOrigin.position, Quaternion.identity, _projectileParent);
        projectile.Fire(direction, _projectileSpeed); 
    }
}
