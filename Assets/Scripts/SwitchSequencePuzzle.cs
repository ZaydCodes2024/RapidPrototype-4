using UnityEngine;
using TMPro;
using System.Linq;
using System;
public class SwitchSequencePuzzle : PuzzleModule
{
    [Header("Switches")]
    public Switches[] switches;
    private int[] solutionPattern;
    [SerializeField] GameObject indicatorLight;
    [SerializeField] Material solvedMaterial;
    [SerializeField] Renderer tvScreenRenderer;
    [SerializeField] private Color[] emissionPalette;
    [SerializeField] private float emissionIntensity;
    private MeshRenderer indicatorRenderer;
    [SerializeField] private float onDuration = 0.6f;
    [SerializeField] private float gapDuration = 0.4f;
    private int currentIndex;
    private float playbackTimer;
    private bool isGap;
    private bool isPlaying;
    private Material tvMaterial;
    public event EventHandler OnScreenChange;
    private void Awake()
    {
        tvMaterial = tvScreenRenderer.material;
        tvMaterial.EnableKeyword("_EMISSION");
    }
    private void Start()
    {
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
    private void GenerateSolution()
    {
        solutionPattern = new int[switches.Length];

        for (int i = 0; i < solutionPattern.Length; i++)
        {
            solutionPattern[i] = UnityEngine.Random.Range(0, emissionPalette.Length);
        }
        
        StartPlayback();
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
            int colorIndex = solutionPattern[currentIndex];

            Color baseColor = emissionPalette[colorIndex];
            Color emissionColor = GetEmissionColor(baseColor, emissionIntensity);
            SetEmission(emissionColor);
            SetBaseColor(baseColor);

            playbackTimer = onDuration;
            isGap = false;
        }
        else
        {
            // blank gap between signals
            TurnOffEmission();
            playbackTimer = gapDuration;
            currentIndex++;
            isGap = true;
        }
    }
    private void SetEmission(Color color)
    {
        tvMaterial.SetColor("_EmissionColor", color);
        OnScreenChange?.Invoke(this, EventArgs.Empty);
    }
    private void SetBaseColor(Color color)
    {
        tvMaterial.SetColor("_Color", color);
    }
    private void TurnOffEmission()
    {
        tvMaterial.SetColor("_EmissionColor", Color.black);
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
            if (switches[i].GetColorIndex() != solutionPattern[i])
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

    public Transform GetTvTransform()
    {
        return tvScreenRenderer.gameObject.transform;
    }
}
