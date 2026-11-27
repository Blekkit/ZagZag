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
        {
            _originPool.ReturnObjectToPool(this);
        }
        else
        {
            Debug.LogWarning($"Projectile {gameObject.name} is trying to return to unassigned pool. Destroying projectile GameObject...");
            Destroy(this.gameObject);
        }
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
