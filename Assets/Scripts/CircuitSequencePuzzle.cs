using UnityEngine;
using UnityEngine.UI;

public class CircuitSequencePuzzle : PuzzleModule
{
    public GameObject circuitHolder;
    private CircuitPiece[] circuitPieces;
    [SerializeField] GameObject indicatorLight;
    [SerializeField] Material solvedMaterial;
    private MeshRenderer indicatorRenderer;

    private int totalPieces = 0;
    private int correctedPieces = 0;

    private void Start()
    {
        totalPieces = circuitHolder.transform.childCount;

        circuitPieces = new CircuitPiece[totalPieces];

        for (int i = 0; i < totalPieces; i++)
        {
            circuitPieces[i] = circuitHolder.transform.GetChild(i).GetComponent<CircuitPiece>();
        }
        indicatorRenderer = indicatorLight.GetComponent<MeshRenderer>();
    }

    public void CorrectMove()
    {   
        if (correctedPieces < totalPieces)
        {
            correctedPieces++;
            Debug.Log($"Correct Move! {correctedPieces}/{totalPieces}");

            if (correctedPieces == totalPieces)
            {
                Debug.Log("Circuit Connected!");
                Solve();
                indicatorRenderer.material = solvedMaterial;
            }
        }
        else
        {
          Debug.Log("All pieces are placed correctly");
        }
    }
}
