using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private IntValue _playerHealth;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private GamePause _pauseManager;

    [Header("Settings")]
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private float _invulnTime;

    public bool CanBeDamaged { get; private set; } = true;

    public void TakeDamage(int amount)
    {
        _playerHealth.SetValue(_playerHealth.Value - amount);

        if (_playerHealth.Value <= 0)
        {
            _gameOverCanvas.gameObject.SetActive(true);
            _pauseManager.PauseGame();
        }
        else
            StartCoroutine(DoInvulnTime());
    }

    private IEnumerator DoInvulnTime()
    {
        CanBeDamaged = false;
        yield return new WaitForSeconds(_invulnTime);
        CanBeDamaged = true;
    }

    private void Awake()
    {
        _playerHealth.SetValue(_maxHealth);
    }
}
