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
            Debug.Log("Item added: " + item.name);
            InventoryManager.instance.ShowInventory();
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
            Debug.Log("Item removed: " + itemName);
        }
    }
    public void ShowInventory()
    {
        if (inventory.Count == 0)
        {
            Debug.Log("Inventory is empty.");
        }
        else
        {
            Debug.Log("Inventory contains the following items:");
            foreach (GameObject item in inventory)
            {
                Debug.Log(item.name); // Logs the name of each item in the inventory
            }
        }
    }
}
