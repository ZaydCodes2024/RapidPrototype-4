using UnityEngine;

public class CircuitSequencePuzzle : PuzzleModule
{
    private CircuitPiece[] circuitPieces;

    void Start()
    {
        circuitPieces = FindObjectsOfType<CircuitPiece>();
    }

    public void CheckCircuitCompletion()
    {
        foreach (CircuitPiece piece in circuitPieces)
        {
            if (!piece.IsConnected())
            {
                Debug.Log("Circuit is not completed");
                return;
            }
        }

        Debug.Log("Circuit is Powered!");
        Solve();
    }
}