using UnityEngine;

public class SimpleProjectile : ProjectileBase
{
    public override void Fire()
    {
        Vector3 moveVector = new Vector3(_projectileData.FireDirection.x, 0, _projectileData.FireDirection.y);
        moveVector.Normalize();
        moveVector *= _projectileData.Speed;

        _rb.linearVelocity = moveVector;

        base.Fire();
    }
}
