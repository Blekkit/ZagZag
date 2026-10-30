using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private PoolableObject _pooledObject;
    [SerializeField] private int _startingPoolSize = 10;
    [SerializeField] private Transform _parentTransform;

    private Queue<PoolableObject> _objectPool = new Queue<PoolableObject>();

    public GameObject GetObjectFromPool(Transform position)
    {
        if (_objectPool.Count > 0)
        {
            PoolableObject poolObj = _objectPool.Dequeue();
            GameObject obj = poolObj.GetGameObject();
            obj.transform.position = position.position;
            obj.transform.rotation = position.rotation;
            obj.SetActive(true);
            return obj;
        }
        else
        {
            AddObjectToPool(1);
            return GetObjectFromPool(position);
        }
    }

    public void ReturnObjectToPool(PoolableObject obj)
    {
        obj.GetGameObject().SetActive(false);
        _objectPool.Enqueue(obj);
    }

    private void AddObjectToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(_pooledObject.GetGameObject(), _parentTransform);
            obj.SetActive(false);
            PoolableObject poolObj = obj.GetComponent<PoolableObject>();
            poolObj.SetPool(this);
            _objectPool.Enqueue(poolObj);
        }
    }

    private void Awake()
    {
        AddObjectToPool(_startingPoolSize);
    }
}