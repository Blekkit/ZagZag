using System.Collections;
using UnityEngine;

public abstract class UsableAbility : MonoBehaviour
{
    public string AbilityName;

    [SerializeField] private int AbilityCooldown;

    public bool IsUsingAbility {  get; protected set; }

    protected bool _isHoldingButton = false;

    public int GetCooldown()
    {
        return AbilityCooldown;
    }

    public void OnButtonReleased()
    {
        _isHoldingButton = false;
    }

    public abstract IEnumerator UseAbility(Vector3 targetPosition);
}
