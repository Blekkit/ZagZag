using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _knockbackForce = 1f;

    [Header("References")]
    [SerializeField] private Transform _knockbackOriginPoint;

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedGameObject = other.gameObject;

        DealDamage(collidedGameObject);

        ApplyKnockback(collidedGameObject);
    }

    private void DealDamage(GameObject target)
    {
        if (target.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(_damage);
        }
    }

    private void ApplyKnockback(GameObject target)
    {
        if (target.TryGetComponent<Rigidbody>(out Rigidbody targetRigidbody))
        {
            Vector3 forceDirection = target.transform.position - _knockbackOriginPoint.position;
            forceDirection.Normalize();
            targetRigidbody.AddForce(forceDirection * _knockbackForce, ForceMode.Impulse);
        }
    }
}
