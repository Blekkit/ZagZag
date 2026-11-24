using System.Collections;
using UnityEngine;

public class JesterBanana : ProjectileBase
{
    //[SerializeField] float _speedFalloff = 1f;
    //[SerializeField] float _stopRounding = 0.1f;

    public override void Fire()
    {
        Vector3 moveVector = new Vector3(_projectileData.FireDirection.x, 0, _projectileData.FireDirection.y);
        moveVector.Normalize();
        moveVector *= _projectileData.Speed;

        _rb.AddForce(moveVector, ForceMode.Impulse);

        base.Fire();
    }

    //private IEnumerator SpeedFalloff()
    //{
    //    while (_rb.linearVelocity.agnitude > _stopRounding)
    //    {
    //        _rb.linearVelocity.
    //        yield return null;
    //    }
    //}
}
