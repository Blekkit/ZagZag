using UnityEngine;

public abstract class ProjectileBase : PoolableObject
{
    protected Rigidbody _rb;
    protected Transform _tf;

    public abstract void Fire(ProjectileData data);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _tf = GetComponent<Transform>();
    }
}
