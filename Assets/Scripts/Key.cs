using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private  InventoryItemSO inventoryItemSO;
    public void Interact()
    {
        if (IsInventoryItem())
        {
            InventoryManager.Instance.AddItem(inventoryItemSO);
            gameObject.SetActive(false);
        }
    }
    public InventoryItemSO GetInventoryItemSO()
    {
        return inventoryItemSO;
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
