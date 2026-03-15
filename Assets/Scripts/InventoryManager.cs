using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; private set;}
    private List<IInteractable> inventoryItem;

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
        }
    }
    
}
