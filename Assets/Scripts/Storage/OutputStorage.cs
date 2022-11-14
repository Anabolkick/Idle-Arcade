using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputStorage : Storage, Interactable
{
    [SerializeField] private float _replaceTime = 0.5f;
    public void Interact(PlayerInventory player)
    {
        if (!player.IsInteracting)
        {
            player.IsInteracting = true;
            StartCoroutine(GiveProducts(player));
        }
    }

    public void StopInteract(PlayerInventory player)
    {
        player.IsInteracting = false;
        player.RefreshVisualOrder();
    }

    private IEnumerator GiveProducts(PlayerInventory player)
    {
        while (player.IsInteracting && 0 < ProductsCount && !player.IsFull)
        {
            var product = ProductsList[0];
            RemoveProduct(product);
            var addedVector = Vector3.up * product.transform.lossyScale.y * 1.2f * (player.ProductsCount);  //1.2 const to make some space between products
            yield return StartCoroutine(Replacer.Instance.MoveLerp(product.transform, player.Backpack, addedVector,_replaceTime));
            player.AddProduct(product);
        }
        player.IsInteracting = false;
    }
}
