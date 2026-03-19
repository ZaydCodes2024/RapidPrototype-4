using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspectable : MonoBehaviour
{
    public static event EventHandler OnEnterInspect;
    public static event EventHandler OnExitInspect;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float rotationSpeed = 15f;
    private bool isInspecting; 
    public void StartInspect(Transform inspectPoint)
    {
        if (isInspecting) return;

        isInspecting = true;

        originalPosition = transform.position;
        originalRotation = transform.rotation;

        transform.position = inspectPoint.position;
        transform.rotation = inspectPoint.rotation; 
        OnEnterInspect?.Invoke(this, EventArgs.Empty);
    }

    public void StopInspect()
    {
        if (!isInspecting) return;

        isInspecting = false;

        transform.position = originalPosition;
        transform.rotation = originalRotation;
        OnExitInspect?.Invoke(this, EventArgs.Empty);
    }

    private void RotateObject(float rotX, float rotY)
    {
        // Rotating around the X-axis (left/right)
        transform.Rotate(Vector3.up, rotX, Space.World);
        // Rotating around the Y-axis (up/down) — you can adjust this if you need different rotation behavior
        transform.Rotate(Vector3.right, rotY, Space.World);
    }

    public void HandleRotation()
    {
        if (!isInspecting) return;
        
        // Get scroll wheel input (positive or negative) to rotate the object
        float scrollInput = GameInput.Instance.GetScrollVectorNormalized().y;

        // If there's scroll input, apply rotation
        if (scrollInput != 0)
        {
            // Apply the scroll wheel input to rotate around the X-axis (left/right) and Y-axis (up/down)
            float rotationAmount = scrollInput * rotationSpeed;
            // Rotate the object based on the scroll input
            RotateObject(rotationAmount, 0);
        }
    }
    public bool GetInspectingState()
    {
        return isInspecting;
    }
}
