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
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
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
    
}
    
