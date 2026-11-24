using System;
using UnityEngine;

[Serializable]
public struct ProjectileData
{
    public int Damage;
    public int PierceAmount;
    public float Speed;
    public float LifeTime;
    public Vector3 Scale;
    public Vector2 FireDirection;
    public EffectData EffectData;
}
