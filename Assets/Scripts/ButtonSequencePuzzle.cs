using UnityEngine;
using System;
using TMPro;
public class ButtonSequencePuzzle : PuzzleModule
{
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private int[] correctSequence;
    [SerializeField] TextMeshPro correctSequenceText;
    [SerializeField] GameObject indicatorLight;
    [SerializeField] Material solvedMaterial;
    [SerializeField] Material failedMaterial;
    private MeshRenderer indicatorRenderer;
    [SerializeField] private Color highlightColor = Color.cyan; // Color when hovered
    [SerializeField] private Color defaultColor = Color.white; // Default button color
    private int index;
    int buttonInteractId;
    private void Awake()
    {
      RandomizeSequence();
      AssignButtonIDs();
      indicatorRenderer = indicatorLight.GetComponent<MeshRenderer>();
    }
    private void RandomizeSequence()
    {
        for (int i = 0; i < correctSequence.Length; i++)
        {
            correctSequence[i] = UnityEngine.Random.Range(1,5);
        }

        string correctSequenceString = string.Join("", correctSequence);

        if (correctSequenceText != null)
        {
            correctSequenceText.text = correctSequenceString;
        }
    }
    private void AssignButtonIDs()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            ButtonInteract buttonInteract = buttons[i].GetComponent<ButtonInteract>();

            buttonInteractId = buttonInteract.GetButtonID();
            
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
                SoundManager.Instance.PlayInteractSuccessSound(transform.position, 5f);
            }
        }
        else
        {
            index = 0; // Reset if wrong
            indicatorRenderer.material = failedMaterial;
            SoundManager.Instance.PlayInteractFailedSound(transform.position, 2f);
        }
    }
    public Color GetHighlightColor()
    {
        return highlightColor;
    }
}
