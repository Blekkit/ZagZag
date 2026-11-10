using System;
using UnityEngine;

[Serializable]
public struct ProjectileData
{
    public int Damage;
    public int PierceAmount;
    public float Speed;
    public Vector2 FireDirection;
    public Vector3 Scale;
    public float LifeTime;
}
