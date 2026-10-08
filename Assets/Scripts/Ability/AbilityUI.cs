using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    [SerializeField] Slider _ability1Cooldown;
    [SerializeField] Slider _ability2Cooldown;
    [SerializeField] Slider _ability3Cooldown;
    [SerializeField] FloatValue _ability1Timer;
    [SerializeField] FloatValue _ability2Timer;
    [SerializeField] FloatValue _ability3Timer;

    public void OnAbility1Cooldown()
    {
        _ability1Cooldown.value = _ability1Timer.Value / _ability1Timer.MaxValue;
    }

    public void OnAbility2Cooldown()
    {
        _ability2Cooldown.value = _ability2Timer.Value / _ability2Timer.MaxValue;
    }

    public void OnAbility3Cooldown()
    {
        _ability3Cooldown.value = _ability3Timer.Value / _ability3Timer.MaxValue;
    }

    private void Awake()
    {
        _ability1Timer.OnValueChanged.AddListener(OnAbility1Cooldown);
        _ability2Timer.OnValueChanged.AddListener(OnAbility2Cooldown);
        _ability3Timer.OnValueChanged.AddListener(OnAbility3Cooldown);
    }

    private void OnDestroy()
    {
        _ability1Timer.OnValueChanged.RemoveListener(OnAbility1Cooldown);
        _ability2Timer.OnValueChanged.RemoveListener(OnAbility2Cooldown);
        _ability3Timer.OnValueChanged.RemoveListener(OnAbility3Cooldown);
    }
}
