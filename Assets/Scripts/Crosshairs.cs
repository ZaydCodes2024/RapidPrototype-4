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

    public static Crosshairs instance;
     void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowInteractableCrosshair(bool show)
    {
        showInteractableCrosshair = show;
    }
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
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out hit, 4f, interactableLayerMask))
        {
            showInteractableCrosshair = true;
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            CircuitPiece circuitPiece = hit.collider.GetComponent<CircuitPiece>();


            // If the circuit piece is being hovered over
            if (circuitPiece != null && InventoryManager.instance.HasItem(circuitPiece.circuitPiecePrefab))
            {
                // Change the crosshair to show it's an appropriate placement
                showInteractableCrosshair = true;

                // If interact button is pressed, place the circuit piece in the correct spot
                if (Input.GetButtonDown("Interact"))
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
            
        }
        else
        {
            showInteractableCrosshair = false;
        }
    }
}