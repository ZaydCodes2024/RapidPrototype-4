using UnityEngine;
public class CircuitPiece : MonoBehaviour, IInteractable
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

    public int ItemCount()
    {
        return InventoryManager.Instance.CountItem(this);
    }
}
