using UnityEngine;
using System;

public class PuzzleModule : MonoBehaviour
{
    public event Action OnSolved;
    public bool isSolved = false;

    public virtual void Solve()
    {
        if (isSolved) return;

        isSolved = true;
        OnSolved?.Invoke(); // Notify PuzzleBox that this puzzle is solved
    }
}
