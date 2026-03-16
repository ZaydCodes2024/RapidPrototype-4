using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; private set;}
    public event EventHandler onItemAdded;
    public event EventHandler onItemRemoved;
    private List<IInteractable> inventoryItem;
    int itemCount = 0;
    private void Awake()
    {
        Instance = this;
        inventoryItem = new List<IInteractable>();
    }

    public void AddItem(IInteractable interactableItem)
    {
        if (!inventoryItem.Contains(interactableItem))
        {
            inventoryItem.Add(interactableItem);
            onItemAdded?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool HasItem(IInteractable interactableItem)
    {
        return inventoryItem.Contains(interactableItem);
    }
    public void RemoveItem(IInteractable interactableItem)
    {
        if (inventoryItem.Contains(interactableItem))
        {
            inventoryItem.Remove(interactableItem);
            itemCount--;
            onItemRemoved?.Invoke(this, EventArgs.Empty);
        }
    }
    public int CountItem(IInteractable interactable)
    {
        if (inventoryItem.Contains(interactable))
        {
            itemCount++;
        }
        return itemCount;
    }
    public List<IInteractable> GetInteractablesList()
    {
        return inventoryItem;
    }
    
}
