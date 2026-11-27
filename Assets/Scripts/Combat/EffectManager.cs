using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private Move _entityMovement;
    [SerializeField] private Health _entityHealth;
    [SerializeField] private float _entityMinSlowedSpeed = 0.1f;

    public void StartEffect(EffectData data)
    {
        StartCoroutine(ApplyEffect(data));
    }

    private IEnumerator ApplyEffect(EffectData data)
    {
        Debug.Log($"Applying effect {data.EffectType} for {data.EffectDuration} to entity {gameObject.name}");

        if (data.EffectType == EEffect.none)
        {
            yield break;
        }

        if (data.EffectType == EEffect.scating)
        {
            StartCoroutine(_entityMovement.Scate(data.ScateData));
            yield break;
        }

        float effectTimer = 0f;

        OnEffectStart(data);

        while(effectTimer <= data.EffectDuration)
        {
            effectTimer += Time.deltaTime;
            Debug.Log(effectTimer);
            if (data.EffectTickRate % effectTimer == 0)
            {
                OnEffectTick(data);
            }

            yield return new WaitForEndOfFrame();
        }

        OnEffectEnd(data);
        yield return null;
    }

    private void OnEffectStart(EffectData data)
    {
        switch (data.EffectType)
        {
            case EEffect.slow:
                float slowedSpeed = Mathf.Max(_entityMovement.GetCurrentMoveSpeed() - data.EffectStrength, _entityMinSlowedSpeed);
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
