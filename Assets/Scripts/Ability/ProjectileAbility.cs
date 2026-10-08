using UnityEngine;

public class ProjectileAability : UsableAbility
{
    public override void UseAbility(Vector3 targetPosition)
    {
        Debug.Log($"Projectile ability used at position: {targetPosition}");
    }
}
