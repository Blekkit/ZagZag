using UnityEngine;

public class GamePause : MonoBehaviour
{
    private float _baseTimeScale;
    private const float PAUSED_TIME_SCALE = 0f;

    private void Awake()
    {
        _baseTimeScale = Time.timeScale;
    }

    public void PauseGame()
    {
        Time.timeScale = PAUSED_TIME_SCALE;
    }

    public void UnpauseGame()
    {
        Time.timeScale = _baseTimeScale;
    }

    private void OnDestroy()
    {
        UnpauseGame();
    }
}
