using UnityEngine;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private List<GameObject> _players;
    [SerializeField] private GameObject _selectMenu;
    [SerializeField] private EnemySpawning _enemySpawner;
    [SerializeField] private GamePause _pauseManager;

    private Transform _playerTF;

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
            }
        }

        _selectMenu.SetActive(false);

        _pauseManager.UnpauseGame();

        if (_playerTF != null)
        {
            _enemySpawner.SetPlayerTransform(_playerTF);
            _enemySpawner.StartGame();
        }
    }

    private void Awake()
    {
        _pauseManager.PauseGame();
    }
}
