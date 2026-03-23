using System;
using UnityEngine;

public class Switches : MonoBehaviour
{
    public event EventHandler OnSwitchPress;
    private int currentColorIndex = -1;
    private Renderer switchRenderer;
    [SerializeField] private SwitchSequencePuzzle switchpuzzleManager;

    [Header("Feedback Materials")]
    private Material switchMaterial;
    [SerializeField] private Color[] emissionPalette;
    [SerializeField] private float emissionIntensity;
    private void Awake()
    {
        switchRenderer = GetComponent<Renderer>();
        switchMaterial = switchRenderer.material;
        switchMaterial.EnableKeyword("_EMISSION");
        currentColorIndex = -1;
        UpdateVisual();
    }
    public void ToggleState()
    {
        if (emissionPalette == null || emissionPalette.Length == 0)     return;

        currentColorIndex++;

        if (currentColorIndex >= emissionPalette.Length)
            currentColorIndex = -1;

        OnSwitchPress?.Invoke(this, EventArgs.Empty);
        
        UpdateVisual();

        if (switchpuzzleManager != null)
        {
            switchpuzzleManager.ToggleSwitch(this);
        }
    }

    // Get the current state of the switch
    public int GetColorIndex()
    {
        return currentColorIndex;
    }
    public void SetColorIndex(int index)
    {
        currentColorIndex = index;
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        if (switchRenderer == null) return;

        if (currentColorIndex == -1)
        {
            switchMaterial.SetColor("_EmissionColor", Color.black);
        }
        else
        {
            Color baseColor = emissionPalette[currentColorIndex];
            Color emissionColor = GetEmissionColor(baseColor, emissionIntensity);
            switchMaterial.SetColor("_Color", baseColor);  
            switchMaterial.SetColor("_EmissionColor", emissionColor);
        }
    }
    private void OnDestroy()
    {
        if (switchMaterial != null)
            Destroy(switchMaterial);
    }
    private Color GetEmissionColor(Color baseColor, float intensity)
    {
        // Convert RGB to HSV
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);

        // Increase value (brightness) without affecting hue or saturation
        v = Mathf.Clamp01(v * intensity);

        // Convert back to RGB
        return Color.HSVToRGB(h, s, v);
    }
}
