using UnityEngine;
using System.Collections.Generic;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField] private List<GameObject> _players;
    [SerializeField] private EnemySpawning _enemySpawner;
    [SerializeField] private FollowCamera _followCamera;
    [SerializeField] private MenuManager _menuManager;
    //[SerializeField] private GameObject _selectMenu;
    //[SerializeField] private GamePause _pauseManager;

    private Transform _playerTF;
    private BasicAttack _playerBasicAttack;

    public void SelectPlayer(int id)
    {
        Move playerMovement = null;

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
                _players[i].TryGetComponent<Move>(out playerMovement);
            }
        }

        _menuManager.CloseMenus();
        //_pauseManager.UnpauseGame();
        //_selectMenu.SetActive(false);

        if (_playerTF != null)
        {
            _enemySpawner.SetPlayerTransform(_playerTF);
            _enemySpawner.StartGame();
            _followCamera.SetTarget(_playerTF);
        }

        if (_playerBasicAttack != null)
            _playerBasicAttack.StartBasicAttacks();

        if (playerMovement != null && playerMovement.IsUsingScateMovement)
            playerMovement.StartScateLoop();
    }

    //private void Start()
    //{
    //    _pauseManager.PauseGame();
    //}
}

//todo: reference manager?