using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshairs : MonoBehaviour
{   
    [Header("Camera")]
    [SerializeField] Camera playerCamera;

    [Header("Main Crosshair")]
    [SerializeField] Texture2D normalCrosshair;
    [SerializeField] float normalScale = 1f;

    [Space]

    [Header("Interactable Crosshair")]
    [SerializeField] Texture2D interactableCrosshair; 
    [SerializeField] float interactableScale = 1.5f;
    [SerializeField] float interactionDistance = 4f;

    [Space]

    [Header("Interactable Crosshair Triggers")]
    [SerializeField] LayerMask interactableLayerMask; 

    private GUIStyle crosshairStyle = new GUIStyle();
    private bool showInteractableCrosshair = false;
    private RaycastHit hit;
    private IInteractable currentInteractable = null;
    private CircuitPiecePostion circuitPiecePosition = null;
    private Switches switches = null;
    private PuzzleBox puzzleBox = null;
    private void OnGUI()
    {
        crosshairStyle.normal.background = showInteractableCrosshair ? interactableCrosshair : normalCrosshair;
        float scale = showInteractableCrosshair ? interactableScale : normalScale;
        Vector2 pivotPoint = new Vector2(crosshairStyle.normal.background.width / 2, crosshairStyle.normal.background.height / 2);
        Vector2 position = new Vector2(Screen.width / 2 - pivotPoint.x * scale, Screen.height / 2 - pivotPoint.y * scale);

        GUI.DrawTexture(new Rect(position.x, position.y, crosshairStyle.normal.background.width * scale, crosshairStyle.normal.background.height * scale), crosshairStyle.normal.background);
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnMouseButtonAction += GameInput_OnMouseButtonAction;
        GameInput.Instance.OnMouseScrollAction += GameInput_OnMouseScrollAction;
    }

    private void GameInput_OnMouseScrollAction(object sender, EventArgs e)
    {
        puzzleBox?.Interact();  // Allow rotation while inspecting the object
    }

    private void GameInput_OnMouseButtonAction(object sender, EventArgs e)
    {
        circuitPiecePosition?.SnapToCorrectPosition(); // If mouse button is pressed, place the circuit piece in the correct spot
        switches?.ToggleState();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        currentInteractable?.Interact();
    }

    private void Update()
    {
        PerformRayCast();
    }

    void PerformRayCast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayerMask))
        {
            showInteractableCrosshair = true;

            currentInteractable = hit.collider.GetComponent<IInteractable>();
            puzzleBox = hit.collider.GetComponent<PuzzleBox>();
            circuitPiecePosition = hit.collider.GetComponent<CircuitPiecePostion>();
            switches = hit.collider.GetComponent<Switches>();

            if (puzzleBox != null)
            {
                // If the player presses the 'I' key, start inspecting the object
                if (Keyboard.current.iKey.wasPressedThisFrame)    puzzleBox.StartInspect();

                if (Keyboard.current.escapeKey.wasPressedThisFrame)    puzzleBox.ExitInspect();
            }
        }
        else
        {
            showInteractableCrosshair = false;
            currentInteractable = null;
            circuitPiecePosition = null;
            switches = null;
        }
    }
}