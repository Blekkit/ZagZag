using System;
using UnityEngine;

[Serializable]
public class MeleeAttackData
{
    public int Damage;
    public Vector2 HitboxScale;
    public EHitboxShape HitboxShape;
    public Vector3 AttackPositionOffset;

    public float WindupTime;
    public float ActiveTime;
    public float WinddownTime;
    
    public float AttackCooldown;
}
