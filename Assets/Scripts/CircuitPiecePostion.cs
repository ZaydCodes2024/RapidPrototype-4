using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitPiecePostion : MonoBehaviour
{
    [SerializeField] private Transform correctPosition; 
    [SerializeField] private CircuitPiece circuitPiecePrefab;
    private CircuitSequencePuzzle circuitPuzzle;
    private void Awake()
    {
        circuitPuzzle = FindObjectOfType<CircuitSequencePuzzle>();
    }

    // Snaps the piece to the correct position on the circuit
    public void SnapToCorrectPosition()
    {
        
        if (correctPosition != null && InventoryManager.Instance.HasItem(circuitPiecePrefab))
        {   
            circuitPiecePrefab.transform.SetParent(correctPosition);
            
            // Reset transform properties to align perfectly
            circuitPiecePrefab.transform.localPosition = Vector3.zero; 
            circuitPiecePrefab.transform.localRotation = Quaternion.identity; 
            circuitPiecePrefab.transform.localScale = Vector3.one;

            circuitPiecePrefab.gameObject.SetActive(true);
            Debug.Log("Placed: " + circuitPiecePrefab.name);
            InventoryManager.Instance.RemoveItem(circuitPiecePrefab); // Remove from inventory after placing

            circuitPuzzle.CorrectMove();  // Notify puzzle that the move is correct

            this.enabled = false; // Disable this script to prevent further interaction with the piece
        }
    }
}
