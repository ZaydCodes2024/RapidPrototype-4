using UnityEngine;

public class Lock : Interactable
{
    public GameObject requiredKey;
    private bool isUnlocked = false;
    public override void Interact()
    {       
        
            if (isUnlocked) return;
            if (InventoryManager.instance.HasItem(requiredKey))
            {
                if (LockManager.Instance.UnlockLock(this))
                {
                    isUnlocked = true;
                    
                    gameObject.SetActive(false); // Hide lock after unlocking
                }
            }    
    }
}
