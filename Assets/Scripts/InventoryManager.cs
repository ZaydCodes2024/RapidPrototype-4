using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance; // Singleton for easy access
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
        }
    }

    public void ShowInventory()
    {
        Debug.Log("Inventory: " + string.Join(", ", inventory.ConvertAll(i => i.name)));
    }
}
