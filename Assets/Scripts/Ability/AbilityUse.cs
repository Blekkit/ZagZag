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

    private const int ZERO = 0;
    private const int ABILITY_1_ID = 0;
    private const int ABILITY_2_ID = 1;
    private const int ABILITY_3_ID = 2;

    private bool _isUsingAbility = false;

    #region callbacks
    public void OnAbility1Use(InputAction.CallbackContext context)
    {
        OnAbilityUse(ABILITY_1_ID, _ability1Timer, context);
    }

    public void OnAbility2Use(InputAction.CallbackContext context)
    {
        OnAbilityUse(ABILITY_2_ID, _ability2Timer, context);
    }

    public void OnAbility3Use(InputAction.CallbackContext context)
    {
        OnAbilityUse(ABILITY_3_ID, _ability3Timer, context);
    }
    #endregion

    private void OnAbilityUse(int abilityID, FloatValue abilityCooldownTimer, InputAction.CallbackContext context)
    {
        if (_abilities == null || _abilities.Count <= abilityID)
        {
            Debug.Log("Ability not assigned.");
            return;
        }

        if (context.canceled)
        {
            _abilities[abilityID].OnButtonReleased();
        }

        if (abilityCooldownTimer.Value > ZERO)
        {
            Debug.Log("Ability on cooldown.");
            return;
        }

        if (_isUsingAbility)
        {
            Debug.Log("Other ability is being used.");
            return;
        }

        if (context.started)
        {
            StartCoroutine(UseAbility(_abilities[abilityID], abilityCooldownTimer));
        }
    }

    private IEnumerator UseAbility(UsableAbility ability, FloatValue abilityCooldownTimer)
    {
        _isUsingAbility = true;
        _basicAttack.PauseBasicAttacks();
        yield return StartCoroutine(ability.UseAbility(_mousePositionProvider.GetMousePosition()));
        _basicAttack.ResumeBasicAttacks();
        _isUsingAbility = false;
        yield return StartCoroutine(DoCooldown(abilityCooldownTimer, ability.GetCooldown()));
    }

    private IEnumerator DoCooldown(FloatValue cooldownTimer, int cooldown)
    {
        cooldownTimer.SetValue(cooldown);

        while (cooldownTimer.Value > ZERO)
        {
            cooldownTimer.SetValue(Mathf.Max(ZERO, cooldownTimer.Value - Time.deltaTime));
            yield return null;
        }

        cooldownTimer.SetValue(ZERO);
    }

    private void Awake()
    {
        _ability1Timer.SetMaxValue(_abilities[ABILITY_1_ID].GetCooldown());
        _ability2Timer.SetMaxValue(_abilities[ABILITY_2_ID].GetCooldown());
        _ability3Timer.SetMaxValue(_abilities[ABILITY_3_ID].GetCooldown());

        _ability1Timer.SetValue(ZERO);
        _ability2Timer.SetValue(ZERO);
        _ability3Timer.SetValue(ZERO);
    }
}
