using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;}
    [SerializeField] private Transform cameraTransform;
    private PlayerLook playerLook;
    private PlayerMovement playerMovement;
    
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        LockCursorState();
        playerLook = GetComponent<PlayerLook>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        playerMovement.HandleMovement();
        playerLook.HandleMouseLook();
    }
    public Transform GetCameraTransform()
    {
        return cameraTransform;
    }
    public void LockCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UnlockCursorState()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
}
    
