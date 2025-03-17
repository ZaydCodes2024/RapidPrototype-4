using UnityEngine;

public class PuzzleBox : MonoBehaviour
{
    public PuzzleModule[] puzzles; 
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

        if (solvedPuzzles >= puzzles.Length)
        {
            GameManager.Instance.CompleteGame();
        }
    }
}
