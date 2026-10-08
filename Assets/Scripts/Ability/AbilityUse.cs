using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class AbilityUse : MonoBehaviour
{
    [SerializeField] private List<UsableAbility> _abilities;
    [SerializeField] private MousePosition _mousePositionProvider;

    [SerializeField] private FloatValue _ability1Timer;
    [SerializeField] private FloatValue _ability2Timer;
    [SerializeField] private FloatValue _ability3Timer;

    public void OnAbility1Use(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_ability1Timer.Value <= 0 && _abilities != null && _abilities.Count >= 1)
            {
                _abilities[0].UseAbility(_mousePositionProvider.GetMousePosition());
                _ability1Timer.SetValue(_abilities[0].GetCooldown());
            }
        }
    }

    public void OnAbility2Use(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_ability2Timer.Value <= 0 && _abilities != null && _abilities.Count >= 2)
            {
                _abilities[1].UseAbility(_mousePositionProvider.GetMousePosition());
                _ability2Timer.SetValue(_abilities[1].GetCooldown());
            }
        }
    }

    public void OnAbility3Use(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_ability3Timer.Value <= 0 && _abilities != null && _abilities.Count >= 3)
            {
                _abilities[2].UseAbility(_mousePositionProvider.GetMousePosition());
                _ability3Timer.SetValue(_abilities[2].GetCooldown());
            }
        }
    }

    private void Awake()
    {
        _ability1Timer.SetMaxValue(_abilities[0].GetCooldown());
        _ability2Timer.SetMaxValue(_abilities[1].GetCooldown());
        _ability3Timer.SetMaxValue(_abilities[2].GetCooldown());    
    }

    private void Update()
    {
        if (_ability1Timer.Value > 0)
        {
            _ability1Timer.SetValue(Mathf.Max(0, _ability1Timer.Value - Time.deltaTime));
        }
        if (_ability2Timer.Value > 0)
        {
            _ability2Timer.SetValue(Mathf.Max(0, _ability2Timer.Value - Time.deltaTime));
        }
        if (_ability3Timer.Value > 0)
        {
            _ability3Timer.SetValue(Mathf.Max(0, _ability3Timer.Value - Time.deltaTime));
        }
    }
}
