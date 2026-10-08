using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private IntValue _playerHealth;
    [SerializeField] private Canvas _gameOverCanvas;
    [SerializeField] private GamePause _pauseManager;

    [Header("Settings")]
    [SerializeField] private int _maxHealth = 10;

    public void TakeDamage(int amount)
    {
        _playerHealth.SetValue(_playerHealth.Value - amount);

        if (_playerHealth.Value <= 0)
        {
            _gameOverCanvas.gameObject.SetActive(true);
            _pauseManager.PauseGame();
        }
    }

    private void Awake()
    {
        _playerHealth.SetValue(_maxHealth);
    }
}
