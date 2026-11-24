using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Move _entityMovement;
    [SerializeField] private Health _entityHealth;
    [SerializeField] private float _entityMinSlowedSpeed = 0.1f;

    public IEnumerator ApplyEffect(EffectData data)
    {
        Debug.Log($"Applying effect {data.EffectType} to entity {gameObject.name}");

        if (data.EffectType == EEffect.none)
        {
            yield break;
        }

        if (data.EffectType == EEffect.scating)
        {
            _entityMovement.Scate(data.ScateData);
            yield break;
        }

        float effectTimer = 0f;
        float tickTimer = 0f;

        OnEffectStart(data);

        while (effectTimer <= data.EffectDuration)
        {
            effectTimer += Time.deltaTime;
            if (data.EffectTickRate % tickTimer == 0)
            {
                OnEffectTick(data);
            }
            yield return null;
        }

        OnEffectEnd(data);
    }


    private void OnEffectStart(EffectData data)
    {
        switch (data.EffectType)
        {
            case EEffect.slow:
                float slowedSpeed = Mathf.Max(_entityMovement.GetCurrentMoveSpeed() - data.EffectStrength, _entityMinSlowedSpeed);
                Debug.Log($"Slowed entity {gameObject.name} to speed: {slowedSpeed}");
                _entityMovement.SetTempSpeed(slowedSpeed);
                break;
        }
    }

    private void OnEffectTick(EffectData data)
    {
        switch (data.EffectType)
        {
            case EEffect.poison:
                _entityHealth.TakeDamage((int)data.EffectStrength);
                break;
        }
    }

    private void OnEffectEnd(EffectData data)
    {
        switch (data.EffectType)
        {
            case EEffect.poison:
                break;

            case EEffect.slow:
                _entityMovement.ResetSpeed();
                break;
        }
    }
}
