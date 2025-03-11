using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivity;
    private CharacterController controller;
    private Vector3 playerVelocity;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = horizontal * transform.right + vertical * transform.forward;
        
        controller.Move(move * moveSpeed * Time.deltaTime); 

        playerVelocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        Vector3 currentRotation = cameraTransform.rotation.eulerAngles;
        float desiredRotationX = currentRotation.x - mouseY;
        if (desiredRotationX > 180)
            desiredRotationX -= 360;
        desiredRotationX = Mathf.Clamp(desiredRotationX, -90f, 90f);
        cameraTransform.rotation = Quaternion.Euler(desiredRotationX, currentRotation.y, currentRotation.z);
    }
}
