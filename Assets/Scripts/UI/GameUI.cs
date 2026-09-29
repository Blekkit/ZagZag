using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] IntValue _playerScore;

    private void Awake()
    {
        _playerScore.SetValue(0);
        UpdateScore();
        _playerScore.OnValueChanged.AddListener(OnScoreChanged);
    }

    public void OnScoreChanged()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        _scoreText.text = $"Score: {_playerScore.Value}";
    }

    private void OnDestroy()
    {
        if (_playerScore != null)
        {
            _playerScore.OnValueChanged.RemoveListener(OnScoreChanged);
        }
    }
}
