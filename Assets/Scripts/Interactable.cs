using UnityEngine;
public class Interactable : MonoBehaviour
{
    public string itemName = "Default Item";  
    public Sprite itemIcon; 
    public bool isCollectible = true; 

    public virtual void Interact()
    {
        if (isCollectible)
        {
            InventoryManager.instance.AddItem(gameObject);
            gameObject.SetActive(false); 
            Debug.Log($"Collected: {itemName}");
        }
    }
}
