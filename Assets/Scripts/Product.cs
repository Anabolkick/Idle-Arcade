using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    [HideInInspector] public PoolObject PoolObject;
    public int ProductId;
    void Awake()
    {
        if (PoolObject == null)
        {
            PoolObject = GetComponent<PoolObject>();
        }
    }
}
