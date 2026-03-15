using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set;}
    public event EventHandler OnInteractAction;
    public event EventHandler OnMouseButtonAction;
    InputActions playerInputActions;
    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Interact.performed += Interact_Performed;
        playerInputActions.Player.MouseButton.performed += MouseButton_Performed;
    }

    private void MouseButton_Performed(InputAction.CallbackContext context)
    {
        OnMouseButtonAction?.Invoke(this,EventArgs.Empty);
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
