using UnityEngine;

public class ButtonSequencePuzzle : PuzzleModule
{
    [SerializeField] GameObject[] buttons;
    [SerializeField] int[] correctSequence = { 1, 3, 2 , 4};
    [SerializeField] GameObject indicatorLight;
    [SerializeField] Material solvedMaterial;
    private MeshRenderer indicatorRenderer;
    public Color highlightColor = Color.cyan; // Color when hovered
    public Color defaultColor = Color.white; // Default button color
    int index = 0;

    void Start()
    {
      AssignButtonIDs();
      indicatorRenderer = indicatorLight.GetComponent<MeshRenderer>();
    }
    void AssignButtonIDs()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonInteract buttonInteract = buttons[i].AddComponent<ButtonInteract>();
            buttonInteract.buttonID = i + 1;
            buttonInteract.puzzle = this;

            MeshRenderer meshRenderer = buttons[i].GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                buttonInteract.buttonRenderer = meshRenderer;
                buttonInteract.defaultMaterial = meshRenderer.material; // Store original material
            }
        }
    }
    public void PressButton(int buttonID)
    {   
        if (isSolved || index >= correctSequence.Length) return;

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
}
