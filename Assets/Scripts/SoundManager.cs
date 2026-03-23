using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipRefSO audioClipRefSO;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] SwitchSequencePuzzle switchPuzzle;
    [SerializeField] List<ButtonInteract> buttonInteract;
    [SerializeField] List<CircuitPiecePostion> circuitPiecePostions;
    [SerializeField] List<Switches> switchesList;
    [SerializeField] List<Lock> lockList;
    private float volume = 1f;
    private const string PLAYER_PREFS_SFX_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance {get; private set;}
    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SFX_VOLUME, 1f);
    }
    private void Start()
    {
        playerMovement.OnCrouchUp += PlayerMovement_OnCrouchUp;
        playerMovement.OnCrouchDown += PlayerMovement_OnCrouchDown;
        switchPuzzle.OnScreenChange += SwitchPuzzle_OnScreenChange;
        foreach (ButtonInteract button in buttonInteract)
        {
            button.OnButtonPressed += ButtonInteract_OnButtonPressed;
        }

        foreach (CircuitPiecePostion pieces in circuitPiecePostions)
        {
            pieces.OnCircuitPiecePlace += CircuitPiecePosition_OnCircuitPiecePlace;
        }

        foreach (Switches switches in switchesList)
        {
            switches.OnSwitchPress += Switches_OnSwitchPress;
        }

        foreach (Lock lockItem in lockList)
        {
            lockItem.OnLockUnlocked += Lock_OnLockUnlocked;
        }
    }

    private void SwitchPuzzle_OnScreenChange(object sender, System.EventArgs e)
    {
        switchPuzzle = sender as SwitchSequencePuzzle;
        PlaySound(audioClipRefSO.tvStatic, switchPuzzle.GetTvTransform().position, 0.1f); 
    }

    private void Lock_OnLockUnlocked(object sender, System.EventArgs e)
    {
        Lock locks = sender as Lock;
        PlaySound(audioClipRefSO.lockUnlock, locks.transform.position, volume);
    }

    private void Switches_OnSwitchPress(object sender, System.EventArgs e)
    {
        Switches switches = sender as Switches;
        PlaySound(audioClipRefSO.buttonPress, switches.transform.position, volume);
    }

    private void CircuitPiecePosition_OnCircuitPiecePlace(object sender, System.EventArgs e)
    {
        CircuitPiecePostion circuitPiecePostion = sender as CircuitPiecePostion;
        PlaySound(audioClipRefSO.circuitPiecePlace, circuitPiecePostion.transform.position, volume);
    }

    private void ButtonInteract_OnButtonPressed(object sender, System.EventArgs e)
    {
        ButtonInteract buttonInteract = sender as ButtonInteract;
        PlaySound(audioClipRefSO.buttonPress, buttonInteract.transform.position, volume);
    }

    private void PlayerMovement_OnCrouchDown(object sender, System.EventArgs e)
    {
        playerMovement = sender as PlayerMovement;
        PlaySound(audioClipRefSO.crouchDown, playerMovement.transform.position, volume);
    }

    private void PlayerMovement_OnCrouchUp(object sender, System.EventArgs e)
    {
        playerMovement = sender as PlayerMovement;
        PlaySound(audioClipRefSO.crouchUp, playerMovement.transform.position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClip,position, volume);
    }
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)],position, volume);
    }
    public void PlayInteractSuccessSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefSO.interactSuccess, position, volumeMultiplier * volume);
    }
    public void PlayInteractFailedSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefSO.interactFail, position, volumeMultiplier * volume);
    }
    public void PlayFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefSO.footstep, position, volumeMultiplier * volume);
    }
    public void PlayItemPickupSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipRefSO.itemPickup, position, volumeMultiplier * volume);
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
            volume = 0f;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SFX_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
