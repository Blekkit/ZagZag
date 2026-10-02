using UnityEngine;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private List<GameObject> _players;
    [SerializeField] private GameObject _selectMenu;
    [SerializeField] private EnemySpawning _enemySpawner;

    private float _timeScale;
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

        Time.timeScale = _timeScale;

        if (_playerTF != null)
            _enemySpawner.SetPlayerTransform(_playerTF);
    }

    private void Awake()
    {
        _timeScale = Time.timeScale;
        Time.timeScale = 0f;
    }
}
