using UnityEngine;
using System;

public class PuzzleModule : MonoBehaviour
{
    public event Action OnSolved;
    [SerializeField] private bool isSolved = false;

    public virtual void Solve()
    {
        if (isSolved) return;

        isSolved = true;
        OnSolved?.Invoke(); // Notify PuzzleBox that this puzzle is solved
    }
    
    public bool GetSolvedState()
    {
        return isSolved;
    }
}
