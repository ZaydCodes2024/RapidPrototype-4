using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}
    public event EventHandler OnInteractAction;
    InputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Interact.performed += Interact_Performed;
    }

    private void Interact_Performed(InputAction.CallbackContext context)
    {
       OnInteractAction?.Invoke(this, EventArgs.Empty);
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
    public Vector2 GetScrollVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Scroll.ReadValue<Vector2>().normalized;
        return inputVector;
    }

}
