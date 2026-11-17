using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    public UnityEvent OnAttackSwing;

    [SerializeField] private AttackHitbox _squareAttackHitbox;
    [SerializeField] private AttackHitbox _circleAttackHitbox;
    [SerializeField] private Transform _hitboxParent;

    public bool CanAttack { get; private set; } = true;

    public void CancelAttack()
    {
        StopAllCoroutines();
        _hitboxParent.localPosition = Vector3.zero;
        _squareAttackHitbox.gameObject.SetActive(false);
        _circleAttackHitbox.gameObject.SetActive(false);
        CanAttack = true;
    }

    public IEnumerator PerformAttack(MeleeAttackData data)
    {
        AttackHitbox currentHitbox;
        switch(data.HitboxShape)
        {
            case EHitboxShape.square:
                currentHitbox = _squareAttackHitbox;
                break;

            case EHitboxShape.circle:
                currentHitbox = _circleAttackHitbox;
                break;

            default:
                currentHitbox = _squareAttackHitbox;
                break;
        }

        //attack windup step
        CanAttack = false;
        currentHitbox.SetDamage(data.Damage);
        _hitboxParent.localScale = new Vector3(data.HitboxScale.x, 1, data.HitboxScale.y);
       
        _hitboxParent.localPosition = data.AttackPositionOffset;
        yield return new WaitForSeconds(data.WindupTime);
        
        //attack active step        DAMAGE STEP
        currentHitbox.gameObject.SetActive(true);
        OnAttackSwing?.Invoke();

        //moving active hitbox
        if (data.DoesUpdateHitboxPosition)
        {
            float elapsedTime = 0f;
            while (elapsedTime < data.ActiveTime)
            {
                _hitboxParent.localPosition = data.AttackPositionOffset;
                _hitboxParent.rotation = Quaternion.Euler(data.HitboxRotation);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        //stationary active hitbox
        else
        {
            yield return new WaitForSeconds(data.ActiveTime);
        }
        
        //attack cooldown step
        currentHitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(data.WinddownTime);
        
        //after attack finished
        CanAttack = true;
        _hitboxParent.localPosition = Vector3.zero;
        _hitboxParent.localRotation = Quaternion.identity;
    }


}