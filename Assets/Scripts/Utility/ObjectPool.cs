using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _pooledObject;
    [SerializeField] private int _startingPoolSize = 10;
    [SerializeField] private Transform _parentTransform;

    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    public GameObject GetObjectFromPool(Transform position)
    {
        if (_objectPool.Count > 0)
        {
            GameObject obj = _objectPool.Dequeue();
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

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        _objectPool.Enqueue(obj);
    }

    private void AddObjectToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(_pooledObject, _parentTransform);
            obj.SetActive(false);
            _objectPool.Enqueue(obj);
        }
    }

    private void Awake()
    {
        AddObjectToPool(_startingPoolSize);
    }
}