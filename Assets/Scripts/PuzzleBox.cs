using UnityEngine;

public class PuzzleBox : MonoBehaviour
{
    public PuzzleModule[] puzzles; // Array of puzzles in the box
    private int solvedPuzzles = 0;

    void Start()
    {
        foreach (var puzzle in puzzles)
        {
            puzzle.OnSolved += CheckCompletion;
        }
    }

    public void CheckCompletion()
    {
        solvedPuzzles++;
        Debug.Log($"Puzzles Solved: {solvedPuzzles}/{puzzles.Length}");

        if (solvedPuzzles >= puzzles.Length)
        {
            GameManager.Instance.CompleteGame();
        }
    }
}
