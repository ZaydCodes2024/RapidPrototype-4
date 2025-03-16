using UnityEngine;

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
    [Space]
    [Header("Interactable Crosshair Triggers")]
    [SerializeField] LayerMask interactableLayerMask; 

    private GUIStyle crosshairStyle = new GUIStyle();
    private bool showInteractableCrosshair = false;
    private RaycastHit hit;
    void OnGUI()
    {
        crosshairStyle.normal.background = showInteractableCrosshair ? interactableCrosshair : normalCrosshair;
        float scale = showInteractableCrosshair ? interactableScale : normalScale;
        Vector2 pivotPoint = new Vector2(crosshairStyle.normal.background.width / 2, crosshairStyle.normal.background.height / 2);
        Vector2 position = new Vector2(Screen.width / 2 - pivotPoint.x * scale, Screen.height / 2 - pivotPoint.y * scale);

        GUI.DrawTexture(new Rect(position.x, position.y, crosshairStyle.normal.background.width * scale, crosshairStyle.normal.background.height * scale), crosshairStyle.normal.background);
    }

    void Update()
    {
        PerformRayCast();
    }

    void PerformRayCast()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, 4f, interactableLayerMask))
        {
            showInteractableCrosshair = true;
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            Inspectable inspectable = hit.collider.GetComponent<Inspectable>();
            CircuitPiece circuitPiece = hit.collider.GetComponent<CircuitPiece>();

            // If the circuit piece is being hovered over
            if (circuitPiece != null && InventoryManager.instance.HasItem(circuitPiece.circuitPiecePrefab))
            {
                // Change the crosshair to show it's an appropriate placement
                showInteractableCrosshair = true;

                // If interact button is pressed, place the circuit piece in the correct spot
                if (Input.GetMouseButtonDown(0))
                {
                    circuitPiece.SnapToCorrectPosition(); // Call the PlacePiece function of CircuitPiece
                }
            }
            else if (interactable != null)
            {
                // For any other interactable item, use the normal interaction
                if (Input.GetButtonDown("Interact"))
                {
                    interactable.Interact(); // Calls the item's interaction behavior
                }
            }
            else if (inspectable != null)
            {
                // If the player presses the 'I' key, start inspecting the object
                if (Input.GetKeyDown(KeyCode.I))
                {
                    inspectable.StartInspect(inspectable);
                }

                // Allow rotation while inspecting the object
                if (inspectable != null && inspectable.isInspecting)
                {
                    inspectable.HandleInspectionRotation();
                }
                
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    inspectable.StopInspect();
                }
            }
        }
        else
        {
            showInteractableCrosshair = false;
        }
    }
}