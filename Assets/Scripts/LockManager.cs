using UnityEngine;
using System.Collections.Generic;
using System;

public class LockManager : MonoBehaviour
{
    public static LockManager Instance {get; private set;}
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject code;
    [SerializeField] private GameObject puzzleBox;
    [SerializeField] private List<Lock> unlockedLocks = new List<Lock>(); 
    private int totalLocks; 
    private void Awake()
    {
       Instance = this;
    }

    public void UnlockLock()
    {
        totalLocks++;

        if (Lock.Instance.IsLockUnlocked())
        {
            CheckCompletion();
        }
    }
    private void CheckCompletion()
    {
        if (unlockedLocks.Count == totalLocks)
        {
            chest.SetActive(false);
            code.SetActive(true);
            puzzleBox.SetActive(true);
            totalLocks = 0;
        }
    }
}
