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
        Debug.Log($"{gameObject.name} Puzzle Solved!");
        OnSolved?.Invoke(); // Notify PuzzleBox that this puzzle is solved
    }
}
