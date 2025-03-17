using UnityEngine;

public class SwitchSequencePuzzle : PuzzleModule
{
    [Header("Switches")]
    public Switches[] switches;
    private bool[] solutionPattern;
    private bool puzzleSolved = false;

    private void Start()
    {
        RandomizeSwitchStates();
        GenerateSolution();
    }
    private void RandomizeSwitchStates()
    {
        foreach (Switches sw in switches)
        {
            bool randomState = Random.value > 0.5f; // 50% chance ON or OFF
            sw.SetState(randomState); // Assuming SetState(bool state) exists in Switches
        }
    }

    private void GenerateSolution()
    {
         solutionPattern = new bool[switches.Length];

        for (int i = 0; i < solutionPattern.Length; i++)
        {
            solutionPattern[i] = Random.value > 0.5f; // 50% chance ON or OFF
        }

        Debug.Log("Generated Solution: " + string.Join(", ", solutionPattern));
    }

    // Toggle the switch state and update the visual feedback
    public void ToggleSwitch(Switches switchScript)
    {   
        // Update the state and feedback of the switch
        CheckPuzzleSolution();
    }


    // Check if the switches match the solution pattern
    void CheckPuzzleSolution()
    {   
        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i].GetState() != solutionPattern[i])
            {
                return; // If any switch is incorrect, the puzzle is not solved
            }
        }

        if (!puzzleSolved)
        {
            puzzleSolved = true;    
            Solve();
            // Trigger the puzzle success (e.g., opening a box, activating a mechanism)
            Debug.Log("Puzzle Solved!");
        }
    }
}
