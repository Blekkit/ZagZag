using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] IntValue _playerScore;
    [SerializeField] IntValue _playerHealth;
    [SerializeField] Slider _healthSlider;

    private void Awake()
    {
        _playerScore.SetValue(0);
        UpdateScore();
        _playerScore.OnValueChanged.AddListener(OnScoreChanged);

        UpdateHealth();
        _playerHealth.OnValueChanged.AddListener(OnHealthChanged);
    }

    public void OnScoreChanged()
    {
        UpdateScore();
    }

    public void OnHealthChanged()
    {
        UpdateHealth();
    }

    private void UpdateScore()
    {
        _scoreText.text = $"Score: {_playerScore.Value}";
    }

    private void UpdateHealth()
    {
        _healthSlider.value = _playerHealth.Value;
    }

    private void OnDestroy()
    {
        if (_playerScore != null)
        {
            _playerScore.OnValueChanged.RemoveListener(OnScoreChanged);
        }

        if (_playerHealth != null)
        {
            _playerHealth.OnValueChanged.RemoveListener(OnScoreChanged);
        }
    }
}
