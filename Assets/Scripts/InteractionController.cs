using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public static InteractionController Instance {get; private set;}
    private IInteractable currentInteractable;
    private CircuitPiecePostion circuitPiecePosition;
    private Switches switches;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnMouseButtonAction += GameInput_OnMouseButtonAction;
        GameInput.Instance.OnMouseScrollAction += GameInput_OnMouseScrollAction;
    }

    private void GameInput_OnMouseScrollAction(object sender, EventArgs e)
    {
        if (Inspectable.Instance == null) return;
        
        // Allow rotation while inspecting the object
        if (Inspectable.Instance.GetInspectingState())
        {
            Inspectable.Instance.HandleRotation();
        }
    }

    private void GameInput_OnMouseButtonAction(object sender, EventArgs e)
    {
        circuitPiecePosition?.SnapToCorrectPosition();
        switches?.ToggleState();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())  return;
        
        if (Inspectable.Instance != null && Inspectable.Instance.GetInspectingState())
        {
            Inspectable.Instance.StopInspect();
            return;
        }

        currentInteractable?.Interact();

    }
    public void HandleInteractions(RaycastHit hit)
    {
        currentInteractable = null;
        this.circuitPiecePosition = null;
        this.switches = null;

        if (hit.transform.TryGetComponent(out IInteractable interactable))
        {
            currentInteractable = interactable;
        }

        if (hit.transform.TryGetComponent(out CircuitPiecePostion circuitPiecePosition))
        {
            this.circuitPiecePosition = circuitPiecePosition;
        }

        if (hit.transform.TryGetComponent(out Switches switches))
        {
            this.switches = switches;
        }
    }
    public void ClearInteractions()
    {
        currentInteractable = null;
        circuitPiecePosition = null;
        switches = null;
    }
}
