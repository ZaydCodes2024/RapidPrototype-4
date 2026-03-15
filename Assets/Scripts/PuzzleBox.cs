using UnityEngine;

public class PuzzleBox : MonoBehaviour, IInteractable
{
    public static PuzzleBox Instance {get; private set;}
    public PuzzleModule[] puzzles; 
    private int solvedPuzzles = 0;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
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

    public void Interact()
    {
        if (!Inspectable.Instance.GetInspectingState())
        {
            StartInspect();
        }
    }
    public void StartInspect()
    {
        Inspectable.Instance.StartInspect(gameObject);
    }

    public bool IsInventoryItem()
    {
        return false;
    }
}
