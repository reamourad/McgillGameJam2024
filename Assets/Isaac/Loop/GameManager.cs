using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float turnTimer;
    float timer;

    public Camera mainCamera;

    public Transform attackersPosition;
    public Transform defendersPosition;
    Vector3 targetPosition;

    public Canvas UI;
    Canvas currCanva;

    public int stage = 1; //let us represent 1 = attacking set up, 2 = defense set up, 3 = watch them attack
    bool showUI;

    // Start is called before the first frame update
    void Start()
    {
        attackersPosition.position = new Vector3(attackersPosition.position.x, attackersPosition.position.y, mainCamera.transform.position.z);
        defendersPosition.position = new Vector3(defendersPosition.position.x, defendersPosition.position.y, mainCamera.transform.position.z);
        targetPosition = defendersPosition.position;
        mainCamera.transform.position = targetPosition;

        showUI = true;
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //this camera lerping is horrendous
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime);

        if (showUI)
        {
            currCanva = Instantiate(UI);
            currCanva.GetComponent<Canvas>().worldCamera = mainCamera;
            showUI = false;
        }

        if (timer > turnTimer)
        {
            timer = 0;
            stage++;

            if (stage == 2)
            {
                targetPosition = attackersPosition.position;
            } else if (stage == 3)
            {
                targetPosition = (defendersPosition.position + attackersPosition.position) / 2;
            }

            Destroy(currCanva);
            showUI = true;
        }
    }

}
