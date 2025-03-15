using UnityEngine;
public class Interactable : MonoBehaviour
{
    public string itemName = "Default Item";  // Name of the object
    public Sprite itemIcon; // (Optional) UI icon for inventory
    public bool isCollectible = true; // Can this object be picked up?

    public virtual void Interact()
    {
        if (isCollectible)
        {
            InventoryManager.instance.AddItem(gameObject);
            gameObject.SetActive(false); // Hide object after collecting
            Debug.Log($"Collected: {itemName}");
        }
    }
}
