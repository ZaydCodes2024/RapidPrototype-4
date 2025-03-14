using UnityEngine;

public class CircuitPiece : MonoBehaviour
{
    public bool isPowered;
    private int currentrotationIndex = 0;
    public Vector3[] rotationAngles = {Vector3.zero, new Vector3(0,0,90),new Vector3(0,0,180),new Vector3(0,0,270)};
    private CircuitSequencePuzzle puzzle;

    void Start()
    {
        puzzle = FindObjectOfType<CircuitSequencePuzzle>();
    }

    void OnMouseDown()
    {
        RotatePiece();
        puzzle.CheckCircuitCompletion();
    }
    void RotatePiece()
    {
        int nextrotationIndex = (currentrotationIndex + 1) % rotationAngles.Length;
        Vector3 rotationDifference = rotationAngles[nextrotationIndex] - rotationAngles[currentrotationIndex];
        transform.Rotate(rotationDifference);
        currentrotationIndex = nextrotationIndex;
    }
    public bool IsConnected()
    {
        return isPowered = true;
    }
}