using System.Collections;
using UnityEngine;

public class MeleeAbility : UsableAbility
{
    public override IEnumerator UseAbility(Vector3 targetPosition)
    {
        Debug.Log($"Melee ability used at position: {targetPosition}");
        yield return null;
    }
}
