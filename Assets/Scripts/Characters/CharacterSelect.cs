using UnityEngine;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private List<GameObject> _players;
    [SerializeField] private GameObject _selectMenu;
    [SerializeField] private EnemySpawning _enemySpawner;
    [SerializeField] private GamePause _pauseManager;

    private Transform _playerTF;
    private BasicAttack _playerBasicAttack;

    public void SelectPlayer(int id)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (i != id)
            {
                Destroy(_players[i]);
            }
            else
            {
                _playerTF = _players[i].transform;
                _players[i].TryGetComponent<BasicAttack>(out _playerBasicAttack);
            }
        }

        _pauseManager.UnpauseGame();

        _selectMenu.SetActive(false);

        if (_playerTF != null)
        {
            _enemySpawner.SetPlayerTransform(_playerTF);
            _enemySpawner.StartGame();
        }

        if (_playerBasicAttack != null)
            _playerBasicAttack.StartBasicAttacks();
    }

    private void Start()
    {
        _pauseManager.PauseGame();
    }
}

//todo: reference manager?