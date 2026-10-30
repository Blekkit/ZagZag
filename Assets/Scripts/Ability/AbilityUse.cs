using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Collections;

public class AbilityUse : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MousePosition _mousePositionProvider;
    [SerializeField] private BasicAttack _basicAttack;

    [Header("Abilities")]
    [SerializeField] private List<UsableAbility> _abilities;

    [SerializeField] private FloatValue _ability1Timer;
    [SerializeField] private FloatValue _ability2Timer;
    [SerializeField] private FloatValue _ability3Timer;

    public void OnAbility1Use(InputAction.CallbackContext context)
    {
        if (_abilities == null || _abilities.Count < 1)
        {
            Debug.Log("ability invalid");
            return;
        }

        if (_ability1Timer.Value > 0 || _abilities[0]._isUsingAbility)
        {
            Debug.Log("ability on cooldown or being used");
            return;
        }

        if (context.started)
        {
            StartCoroutine(UseAbility(_abilities[0], _ability1Timer));
        }
        else if (context.canceled)
        {
            _abilities[0].OnButtonReleased();
        }
    }

    public void OnAbility2Use(InputAction.CallbackContext context)
    {
        if (_abilities == null || _abilities.Count < 2)
        {
            Debug.Log("ability invalid");
            return;
        }

        if (_ability2Timer.Value > 0 || _abilities[1]._isUsingAbility)
        {
            Debug.Log("ability on cooldown or being used");
            return;
        }

        if (context.started)
        {
            StartCoroutine(UseAbility(_abilities[1], _ability2Timer));
        }
        else if (context.canceled)
        {
            _abilities[1].OnButtonReleased();
        }
    }

    public void OnAbility3Use(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _abilities[2].OnButtonReleased();
            return;
        }

        if (_abilities == null || _abilities.Count < 3)
        {
            Debug.Log("ability invalid");
            return;
        }
        
        if (_ability3Timer.Value > 0 || _abilities[2]._isUsingAbility)
        {
            Debug.Log($"ability on cooldown or being used on {this.gameObject.name}. {_abilities[2]._isUsingAbility}");
            return;
        }
        
        if (context.started)
        {
            StartCoroutine(UseAbility(_abilities[2], _ability3Timer));
        }
    }

    private IEnumerator UseAbility(UsableAbility ability, FloatValue abilityCooldownTimer)
    {
        _basicAttack.PauseBasicAttacks();
        yield return StartCoroutine(ability.UseAbility(_mousePositionProvider.GetMousePosition()));
        _basicAttack.ResumeBasicAttacks();
        yield return StartCoroutine(StartCooldown(abilityCooldownTimer, ability.GetCooldown()));
    }

    private IEnumerator StartCooldown(FloatValue cooldownTimer, int cooldown)
    {
        cooldownTimer.SetValue(cooldown);

        while (cooldownTimer.Value > 0)
        {
            cooldownTimer.SetValue(Mathf.Max(0, cooldownTimer.Value - Time.deltaTime));
            yield return null;
        }

        cooldownTimer.SetValue(0);
    }

    private void Awake()
    {
        _ability1Timer.SetMaxValue(_abilities[0].GetCooldown());
        _ability2Timer.SetMaxValue(_abilities[1].GetCooldown());
        _ability3Timer.SetMaxValue(_abilities[2].GetCooldown());

        _ability1Timer.SetValue(0);
        _ability2Timer.SetValue(0);
        _ability3Timer.SetValue(0);
    }
}
