using UnityEngine;

public class Inspectable : MonoBehaviour
{
    private GameObject currentObject;
    public Transform inspectPoint;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float rotationSpeed = 15f; 
    public bool isInspecting = false;
    public FpsController controller;
    public void StartInspect(Inspectable obj)
    {
        if (isInspecting) return;

        controller.StartInspect();
        currentObject = obj.gameObject;
        originalPosition = currentObject.transform.position;
        originalRotation = currentObject.transform.rotation;

        currentObject.transform.position = inspectPoint.position;
        currentObject.transform.rotation = inspectPoint.rotation; 
        isInspecting = true;
    }

    public void StopInspect()
    {
        if (!isInspecting) return;

        
        currentObject.transform.position = originalPosition;
        currentObject.transform.rotation = originalRotation;
        controller.StopInspect();
        currentObject = null;
        isInspecting = false;
    }

    private void RotateObject(float rotX, float rotY)
    {
        // Rotating around the X-axis (left/right)
        currentObject.transform.Rotate(Vector3.up, rotX, Space.World);
        // Rotating around the Y-axis (up/down) — you can adjust this if you need different rotation behavior
        currentObject.transform.Rotate(Vector3.right, rotY, Space.World);
    }

    public void HandleInspectionRotation()
    {
        // Get scroll wheel input (positive or negative) to rotate the object
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // If there's scroll input, apply rotation
        if (scrollInput != 0)
        {
            // Apply the scroll wheel input to rotate around the X-axis (left/right) and Y-axis (up/down)
            float rotationAmount = scrollInput * rotationSpeed;
            // Rotate the object based on the scroll input
            RotateObject(rotationAmount, 0);
        }
    }
}
