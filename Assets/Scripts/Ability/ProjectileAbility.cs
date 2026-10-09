using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileAbility : UsableAbility
{
    [Header("Settings")]
    [SerializeField] private int _projectileAmount = 3;
    [SerializeField] private int _projectileBonusDamage = 1;
    [SerializeField] private float _projectileSpreadAngle = 15f;
    [SerializeField] private float _projectileMinSpeed = 10f;
    [SerializeField] private float _projectileMaxSpeed = 20f;

    [Header("References")]
    [SerializeField] private Transform _shootOrigin;
    [SerializeField] private ProjectileFiring _firing;

    public override void UseAbility(Vector3 targetPosition)
    {
        Debug.Log($"Projectile ability used at position: {targetPosition}");

        for (int i = 0; i < _projectileAmount; i++)
        {
            Vector3 direction = targetPosition - _shootOrigin.position;
            _firing.PerformAttack(direction, _projectileBonusDamage, _projectileSpreadAngle, Random.Range(_projectileMinSpeed, _projectileMaxSpeed));
        }
    }
}