using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manufacture : MonoBehaviour, Interactable
{
    [SerializeField] private ManufactureConfig _config;
    [SerializeField] private InputStorage _inputStorage;
    [SerializeField] private OutputStorage _outputStorage;

    private float _productionTime;
    private bool _isGenerating;
    private Pool _pool;

    void Awake()
    {
        _pool = GetComponent<Pool>();
        _pool.Ñontainer = this.transform;
        _pool.Prefab = _config.OutputProduct.GetComponent<PoolObject>();

        SetConfigValues();
    }

    void Start()
    {
        StartCoroutine(Generate());
    }

    private void SetConfigValues()
    {
        foreach (var configInputProduct in _config.InputProducts)
        {
            _inputStorage.AllowedProducts.Add(configInputProduct);
        }
        _inputStorage.SetAllowedId();

        _outputStorage.AllowedProducts.Add(_config.OutputProduct);
        _outputStorage.SetAllowedId();

        _productionTime = _config.ProductionTime;
    }

    private IEnumerator Generate()
    {
        do
        {
            if (!_outputStorage.IsFull)
            {
                List<Product> recycleList = new List<Product>();
                if ((!_config.RequireInput || !_inputStorage.IsEmpty) && AllInputProductsAvailable(ref recycleList))
                {
                    _isGenerating = true;
                    foreach (var product in recycleList)
                    {
                        StartCoroutine(Recycle(product));
                        _inputStorage.RemoveProduct(product);
                    }

                    yield return new WaitForSeconds(_productionTime);

                    var outProduct = _pool.GetElement(transform.position, Quaternion.identity).GetComponent<Product>();
                    StartCoroutine(MoveToOutput(outProduct));
                    continue;

                }
                else if(_isGenerating)
                {
                    UIManager.Instance.StopInfoText = $"{this.name} - Stopped. No required products at the input storage!";
                }
            }
            else if(_isGenerating)
            {
                UIManager.Instance.StopInfoText = $"{this.name} - Stopped. Output storage is full!";
            }
            _isGenerating = false;
            yield break;

        } while (_isGenerating);
    }

    private bool AllInputProductsAvailable(ref List<Product> recycleList)
    {
        recycleList.Clear();
        foreach (var id in _inputStorage.AllowedProductsId)
        {
            var product = _inputStorage.ProductsList.FirstOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                recycleList.Add(product);
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator Recycle(Product product)
    {
        yield return StartCoroutine(Replacer.Instance.MoveLerp(product.transform, transform));
        product.PoolObject.ReturnToPool();
    }

    private IEnumerator MoveToOutput(Product product)
    {
        yield return StartCoroutine(Replacer.Instance.MoveLerp(product.transform, _outputStorage.transform));
        _outputStorage.AddProduct(product);
    }

    public void Interact(PlayerInventory player)
    {
        if (!_isGenerating)
        {
            StartCoroutine(Generate());
        }
    }

    public void StopInteract(PlayerInventory player)
    {

    }
}
