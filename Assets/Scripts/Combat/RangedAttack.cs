using System.Collections;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] ObjectPool _projectilePool;
    [SerializeField] Transform _shootOrigin;

    public bool CanAttack { get; private set; } = true;

    public void CancelAttack()
    {
        StopAllCoroutines();
        CanAttack = true;
    }

    public IEnumerator PerformAttack(RangedAttackData data)
    {
        CanAttack = false;

        for (int i = 0; i < data.ProjectileAmount; i++)
        {
            GameObject projectileObject = _projectilePool.GetObjectFromPool(_shootOrigin);
            if (projectileObject.TryGetComponent<Projectile>(out Projectile projectile))
            {
                //setup projectile stats
                projectile.SetDamage(data.ProjectileDamage);
                projectile.SetPierceAmount(data.PierceAmount);
                projectile.SetScale(data.ProjectileScale);
                float speed = Random.Range(data.MinProjectileSpeed, data.MaxProjectileSpeed);
                
                //handle fire direction
                Vector3 direction = data.TargetDirection;
                direction.Normalize();
                direction = Quaternion.Euler(0, Random.Range(-data.ProjectileSpreadAngle, data.ProjectileSpreadAngle), 0) * direction;
                Vector2 dir = new Vector2(direction.x, direction.z);
                
                //fire fire fire!
                projectile.Fire(dir, speed);
            }

            yield return new WaitForSeconds(data.DelayBetweenShots);
        }

        CanAttack = true;
    }
}
