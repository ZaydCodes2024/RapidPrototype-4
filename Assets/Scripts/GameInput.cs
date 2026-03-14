using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}
    InputActions playerInputActions;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputActions();
        playerInputActions.Enable();

    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>().normalized;
        return inputVector;
    }
    public float GetMovementSpeed(float runspeed, float walkSpeed)
    {
        float movementSpeed = playerInputActions.Player.Sprint.IsPressed() ? runspeed : walkSpeed;
        return  movementSpeed;
    }

}
