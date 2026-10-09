using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileFiring : MonoBehaviour, Attacking
{
    [Header("References")]
    //[SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private ObjectPool _projectilePool;
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private Transform _projectileParent;

    [Header("Settings")]
    [SerializeField] private float _projectileSpeed;

    public void PerformAttack(Vector2 direction)
    {
        GameObject projectileObject = _projectilePool.GetObjectFromPool(_shootOrigin);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Fire(direction, _projectileSpeed); 
    }

    public void PerformAttack(Vector3 direction, int bonusDamage, float spreadAngle, float speedModifier)
    {
        direction = Quaternion.Euler(0, Random.Range(-spreadAngle, spreadAngle), 0) * direction;
        Vector2 dir = new Vector2(direction.x, direction.z); 

        GameObject projectileObject = _projectilePool.GetObjectFromPool(_shootOrigin);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.AddDamage(bonusDamage);
        //wat
        projectile.Fire(dir, _projectileSpeed * speedModifier);
    }
}
