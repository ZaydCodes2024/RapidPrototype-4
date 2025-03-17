using UnityEngine;

public class Switches : MonoBehaviour
{
    private bool isOn = false;
    private Renderer switchRenderer;
    public SwitchSequencePuzzle switchpuzzleManager;

    [Header("Feedback Materials")]
    public Material onMaterial;
    public Material offMaterial;
    // Toggle the state of the switch
    private void Start()
    {
        switchRenderer = GetComponent<Renderer>();
        UpdateVisual();
    }
    public void ToggleState()
    {
        isOn = !isOn;
        UpdateVisual();

        Debug.Log("Switch toggled: " + (isOn ? "ON" : "OFF"));
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
