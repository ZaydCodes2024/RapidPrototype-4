using UnityEngine;

public class PuzzleBox : MonoBehaviour, IInteractable
{
    public static PuzzleBox Instance {get; private set;}
    [SerializeField] private PuzzleModule[] puzzleModules; 
    private int solvedPuzzles;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (var puzzle in puzzleModules)
        {
            puzzle.OnSolved += CheckCompletion;
        }
    }

    public void CheckCompletion()
    {
        solvedPuzzles++;

        if (solvedPuzzles >= puzzleModules.Length)
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
