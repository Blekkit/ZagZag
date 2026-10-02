using UnityEngine;

public class MeleeAttack : MonoBehaviour, Attacking
{
    [SerializeField] private GameObject _attackHitBox;

    private bool _isHitboxOn = false;

    public void PerformAttack(Vector2 direction)
    {
        _isHitboxOn = !_isHitboxOn;
        _attackHitBox.SetActive(_isHitboxOn);
    }
}
