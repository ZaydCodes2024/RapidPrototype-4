using UnityEngine;

public class Lock : MonoBehaviour, IInteractable
{
    public static Lock Instance {get; private set;}
    [SerializeField] private Key key;
    private bool isUnlocked = false;

    private void Awake()
    {
        Instance = this;
    }

    public void Interact()
    {       
        
        if (isUnlocked) return;

        if (InventoryManager.Instance.HasItem(key))
        {
            isUnlocked = true;
            InventoryManager.Instance.RemoveItem(key);
            LockManager.Instance.UnlockLock();
            key.DestroySelf();
            gameObject.SetActive(false);
        }  
    }

    public bool IsLockUnlocked()
    {
        return isUnlocked;
    }

    public bool IsInventoryItem()
    {
        return false;
    }

    public int ItemCount()
    {
        return -1;
    }
}
