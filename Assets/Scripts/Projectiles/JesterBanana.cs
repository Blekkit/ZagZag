using System.Collections;
using UnityEngine;

public class JesterBanana : ProjectileBase
{
    [SerializeField] float _inactiveTime = 0.2f;
    [SerializeField] Collider _collider;

    public override void Fire()
    {
        _collider.enabled = false;
        Vector3 moveVector = new Vector3(_projectileData.FireDirection.x, 0, _projectileData.FireDirection.y);
        moveVector.Normalize();
        moveVector *= _projectileData.Speed;

        _rb.AddForce(moveVector, ForceMode.Impulse);

        StartCoroutine(TurnActive());

        base.Fire();
    }

    private IEnumerator TurnActive()
    {
        yield return new WaitForSeconds(_inactiveTime);
        _collider.enabled = true;
    }
}
