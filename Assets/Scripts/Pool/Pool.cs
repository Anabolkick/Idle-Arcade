using System;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [HideInInspector] public Transform Ñontainer;
    [HideInInspector] public PoolObject Prefab;
    [SerializeField] private List<PoolObject> _pool;
    [SerializeField] private int _minCapacity;
    [SerializeField] private int _maxCapacity;
    [SerializeField] private bool _autoExpand;

    private void OnValidate()
    {
        if (_autoExpand)
        {
            _maxCapacity = Int32.MaxValue;
        }
    }

    void Start()
    {
        CreatePool();
    }
    private void CreatePool()
    {
        _pool = new List<PoolObject>(_minCapacity);

        for (int i = 0; i < _minCapacity; i++)
        {
            CreateElement();
        }
    }
    public PoolObject GetElement()
    {

        if (TryGetElement(out var element))
        {
            element.ResetDefaultValues();
            element.transform.SetParent(Ñontainer);
            return element;
        }
        else if (_autoExpand || _pool.Count < _maxCapacity)
        {
            return CreateElement(true);
        }
        else
        {
            throw new Exception("Pool is full, cant create new element!");
        }
    }
    public PoolObject GetElement(Vector3 position)
    {
        var element = GetElement();
        element.transform.position = position;
        return element;
    }

    public PoolObject GetElement(Vector3 position, Quaternion rotation)
    {
        var element = GetElement(position);
        element.transform.rotation = rotation;
        return element;
    }

    public PoolObject GetElement(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        var element = GetElement(position, rotation);

        var parent = element.transform.parent;
        element.transform.parent = null;
        element.transform.localScale = scale;
        element.transform.parent = parent;

        return element;
    }
    private PoolObject CreateElement(bool isActive = false)
    {
        var createdObject = Instantiate(Prefab);
        createdObject.transform.SetParent(Ñontainer);
        createdObject.gameObject.SetActive(isActive);

        _pool.Add(createdObject);

        return createdObject;
    }

    private bool TryGetElement(out PoolObject element)
    {
        foreach (var poolObject in _pool)
        {
            if (!poolObject.gameObject.activeInHierarchy)
            {
                element = poolObject;
                poolObject.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

}    
