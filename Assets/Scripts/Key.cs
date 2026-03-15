using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (IsInventoryItem())
        {
            InventoryManager.Instance.AddItem(this);
            Debug.Log("Collected: " + gameObject.name);
            gameObject.SetActive(false);
        }
    }
    public bool IsInventoryItem()
    {
        return true;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
