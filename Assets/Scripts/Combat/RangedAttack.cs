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
            GameObject projectileObject = _projectilePool.GetObjectFromPool(_shootOrigin, data.ProjectileTypeID);
            if (projectileObject.TryGetComponent<ProjectileBase>(out ProjectileBase projectile))
            {
                //handle projectile speed
                float speed = 0f;
                if (data.MinProjectileSpeed == data.MaxProjectileSpeed)
                {
                    speed = data.MinProjectileSpeed;
                }
                else
                {
                    speed = Random.Range(data.MinProjectileSpeed, data.MaxProjectileSpeed);
                }
                data.ProjectileData.Speed = speed;

                //handle fire direction
                Vector3 direction = data.TargetDirection;
                direction.Normalize();
                direction = Quaternion.Euler(0, Random.Range(-data.ProjectileSpreadAngle, data.ProjectileSpreadAngle), 0) * direction;
                Vector2 dir = new Vector2(direction.x, direction.z);
                data.ProjectileData.FireDirection = dir;

                projectile.Initialise(data.ProjectileData);

                //fire fire fire!
                projectile.Fire();
            }

            yield return new WaitForSeconds(data.DelayBetweenShots);
        }

        CanAttack = true;
    }
}
