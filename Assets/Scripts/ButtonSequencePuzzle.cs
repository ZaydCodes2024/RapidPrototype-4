using UnityEngine;

public class ButtonSequencePuzzle : PuzzleModule
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] int[] correctSequence = { 1, 3, 2 , 4};
    [SerializeField] GameObject indicatorLight;
    [SerializeField] Material solvedMaterial;
    private MeshRenderer indicatorRenderer;
    [SerializeField] private Color highlightColor = Color.cyan; // Color when hovered
    [SerializeField] private Color defaultColor = Color.white; // Default button color
    private int index;

    private void Awake()
    {
      AssignButtonIDs();
      indicatorRenderer = indicatorLight.GetComponent<MeshRenderer>();
    }
    private void AssignButtonIDs()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonInteract buttonInteract = buttons[i].GetComponent<ButtonInteract>();

            int buttonInteractId = buttonInteract.GetButtonID();
            
            buttonInteractId = i + 1;
            buttonInteract.SetButtonID(buttonInteractId);
        }
    }
    public void PressButton(int buttonID)
    {   
        if (GetSolvedState() || index >= correctSequence.Length) return;

        if (buttonID == correctSequence[index])
        {
            index++;
            if (index >= correctSequence.Length)
            {
                Solve(); // Puzzle is solved when sequence is completed
                indicatorRenderer.material = solvedMaterial;
            }
        }
        else
        {
            index = 0; // Reset if wrong
        }
    }
    public Color GetHighlightColor()
    {
        return highlightColor;
    }
}
