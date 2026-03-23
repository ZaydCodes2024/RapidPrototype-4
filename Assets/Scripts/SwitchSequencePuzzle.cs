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
    [SerializeField] Renderer tvScreenRenderer;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;
    [SerializeField] private Material defaultMaterial;
    private MeshRenderer indicatorRenderer;
    [SerializeField] private float onDuration = 0.6f;
    [SerializeField] private float offDuration = 0.3f;
    [SerializeField] private float gapDuration = 0.4f;
    private int currentIndex;
    private float playbackTimer;
    private bool isGap;
    private bool isPlaying;
    private void Start()
    {
        RandomizeSwitchStates();
        GenerateSolution();
        indicatorRenderer = indicatorLight.GetComponent<MeshRenderer>();
    }
    private void Update()
    {
        if (!isPlaying) return;

        playbackTimer -= Time.deltaTime;

        if (playbackTimer <= 0f)
        {
            AdvancePlayback();
        }
    }

    private void AdvancePlayback()
    {
        if (currentIndex >= solutionPattern.Length)
        {
            // restart loop
            currentIndex = 0;
            playbackTimer = 2f; // pause before repeating
            return;
        }

        if (isGap)
        {
            // show actual signal
            bool state = solutionPattern[currentIndex];

            tvScreenRenderer.material.color = state ? onMaterial.color : offMaterial.color; 

            playbackTimer = state ? onDuration : offDuration;
            isGap = false;
        }
        else
        {
            // blank gap between signals

            playbackTimer = gapDuration;
            tvScreenRenderer.material.color = defaultMaterial.color;
            currentIndex++;
            isGap = true;
        }
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
            Debug.Log(solutionPattern[i]); 
        }
        
        StartPlayback();
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
    private void StartPlayback()
    {
        currentIndex = 0;
        playbackTimer = 0f;
        isGap = false;
        isPlaying = true;
    }
}
