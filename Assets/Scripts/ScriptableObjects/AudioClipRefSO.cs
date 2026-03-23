using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] footstep;
    public AudioClip[] interactFail;
    public AudioClip[] crouchUp;
    public AudioClip[] crouchDown;
    public AudioClip[] buttonPress;
    public AudioClip[] lockUnlock;
    public AudioClip circuitPiecePlace;
    public AudioClip interactSuccess;
    public AudioClip itemPickup;
    public AudioClip tvStatic;
}
