using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int OwnerID;

    [SerializeField] private int _damage = 0;
    [SerializeField] private float _projectileSpeedModifier = 1;
    [SerializeField] private float _lifeTime = 5f;
    
    private float _lifeTimer = 0;
    private Rigidbody _rb;

    public void Fire(Vector2 direction, float projectileSpeed = 1)
    {
        Vector3 fireDirection = new Vector3(direction.x, 0, direction.y);
        _rb.linearVelocity = fireDirection * projectileSpeed * _projectileSpeedModifier;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer > _lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(_damage);
        }

        Destroy(this.gameObject);
    }
}
