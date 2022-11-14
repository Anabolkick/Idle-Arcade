using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InputStorage : Storage, Interactable
{
    [SerializeField] private float _replaceTime = 0.5f;
    public void Interact(PlayerInventory player)
    {
        if (!player.IsInteracting)
        {
            player.IsInteracting = true;
            StartCoroutine(TakeProducts(player));
        }
    }

    public void StopInteract(PlayerInventory player)
    {
        player.IsInteracting = false;
        player.RefreshVisualOrder();
    }

    private IEnumerator TakeProducts(PlayerInventory player)
    {
        int iterator = player.ProductsCount-1;
        while (player.IsInteracting && iterator >= 0 && !IsFull)
        {
            var product = player.ProductsList[iterator];
            var isProductMoved = false;
            foreach (var id in AllowedProductsId)
            {
                if (id == product.ProductId)
                {
                    isProductMoved = true;
                    player.RemoveAtProduct(iterator);
                    yield return StartCoroutine(Replacer.Instance.MoveLerp(product.transform, transform, duration: _replaceTime));
                    AddProduct(product);
                    iterator--;
                }

            }
            if (!isProductMoved)
            {
                iterator--;
            }
        }
        yield return null;
        player.IsInteracting = false;
    }

}
