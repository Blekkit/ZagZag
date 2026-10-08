using UnityEngine;

public abstract class UsableAbility : MonoBehaviour
{
    [SerializeField] private int AbilityCooldown;

    public int GetCooldown()
    {
        return AbilityCooldown;
    }

    public abstract void UseAbility(Vector3 targetPosition);
}
