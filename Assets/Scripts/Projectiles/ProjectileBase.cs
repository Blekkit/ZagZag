using System.Collections;
using UnityEngine;

public abstract class ProjectileBase : PoolableObject
{
    protected Rigidbody _rb;
    protected Transform _tf;

    protected ProjectileData _projectileData;

    private float _lifeTimer = 0;

    public void Initialise(ProjectileData data)
    {
        _projectileData = data;
        _tf.localScale = data.Scale;
    }

    public virtual void Fire()
    {
        StartCoroutine(StartLifeTimer());
    }

    protected IEnumerator StartLifeTimer()
    {
        _lifeTimer = 0;

        while (_lifeTimer < _projectileData.LifeTime)
        {
            _lifeTimer += Time.deltaTime;
            yield return null;
        }

        Destroy();
    }

    private void Destroy()
    {
        StopAllCoroutines();
        ReturnToPool();
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _tf = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //wall colision
        if (collider.gameObject.CompareTag("Environment"))
        {
            Destroy();
            return;
        }

        //effect
        if (collider.gameObject.TryGetComponent<EffectManager>(out EffectManager effectManager))
        {
            effectManager.StartEffect(_projectileData.EffectData);
            //StartCoroutine(effectManager.ApplyEffect(_projectileData.EffectData));
        }

        //damage
        if (collider.gameObject.TryGetComponent<Health>(out Health targetHealth))
        {
            targetHealth.TakeDamage(_projectileData.Damage);

            if (_projectileData.PierceAmount <= 0)
            {
                Destroy();
            }
            else
            {
                _projectileData.PierceAmount--;
            }
        }
    }
}
