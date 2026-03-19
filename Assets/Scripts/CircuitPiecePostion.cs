using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitPiecePostion : MonoBehaviour
{
    [SerializeField] private Transform correctPosition; 
    [SerializeField] private CircuitPiece circuitPiecePrefab;
    [SerializeField] private CircuitSequencePuzzle circuitPuzzle;

    // Snaps the piece to the correct position on the circuit
    public void SnapToCorrectPosition()
    {
        
        if (correctPosition != null && InventoryManager.Instance.HasItem(circuitPiecePrefab.GetInventoryItemSO()))
        {   
            circuitPiecePrefab.transform.SetParent(correctPosition);
            
            // Reset transform properties to align perfectly
            circuitPiecePrefab.transform.localPosition = Vector3.zero; 
            circuitPiecePrefab.transform.localRotation = Quaternion.identity; 
            circuitPiecePrefab.transform.localScale = Vector3.one;

            circuitPiecePrefab.gameObject.SetActive(true);
            InventoryManager.Instance.RemoveItem(circuitPiecePrefab.GetInventoryItemSO()); // Remove from inventory after placing

            circuitPuzzle.CorrectMove();  // Notify puzzle that the move is correct

            this.enabled = false; // Disable this script to prevent further interaction with the piece
        }
    }
}
