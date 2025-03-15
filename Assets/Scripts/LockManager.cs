using UnityEngine;
using System.Collections.Generic;

public class LockManager : MonoBehaviour
{
    public static LockManager Instance;
    public GameObject chest;
    private List<Lock> unlockedLocks = new List<Lock>(); // Track unlocked locks
    private int totalLocks = 3; // Adjust based on the number of locks
    private Lock[] allLocks;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        allLocks = FindObjectsOfType<Lock>();
    }

    public bool UnlockLock(Lock lockToUnlock)
    {
        if (InventoryManager.instance.HasItem(lockToUnlock.requiredKey))
        {
            if (!unlockedLocks.Contains(lockToUnlock))
            {
                unlockedLocks.Add(lockToUnlock);
                InventoryManager.instance.RemoveItem(lockToUnlock.requiredKey);
                CheckCompletion();
                return true;
            }
        }
        else
        {
            Debug.Log($"You need the {lockToUnlock.requiredKey} to unlock this!");
        }

        return false;
    }

    private void CheckCompletion()
    {
        if (unlockedLocks.Count == totalLocks)
        {
            Debug.Log("All locks unlocked!");
            chest.SetActive(false);
        }
    }
}
