using System.Collections;
using UnityEngine;

public class Projectile : PoolableObject
{
    public int OwnerID;

    [SerializeField] private int _damage = 0;
    [SerializeField] private float _projectileBaseSpeed = 1;
    [SerializeField] private float _lifeTime = 5f;

    public int PierceAmount { get; private set; } = 0;

    private float _lifeTimer = 0;
    private Rigidbody _rb;

    #region setters
    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    public void AddDamage(int damage)
    {
        _damage += damage;
    }

    public void SetPierceAmount(int amount)
    {
        PierceAmount = amount;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
    #endregion

    public void Fire(Vector2 direction, float fireSpeed = 1)
    {
        Vector3 fireDirection = new Vector3(direction.x, 0, direction.y);
        _rb.linearVelocity = fireDirection * fireSpeed * _projectileBaseSpeed;
        StartCoroutine(StartLifeTimer());
    }

    private IEnumerator StartLifeTimer()
    {
        _lifeTimer = 0;

        while (_lifeTimer < _lifeTime)
        {
            _lifeTimer += Time.deltaTime;
            yield return null;
        }
        
        Destroy();        
    }

    private void Destroy()
    {
        StopAllCoroutines();
        ReturnToPool();
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(_damage);

            if (PierceAmount <= 0)
            {
                Destroy();
            }
            else
            {
                PierceAmount--;
            }
        }
        else
        {
            Destroy();
        }
    }
}
