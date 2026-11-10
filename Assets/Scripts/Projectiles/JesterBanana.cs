using UnityEngine;

public class JesterBanana : ProjectileBase
{
    private int _damage;
    private int _pierceAmount;
    private float _lifeTime;

    public override void Fire(ProjectileData data)
    {
        _tf.localScale = data.Scale;
        _damage = data.Damage;
        _pierceAmount = data.PierceAmount;
        _lifeTime = data.LifeTime;

        Vector3 moveVector = data.FireDirection.normalized * data.Speed;

        _rb.AddForce(moveVector, ForceMode.Impulse);
    }
}
