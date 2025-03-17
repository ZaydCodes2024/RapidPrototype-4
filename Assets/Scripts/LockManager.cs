using UnityEngine;
using System.Collections.Generic;

public class LockManager : MonoBehaviour
{
    public static LockManager Instance;
    public GameObject chest;
    [SerializeField] private GameObject code;
    private List<Lock> unlockedLocks = new List<Lock>(); 
    private int totalLocks = 3; 
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
        return false;
    }

    private void CheckCompletion()
    {
        if (unlockedLocks.Count == totalLocks)
        {
           
            chest.SetActive(false);
            code.SetActive(true);
        }
    }
}
