using UnityEngine;
public class CircuitPiece : MonoBehaviour
{
    // A reference to the target position in the circuit (where the piece should be placed)
    public Transform correctPosition; // Set this in the inspector to where the piece should be placed in the puzzle.
    public GameObject circuitPiecePrefab;
    private bool isPlaced = false;
    private bool canPlace = false;
    private CircuitSequencePuzzle circuitPuzzle;
    private void Awake()
    {
        circuitPuzzle = FindObjectOfType<CircuitSequencePuzzle>();
    }
    private void Start()
    {
        // Ensure the piece starts in the correct position
        if (correctPosition != null)
        {
            transform.position = correctPosition.transform.position;
            transform.rotation = correctPosition.transform.rotation; // Optional: you can set the correct rotation here if needed.
        }
    }

    // Snaps the piece to the correct position on the circuit
    public void SnapToCorrectPosition()
    {
        if (correctPosition != null && InventoryManager.instance.HasItem(circuitPiecePrefab))
        {   
            Debug.Log("Snapping piece to the correct position");
            transform.position = correctPosition.transform.position; // Move piece to the correct spot
            transform.rotation = correctPosition.transform.rotation; // Align to the correct rotation

            isPlaced = true; // Mark it as placed
            InventoryManager.instance.RemoveItem(circuitPiecePrefab); // Remove from inventory after placing
            circuitPuzzle.CorrectMove();  // Notify puzzle that the move is correct
            canPlace = false;
            this.enabled = false; // Disable this script to prevent further interaction with the piece
        }
    }
}
