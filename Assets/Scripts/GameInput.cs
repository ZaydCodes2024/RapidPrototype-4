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
    public event EventHandler OnMouseScrollAction;
    public event EventHandler OnCrouchAction;
    public event EventHandler OnGamePauseAction;
    public event EventHandler OnGameUnpauseAction;
    InputActions playerInputActions;
    private bool isGamePause = false;
    public enum Binding
    {
        Interact
    }
    private void Awake()
    {
        Instance = this;
        playerInputActions = new InputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Interact.performed += Interact_Performed;
        playerInputActions.Player.MouseButton.performed += MouseButton_Performed;
        playerInputActions.Player.Scroll.performed += Scroll_performed;
        playerInputActions.Player.Crouch.performed += Crouch_Performed;
        playerInputActions.Player.Pause.performed += Pause_Performed;
    }

    private void Pause_Performed(InputAction.CallbackContext context)
    {
        TogglePauseGame();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= Interact_Performed;
        playerInputActions.Player.MouseButton.performed -= MouseButton_Performed;
        playerInputActions.Player.Scroll.performed -= Scroll_performed;
        playerInputActions.Player.Crouch.performed -= Crouch_Performed;
        playerInputActions.Player.Pause.performed -= Pause_Performed;
        playerInputActions.Dispose();
    }
    private void Crouch_Performed(InputAction.CallbackContext context)
    {
        OnCrouchAction?.Invoke(this, EventArgs.Empty);
    }

    private void Scroll_performed(InputAction.CallbackContext context)
    {
        OnMouseScrollAction?.Invoke(this, EventArgs.Empty);
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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Interact:
            return playerInputActions.Player.Interact.GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions);
        }
    }
    public void TogglePauseGame()
    {
        isGamePause = !isGamePause;

        if (isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePauseAction?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpauseAction?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsGamePaused()
    {
        return isGamePause;
    }
}
