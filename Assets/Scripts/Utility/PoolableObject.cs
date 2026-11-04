using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    public int ObjectTypeID { get; private set; }

    private ObjectPool _originPool;

    public void SetPool(ObjectPool pool, int objectTypeID)
    {
        _originPool = pool;
        ObjectTypeID = objectTypeID;
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
