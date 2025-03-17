using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private List<GameObject> inventory = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(GameObject item)
    {
        if (!inventory.Contains(item))
        {
            inventory.Add(item);
        }
    }
    public bool HasItem(GameObject itemName)
    {
        return inventory.Contains(itemName);
    }
    public void RemoveItem(GameObject itemName)
    {
        if (inventory.Contains(itemName))
        {
            inventory.Remove(itemName);
        }
    }
    
}
