using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveZone : MonoBehaviour
{
    private Interactable _objectToInteract;
    void Awake()
    {
        _objectToInteract = transform.parent.gameObject.GetComponent<Interactable>(); //throw error if parent doesn`t implement Interactable
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(other.TryGetComponent(out PlayerInventory player))
            {
                _objectToInteract.Interact(player);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.TryGetComponent(out PlayerInventory player))
            {
                _objectToInteract.StopInteract(player);
            }
        }
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan * new Color(1f, 1f, 1f, 0.25f);
        Gizmos.DrawCube(transform.position, transform.lossyScale);
    }
#endif
}
