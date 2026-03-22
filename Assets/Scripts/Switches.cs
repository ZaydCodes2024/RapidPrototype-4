using System;
using UnityEngine;

public class Switches : MonoBehaviour
{
    public event EventHandler OnSwitchPress;
    private bool isOn = false;
    private Renderer switchRenderer;
    [SerializeField] private SwitchSequencePuzzle switchpuzzleManager;

    [Header("Feedback Materials")]
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;
    // Toggle the state of the switch
    private void Awake()
    {
        switchRenderer = GetComponent<Renderer>();
        UpdateVisual();
    }
    public void ToggleState()
    {
        isOn = !isOn;

        OnSwitchPress?.Invoke(this, EventArgs.Empty);
        
        UpdateVisual();

        if (switchpuzzleManager != null)
        {
            switchpuzzleManager.ToggleSwitch(this);
        }
    }

    // Get the current state of the switch
    public bool GetState()
    {
        return isOn;
    }

    public void SetState(bool state)
    {
        isOn = state;
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        if (switchRenderer != null)
        {
            switchRenderer.material = isOn ? onMaterial : offMaterial;
        }
    }
}
