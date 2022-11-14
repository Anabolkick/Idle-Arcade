using UnityEngine;

public class PlayerInventory : Storage
{
    [HideInInspector] public bool IsInteracting;
    public Transform Backpack;
    public void RefreshVisualOrder()
    {
        for (var i = 0; i < ProductsList.Count; i++)
        {
            var product = ProductsList[i];
            Vector3? addedVector = Vector3.up * product.transform.lossyScale.y * 1.2f * i; //1.2 const to make some space between products
            StartCoroutine(Replacer.Instance.MoveLerp(product.transform, Backpack, addedVector, 0.01f));
        }
    }

}
