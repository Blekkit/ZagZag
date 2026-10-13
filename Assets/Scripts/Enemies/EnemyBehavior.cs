using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _contactDamage = 1;

    private Transform _playerTransform;

    private Vector3 GetPlayerDirection()
    {
        Vector3 direction = Vector3.zero;

        if (_playerTransform != null)
        {
            direction = (_playerTransform.position - transform.position).normalized;
            direction.y = 0;
        }
        return direction;
    }

    private void Awake()
    {
        if (_playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                _playerTransform = player.transform;
            }
        }
    }

    private void Update()
    {
        Vector3 direction = GetPlayerDirection();
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth pHealth))
        {
            if (pHealth.CanBeDamaged)
                pHealth.TakeDamage(_contactDamage);
        }
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //Debug.Log($"Enemy collided with {collision.gameObject.name}");
    //    collision.gameObject.GetComponent<PlayerHealth>()?.TakeDamage(1);
    //}
}
