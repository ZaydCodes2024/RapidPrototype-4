using UnityEngine;
using TMPro;
using System.Linq;
public class SwitchSequencePuzzle : PuzzleModule
{
    [Header("Switches")]
    public Switches[] switches;
    private bool[] solutionPattern;
    [SerializeField] GameObject indicatorLight;
    [SerializeField] Material solvedMaterial;
    [SerializeField] TextMeshPro solutionText;
    private MeshRenderer indicatorRenderer;

    private void Start()
    {
        RandomizeSwitchStates();
        GenerateSolution();
        indicatorRenderer = indicatorLight.GetComponent<MeshRenderer>();
    }
    private void RandomizeSwitchStates()
    {
        foreach (Switches sw in switches)
        {
            bool randomState = Random.value > 0.5f; // 50% chance ON or OFF
            sw.SetState(randomState); 
        }
    }

    private void GenerateSolution()
    {
        solutionPattern = new bool[switches.Length];

        for (int i = 0; i < solutionPattern.Length; i++)
        {
            solutionPattern[i] = Random.value > 0.5f; 
        }

        string solutionString = string.Join(" ", solutionPattern.Select(b => b ? "ON" : "OFF"));

        if (solutionText != null)
        {
            solutionText.text = "" + solutionString;
        }
    }

    // Toggle the switch state and update the visual feedback
    public void ToggleSwitch(Switches switchScript)
    {   
        CheckPuzzleSolution();
    }


    // Check if the switches match the solution pattern
    void CheckPuzzleSolution()
    {   
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].GetState() != solutionPattern[i])
            {
                return; 
            }
        }

        if (!GetSolvedState())
        {   
            Solve();
            indicatorRenderer.material = solvedMaterial;
            SoundManager.Instance.PlayInteractSuccessSound(transform.position, 5f);
        }
    }
}
