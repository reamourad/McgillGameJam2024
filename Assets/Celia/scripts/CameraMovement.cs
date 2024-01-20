using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float delayTime1 = 5f; // Time to move to the opposite side
    public float delayTime2 = 10f; // Time to move to the center
    public Vector3 defensePos = new Vector3 (0, 0, 0);
    public Vector3 offensePos = new Vector3(0, 0, 0);
    public Vector3 middlePos = new Vector3(0, 0, 0);

    private Camera mainCamera;
    private RectTransform canvasRect;

    void Start()
    {
        mainCamera = Camera.main;
        canvasRect = FindObjectOfType<Canvas>().GetComponent<RectTransform>();

        // Invoke the methods with specified delay times
        Invoke("MoveToOppositeSide", delayTime1);
        Invoke("MoveToCenter", delayTime1 + delayTime2);
    }

    void MoveToOppositeSide()
    {
        float targetX = -canvasRect.sizeDelta.x; // Assuming canvas pivot is at (0.5, 0.5)

        // Move the camera to the opposite side
        Vector3 targetPosition = new Vector3(targetX, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = targetPosition;

        // Schedule the next movement
        Invoke("MoveToCenter", delayTime2);
    }

    void MoveToCenter()
    {
        // Center the camera on the canvas
        Vector3 targetPosition = new Vector3(0f, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = targetPosition;

        // Schedule the next movement
        Invoke("MoveToOppositeSide", delayTime1);
    }
}

