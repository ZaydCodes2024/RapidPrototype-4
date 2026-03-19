using UnityEngine;
using System;

public class PuzzleModule : MonoBehaviour
{
    public event EventHandler OnSolved;
    private bool isSolved = false;

    public void Solve()
    {
        if (isSolved) return;

        isSolved = true;
        OnSolved?.Invoke(this,EventArgs.Empty); 
    }
    
    public bool GetSolvedState()
    {
        return isSolved;
    }
}
