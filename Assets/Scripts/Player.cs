using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity;
    private bool isCrouching;
    private float currentHeight;
    private float lerpSpeed = 10f;
    private float crouchHeight = 0.25f;
    private float crouchSpeed = 2.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        GameInput.Instance.OnCrouchAction += GameInput_OnCrouchAction;
    }

    private void GameInput_OnCrouchAction(object sender, EventArgs e)
    {
        if (Inspectable.Instance != null && Inspectable.Instance.GetInspectingState())  return;

        isCrouching = !isCrouching;
    }

    // Update is called once per frame
    private void Update()
    {
        
        HandleMovement();
        HandleMouseLook();
    }
    private void HandleMovement()
    {
        if (Inspectable.Instance != null && Inspectable.Instance.GetInspectingState())     return;

        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten them so looking up/down doesn't move the player vertically
        forward.y = 0f;
        right.y = 0f;

        Vector3 moveDir = forward * inputVector.y + right * inputVector.x;

        float moveSpeed;

        if (isCrouching)
        {
            moveSpeed = crouchSpeed;
        }
        else
        {
            moveSpeed = GameInput.Instance.GetMovementSpeed(runSpeed,walkSpeed);
        }

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 1f;

        currentHeight = isCrouching ? crouchHeight : playerHeight;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * currentHeight, playerRadius, moveDir, moveDistance);
        
        if (!canMove) // Cannot move towards moveDir
        {
            Vector3 moveDirX = new Vector3(moveDir.x,0,0).normalized; // Attempt only X movement
            canMove = ( moveDir.x < -0.5f || moveDir.x > 0.5f ) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * currentHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX; // Can move only on the X
            }
            else // Cannot move only on the X
            {
                Vector3 moveDirZ = new Vector3(0,0,moveDir.z).normalized; // Attempt only Z movement
                canMove =  ( moveDir.z < -0.5f || moveDir.z > 0.5f ) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * currentHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)    
                {
                    moveDir = moveDirZ; // Can move only on the Z
                }
                else // Cannot move in any direction
                {
                    
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        float targetHeight = isCrouching ? crouchHeight : playerHeight;
        cameraTransform.localPosition = Vector3.Slerp(cameraTransform.localPosition, new Vector3(0, targetHeight, 0), Time.deltaTime * lerpSpeed);

    }
    private void HandleMouseLook()
    {
        float mouseX = Mouse.current.delta.ReadValue().x * mouseSensitivity;
        float mouseY = Mouse.current.delta.ReadValue().y  * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        Vector3 currentRotation = cameraTransform.rotation.eulerAngles;
        float desiredRotationX = currentRotation.x - mouseY;

        if (desiredRotationX > 180) desiredRotationX -= 360;

        desiredRotationX = Mathf.Clamp(desiredRotationX, -90f, 90f);
        cameraTransform.rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);
    }
}
