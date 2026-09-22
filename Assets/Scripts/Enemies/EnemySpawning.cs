using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField, Header("References")] private GameObject _enemyPrefab;
    [SerializeField] private Transform _SpawnCenterPoint;
    [SerializeField] private Transform _playerTF;

    [SerializeField, Header("Settings")] private float _minDistanceFromCenter;
    [SerializeField] private float _maxDistanceFromCenter;
    [SerializeField] private float _minPlayerDistance;
    [SerializeField] private float _spawnDelay;

    private float _spawnTimer;

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;
        while (!validPositionFound)
        {
            float randomDistance = Random.Range(_minDistanceFromCenter, _maxDistanceFromCenter);
            
            Vector2 randomPos = Random.insideUnitCircle.normalized * randomDistance;
            Vector3 offset = new Vector3(randomPos.x, 0f, randomPos.y);
            spawnPosition = _SpawnCenterPoint.position + offset;
            if (Vector3.Distance(spawnPosition, _playerTF.position) >= _minPlayerDistance)
            {
                validPositionFound = true;
            }
        }
        Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private void Start()
    {
        SpawnEnemy();
        _spawnTimer = _spawnDelay;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0f)
        {
            SpawnEnemy();
            _spawnTimer = _spawnDelay;
        }
    }
}
