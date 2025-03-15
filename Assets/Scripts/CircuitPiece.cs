// using UnityEngine;

// public class CircuitPiece : MonoBehaviour
// {
//     public bool isPowered;
//     private int currentrotationIndex = 0;
//     public Vector3[] rotationAngles = {Vector3.zero, new Vector3(0,0,90),new Vector3(0,0,180),new Vector3(0,0,270)};
//     private CircuitSequencePuzzle puzzle;

//     void Start()
//     {
//         puzzle = FindObjectOfType<CircuitSequencePuzzle>();
//     }

//     void OnMouseDown()
//     {
//         RotatePiece();
//         puzzle.CheckCircuitCompletion();
//     }
//     void RotatePiece()
//     {
//         int nextrotationIndex = (currentrotationIndex + 1) % rotationAngles.Length;
//         Vector3 rotationDifference = rotationAngles[nextrotationIndex] - rotationAngles[currentrotationIndex];
//         transform.Rotate(rotationDifference);
//         currentrotationIndex = nextrotationIndex;
//     }
//     public bool IsConnected()
//     {
//         return isPowered = true;
//     }
// }
using UnityEngine;

public class CircuitPiece : MonoBehaviour
{
    float[] rotations = { 0, 90, 180, 270 };
    public float[] correctRotation;
    private bool isPlaced = false;
    private bool canRotate = true;
    private int possibleRotations;

    private CircuitSequencePuzzle circuitPuzzle;

    private void Awake()
    {
        circuitPuzzle = FindObjectOfType<CircuitSequencePuzzle>();
    }

    private void Start()
    {
        possibleRotations = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
    }

    private void OnMouseDown()
    {
        if (canRotate)
        {
            transform.Rotate(new Vector3(0, 0, 90));
            CheckPlacement();
        }
    }

    private void CheckPlacement()
    {
        bool correct = false;

        foreach (float correctRot in correctRotation)
        {
            if (Mathf.Approximately(transform.eulerAngles.z, correctRot))
            {
                correct = true;
                break;
            }
        }

        if (correct && !isPlaced)
        {
            isPlaced = true;
            circuitPuzzle.CorrectMove();
            canRotate = false;
        }
        else if (!correct && isPlaced)
        {
            isPlaced = false;
            canRotate = true;
        }
    }
}
