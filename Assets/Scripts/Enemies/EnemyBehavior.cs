using UnityEngine;

public class EnemyBehavior : PoolableObject
{
    //[SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _contactDamage = 1;
    [SerializeField] private Move _enemyMove;

    private Transform _playerTransform;

    public void SetPlayerTF(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

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

    //private void Awake()
    //{
    //    if (_playerTransform == null)
    //    {
    //        GameObject player = GameObject.FindGameObjectWithTag("Player");
    //        if (player != null)
    //        {
    //            _playerTransform = player.transform;
    //        }
    //    }
    //}

    private void Update()
    {
        Vector3 direction = GetPlayerDirection();
        Vector2 dir = new Vector2(direction.x, direction.z);
        _enemyMove.SetMoveInput(dir.normalized);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth pHealth))
        {
            if (pHealth.CanBeDamaged)
                pHealth.TakeDamage(_contactDamage);
        } 
    }
}
