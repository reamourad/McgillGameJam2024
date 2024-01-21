using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMovement : MonoBehaviour
{

    public float delayTime1 = 3f; // Time to move to the opposite side
    public float delayTime2 = 3f; // Time to move to the center
    public Vector3 offensePos = new Vector3(0, 0, -10);
    public Vector3 middlePos = new Vector3(0, 0, -10);

    public TMP_Text timerText;
    private float timer;

    [HideInInspector] public bool isPlayer1 = true;

    void Start()
    {
        timer = delayTime1;

        // Invoke the methods with specified delay times
        Invoke("MoveToOppositeSide", delayTime1);
    }

    void MoveToOppositeSide()
    {

        // Move the camera to the opposite side
        Camera.main.transform.position = offensePos;

        timer = delayTime2;

        // Schedule the next movement
        Invoke("MoveToCenter", delayTime2);
    }

    void MoveToCenter()
    {
        // Center the camera on the canvas
        Camera.main.transform.position = middlePos;

        if (isPlayer1 == true)
        {
            isPlayer1 = false;
        }

    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer >= 0)
        {
            timerText.text = timer.ToString("F0");
        }
        else if (timer < 0)
        {
            timerText.text = "";
        }

    }
}

