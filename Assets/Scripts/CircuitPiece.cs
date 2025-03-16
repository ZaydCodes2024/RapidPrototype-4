using UnityEngine;
public class CircuitPiece : MonoBehaviour
{
    // A reference to the target position in the circuit (where the piece should be placed)
    public Transform correctPosition; // Set this in the inspector to where the piece should be placed in the puzzle.
    public GameObject circuitPiecePrefab;
    private CircuitSequencePuzzle circuitPuzzle;
    private void Awake()
    {
        circuitPuzzle = FindObjectOfType<CircuitSequencePuzzle>();
    }

    // Snaps the piece to the correct position on the circuit
    public void SnapToCorrectPosition()
    {
        if (correctPosition != null && InventoryManager.instance.HasItem(circuitPiecePrefab))
        {   
            Debug.Log("Snapping piece to the correct position");
            circuitPiecePrefab.transform.SetParent(correctPosition);
            
            // Reset transform properties to align perfectly
            circuitPiecePrefab.transform.localPosition = Vector3.zero; // Center it
            circuitPiecePrefab.transform.localRotation = Quaternion.identity; // Reset rotation
            circuitPiecePrefab.transform.localScale = Vector3.one; // Fix scaling issues
            circuitPiecePrefab.SetActive(true);
            InventoryManager.instance.RemoveItem(circuitPiecePrefab); // Remove from inventory after placing
            circuitPuzzle.CorrectMove();  // Notify puzzle that the move is correct
            this.enabled = false; // Disable this script to prevent further interaction with the piece
        }
    }
}
