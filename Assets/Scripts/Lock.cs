using UnityEngine;

public class Lock : Interactable
{
    public GameObject requiredKey;
    private bool isUnlocked = false;
    public override void Interact()
    {       
        Debug.Log($"Trying to unlock {gameObject.name} with key {requiredKey.name}...");
            if (isUnlocked) return;
            if (InventoryManager.instance.HasItem(requiredKey))
            {
                if (LockManager.Instance.UnlockLock(this))
                {
                    isUnlocked = true;
                    Debug.Log($"Lock {requiredKey} Unlocked!");
                    gameObject.SetActive(false); // Hide lock after unlocking
                }
            }
            else{
                Debug.Log("Get the key");
            }    
    }
}
