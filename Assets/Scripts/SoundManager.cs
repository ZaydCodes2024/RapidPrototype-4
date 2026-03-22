using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClipRefSO audioClipRefSO;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] List<ButtonInteract> buttonInteract;
    [SerializeField] List<CircuitPiecePostion> circuitPiecePostions;
    [SerializeField] List<Switches> switchesList;
    [SerializeField] List<Lock> lockList;
    private float volume = 1f;
    public static SoundManager Instance {get; private set;}
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerMovement.OnCrouchUp += PlayerMovement_OnCrouchUp;
        playerMovement.OnCrouchDown += PlayerMovement_OnCrouchDown;

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

    private void Lock_OnLockUnlocked(object sender, System.EventArgs e)
    {
        Lock locks = sender as Lock;
        PlaySound(audioClipRefSO.lockUnlock, locks.transform.position);
    }

    private void Switches_OnSwitchPress(object sender, System.EventArgs e)
    {
        Switches switches = sender as Switches;
        PlaySound(audioClipRefSO.buttonPress, switches.transform.position);
    }

    private void CircuitPiecePosition_OnCircuitPiecePlace(object sender, System.EventArgs e)
    {
        CircuitPiecePostion circuitPiecePostion = sender as CircuitPiecePostion;
        PlaySound(audioClipRefSO.circuitPiecePlace, circuitPiecePostion.transform.position);
    }

    private void ButtonInteract_OnButtonPressed(object sender, System.EventArgs e)
    {
        ButtonInteract buttonInteract = sender as ButtonInteract;
        PlaySound(audioClipRefSO.buttonPress, buttonInteract.transform.position);
    }

    private void PlayerMovement_OnCrouchDown(object sender, System.EventArgs e)
    {
        PlayerMovement player = sender as PlayerMovement;
        PlaySound(audioClipRefSO.crouchDown, player.transform.position);
    }

    private void PlayerMovement_OnCrouchUp(object sender, System.EventArgs e)
    {
        PlayerMovement player = sender as PlayerMovement;
        PlaySound(audioClipRefSO.crouchUp, player.transform.position);
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
}
