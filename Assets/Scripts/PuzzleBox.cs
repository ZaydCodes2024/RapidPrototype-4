using UnityEngine;

public class PuzzleBox : MonoBehaviour, IInteractable
{
    public static PuzzleBox Instance {get; private set;}
    public PuzzleModule[] puzzles; 
    private int solvedPuzzles = 0;
    private GameObject currentObject;
    [SerializeField] private Transform inspectPoint;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float rotationSpeed = 15f; 

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
        // Get scroll wheel input (positive or negative) to rotate the object
        float scrollInput = GameInput.Instance.GetScrollVectorNormalized().y;

        // If there's scroll input, apply rotation
        if (scrollInput != 0)
        {
            // Apply the scroll wheel input to rotate around the X-axis (left/right) and Y-axis (up/down)
            float rotationAmount = scrollInput * rotationSpeed;
            // Rotate the object based on the scroll input
            RotateObject(rotationAmount, 0);
        }
    }

    public bool IsInventoryItem()
    {
        return false;
    }

    public bool IsInspectable()
    {
        return true;
    }
    public void StartInspect(PuzzleBox obj)
    {
        if (!IsInspectable()) return;

        currentObject = obj.gameObject;
        originalPosition = currentObject.transform.position;
        originalRotation = currentObject.transform.rotation;

        currentObject.transform.position = inspectPoint.position;
        currentObject.transform.rotation = inspectPoint.rotation; 
    }

    public void StopInspect()
    {
        currentObject.transform.position = originalPosition;
        currentObject.transform.rotation = originalRotation;
        currentObject = null;
    }

    private void RotateObject(float rotX, float rotY)
    {
        // Rotating around the X-axis (left/right)
        currentObject.transform.Rotate(Vector3.up, rotX, Space.World);
        // Rotating around the Y-axis (up/down) — you can adjust this if you need different rotation behavior
        currentObject.transform.Rotate(Vector3.right, rotY, Space.World);
    }

}
