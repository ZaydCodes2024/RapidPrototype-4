using System;
using UnityEngine;
public class ButtonInteract : MonoBehaviour
{
    [SerializeField] private int buttonID;
    [SerializeField] private ButtonSequencePuzzle puzzle;
    [SerializeField] private MeshRenderer buttonRenderer;
    [SerializeField] private Material defaultMaterial;
    private Material highlightMaterial;
    public event EventHandler OnButtonPressed;
    private void Awake()
    {
        buttonRenderer.material = defaultMaterial;
    }
    private void Start()
    {
        // Create a highlight material dynamically
        highlightMaterial = new Material(defaultMaterial);
        highlightMaterial.color = puzzle.GetHighlightColor();
    }
    private void OnMouseDown()
    {
        if (puzzle != null)
        {
            puzzle.PressButton(buttonID);
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
    private void OnMouseEnter()
    {
        if (buttonRenderer != null)
        {
            buttonRenderer.material = highlightMaterial; // Change color on hover
        }
    }

    private void OnMouseExit()
    {
        if (buttonRenderer != null)
        {
            buttonRenderer.material = defaultMaterial; // Reset color when not hovered
        }
    }
    public int GetButtonID()
    {
        return buttonID;
    }
    public void SetButtonID(int buttonID)
    {
        this.buttonID = buttonID;
    }
}
