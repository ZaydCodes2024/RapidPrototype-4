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

    private void OnGUI()
    {
        crosshairStyle.normal.background = showInteractableCrosshair ? interactableCrosshair : normalCrosshair;
        float scale = showInteractableCrosshair ? interactableScale : normalScale;
        Vector2 pivotPoint = new Vector2(crosshairStyle.normal.background.width / 2, crosshairStyle.normal.background.height / 2);
        Vector2 position = new Vector2(Screen.width / 2 - pivotPoint.x * scale, Screen.height / 2 - pivotPoint.y * scale);

        if (!GameInput.Instance.IsGamePaused())
            GUI.DrawTexture(new Rect(position.x, position.y, crosshairStyle.normal.background.width * scale, crosshairStyle.normal.background.height * scale), crosshairStyle.normal.background);
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
            InteractionController.Instance.HandleInteractions(hit);
        }
        else
        {
            showInteractableCrosshair = false;
            InteractionController.Instance.ClearInteractions();
        }
    }
}