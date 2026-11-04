using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<PoolableObject> _pooledObjects;
    [SerializeField] private int _startingPoolSize = 10;
    [SerializeField] private Transform _parentTransform;

    private List<Queue<PoolableObject>> _objectPool = new List<Queue<PoolableObject>>();

    private const int INCREASE_POOL_SIZE = 1;

    public GameObject GetObjectFromPool(Transform position, int objectTypeID)
    {
        if (_objectPool[objectTypeID].Count > 0)
        {
            PoolableObject poolObj = _objectPool[objectTypeID].Dequeue();
            GameObject obj = poolObj.GetGameObject();
            obj.transform.position = position.position;
            obj.transform.rotation = position.rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            AddObjectToPool(INCREASE_POOL_SIZE, objectTypeID);
            return GetObjectFromPool(position, objectTypeID);
        }
    }

    public void ReturnObjectToPool(PoolableObject obj)
    {
        obj.GetGameObject().SetActive(false);
        _objectPool[obj.ObjectTypeID].Enqueue(obj);
    }

    private void AddObjectToPool(int amount, int objectTypeID)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(_pooledObjects[objectTypeID].GetGameObject(), _parentTransform);
            obj.SetActive(false);
            PoolableObject poolObj = obj.GetComponent<PoolableObject>();
            poolObj.SetPool(this, objectTypeID);
            _objectPool[objectTypeID].Enqueue(poolObj);
        }
    }

    private void Awake()
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            AddObjectToPool(_startingPoolSize, i);
        }
    }
}