using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; private set;}
    public event EventHandler onItemAdded;
    public event EventHandler onItemRemoved;
    private Dictionary<InventoryItemSO,int> inventoryItem;
    private void Awake()
    {
        Instance = this;
        inventoryItem = new Dictionary<InventoryItemSO,int>();
    }

    public void AddItem(InventoryItemSO item)
    {
        if (inventoryItem.ContainsKey(item))
        {
            inventoryItem[item]++;
        }
        else
        {
            inventoryItem[item] = 1;
        }
        onItemAdded?.Invoke(this, EventArgs.Empty);
        SoundManager.Instance.PlayItemPickupSound(transform.position, 1f);
    }
    public bool HasItem(InventoryItemSO item)
    {
        return inventoryItem.ContainsKey(item);
    }
    public void RemoveItem(InventoryItemSO item)
    {
        if (!inventoryItem.ContainsKey(item))    return;
        inventoryItem[item]--;

        if (inventoryItem[item] <= 0)
        {
            inventoryItem.Remove(item);    
        }   

        onItemRemoved?.Invoke(this, EventArgs.Empty);
    }
    public Dictionary<InventoryItemSO,int> GetInventoryItemList()
    {
        return inventoryItem;
    }
}
