using UnityEngine;
public class CircuitPiece : MonoBehaviour, IInteractable
{
    [SerializeField] private  InventoryItemSO inventoryItemSO;
    public void Interact()
    {
        if (IsInventoryItem())
        {
            InventoryManager.Instance.AddItem(inventoryItemSO);
            Debug.Log("Collected: " + gameObject.name);
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
}
