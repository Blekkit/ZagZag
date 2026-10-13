using System.Collections;
using UnityEngine;

public class MeleeAttack : MonoBehaviour, Attacking
{
    [SerializeField] private GameObject _attackHitBox;

    [Header("Settings")]
    [SerializeField] private float _windupTime = 0f;
    [SerializeField] private float _attackDuration = 1f;
    [SerializeField] private float _cooldownDuration = 0.5f;

    //private bool _isHitboxOn = false;
    public bool CanAttack { get; private set; } = true;

    public IEnumerator PerformAttack(Vector2 direction)
    {
        //_isHitboxOn = !_isHitboxOn;
        //_attackHitBox.SetActive(_isHitboxOn);
        yield return StartCoroutine(DoMeleeAttack());
    }

    private IEnumerator DoMeleeAttack()
    {
        CanAttack = false;
        yield return new WaitForSeconds(_windupTime);
        _attackHitBox.SetActive(true);
        yield return new WaitForSeconds(_attackDuration);
        _attackHitBox.SetActive(false);
        yield return new WaitForSeconds(_cooldownDuration);
        CanAttack = true;
    }
}
