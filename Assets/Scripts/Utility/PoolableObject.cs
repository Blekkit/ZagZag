using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    private ObjectPool _originPool;

    public void SetPool(ObjectPool pool)
    {
        _originPool = pool;
    }

    public void ReturnToPool()
    {
        if (_originPool != null)
            _originPool.ReturnObjectToPool(this);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
