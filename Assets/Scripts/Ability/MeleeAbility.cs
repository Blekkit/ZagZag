using UnityEngine;

public class MeleeAbility : UsableAbility
{
    public override void UseAbility(Vector3 targetPosition)
    {
        Debug.Log($"Melee ability used at position: {targetPosition}");
    }
}
