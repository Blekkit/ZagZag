using System;
using UnityEngine;

[Serializable]
public struct RangedAttackData
{
    public int ProjectileTypeID;
    public int ProjectileAmount;
    public float DelayBetweenShots;
    public int ProjectileDamage;
    public float ProjectileSpreadAngle;
    public float MinProjectileSpeed;
    public float MaxProjectileSpeed;
    public int PierceAmount;
    public Vector3 ProjectileScale;
    public Vector3 TargetDirection;
    public float AttackCooldown;
}
