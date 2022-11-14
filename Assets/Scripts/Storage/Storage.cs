using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class Storage : MonoBehaviour, IStorage
{
    [HideInInspector] public List<Product> AllowedProducts = new List<Product>();
    [HideInInspector] public List<int> AllowedProductsId = new List<int>();
    [HideInInspector] public List<Product> ProductsList { get; private set; }
    public bool IsFull { get; private set; }
    public bool IsEmpty { get; private set; }

    [SerializeField] private int _capacity = 10;

    private int _productsCount;

    public int Capacity
    {
        get => _capacity;
        set
        {
            _capacity = value;
            ProductsCount = ProductsCount;
        }
    }
    public int ProductsCount
    {
        get => _productsCount;
        set
        {
            _productsCount = value;
            if (_productsCount >= Capacity)
            {
                IsFull = true;
                IsEmpty = false;
            }
            else if (ProductsCount <= 0)
            {
                IsEmpty = true;
                IsFull = false;
            }
            else
            {
                IsEmpty = false;
                IsFull = false;
            }
        }
    }

    void Awake()
    {
        ProductsList = new List<Product>();
        ProductsCount = ProductsList.Count;
    }


    public void AddProduct(Product product)
    {
        ProductsList.Add(product);
        ProductsCount++;
    }

    public void RemoveAtProduct(int index)
    {
        ProductsList.RemoveAt(index);
        ProductsCount--;
    }

    public void RemoveProduct(Product product)
    {
        ProductsList.Remove(product);
        ProductsCount--;
    }

    public void SetAllowedId()
    {
        foreach (var allowedProduct in AllowedProducts)
        {
            AllowedProductsId.Add(allowedProduct.ProductId);
        }
    }
}
