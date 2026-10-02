using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] bool _isEnemy;
    [SerializeField] IntValue _playerScore;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isEnemy)
        {
            _playerScore.SetValue(_playerScore.Value + 10);
        }

        Destroy(gameObject);
    }
}
