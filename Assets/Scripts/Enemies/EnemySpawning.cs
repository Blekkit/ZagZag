using UnityEngine;
using System.Collections.Generic;

public class EnemySpawning : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _SpawnCenterPoint;
    [SerializeField] private ObjectPool _enemyPool;
    [SerializeField] private List<Transform> _fixedSpawnPositions;

    [Header("Settings")]
    [SerializeField] private bool _doesUseFixedPositions;
    [SerializeField] private float _spawnDelay;

    [Header("Fixed Spawning")]
    [SerializeField] private bool _isUsingRandomSpawnOrder;

    [Header("Random Spawning")]
    [SerializeField] private float _minDistanceFromCenter;
    [SerializeField] private float _maxDistanceFromCenter;
    [SerializeField] private float _minPlayerDistance;

    private Transform _playerTF;
    private float _spawnTimer;
    private bool _hasGameStarted = false;
    private int _spawnPositionIndexCounter = 0;

    public void StartGame()
    {
        _hasGameStarted = true;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTF = playerTransform;
    }

    private void SpawnEnemyRandomly()
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

    private void SpawnEnemyFixed()
    {
        if (_fixedSpawnPositions == null || _fixedSpawnPositions.Count <= 0)
            return;

        if (_isUsingRandomSpawnOrder)
        {
            _enemyPool.GetObjectFromPool(_fixedSpawnPositions[Random.Range(0, _fixedSpawnPositions.Count)]);
        }
        else
        {
            _enemyPool.GetObjectFromPool(_fixedSpawnPositions[_spawnPositionIndexCounter]);
            _spawnPositionIndexCounter = _fixedSpawnPositions.Count % (_spawnPositionIndexCounter + 1);
        }
    }

    private void Start()
    {
        //SpawnEnemy();
        _spawnTimer = _spawnDelay;
    }

    private void Update()
    {
        if (_hasGameStarted)
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0f)
            {
                if (_doesUseFixedPositions)
                    SpawnEnemyFixed();
                else
                    SpawnEnemyRandomly();
                _spawnTimer = _spawnDelay;
            }
        }
    }
}
