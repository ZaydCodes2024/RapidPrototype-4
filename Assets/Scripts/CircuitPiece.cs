using UnityEngine;
public class CircuitPiece : MonoBehaviour, IInteractable
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
}
