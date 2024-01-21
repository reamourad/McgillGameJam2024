using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float turnTimer;
    public float fightTimer;
    float timer;

    public Camera mainCamera;

    public Transform attackersPosition;
    public Transform defendersPosition;
    Vector3 targetPosition;

    public Canvas UI;
    Canvas currCanva;

    public int stage = 1; //let us represent 1 = defense set up, 2 = attacking set up, 3 = watch them attack
    bool showUI;

    [HideInInspector] public bool isPlayer1 = true;
    [HideInInspector] public float scorePlayer1 = 0;
    [HideInInspector] public float scorePlayer2 = 0;

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

        if (showUI && Vector2.Distance(mainCamera.transform.position, targetPosition) < 0.5f)
        {
            currCanva = Instantiate(UI, mainCamera.transform);
            currCanva.GetComponent<Canvas>().worldCamera = mainCamera;
            showUI = false;
        }


        if (stage == 3 && timer == fightTimer)
        {
            if (isPlayer1 == true)
            {
                stage = 1;
                timer = 0;
                isPlayer1 = false;
            }
            else
            {
                Debug.Log("Score of player 1 is: " + scorePlayer1);
                Debug.Log("Score of player 1 is: " + scorePlayer2);
            }
        }
        else if (timer > turnTimer)
        {
            timer = 0;
            stage++;

            if (stage == 2)
            {
                targetPosition = attackersPosition.position;
                showUI = true;
            } else if (stage == 3)
            {
                targetPosition = (defendersPosition.position + attackersPosition.position) / 2;
            }

            currCanva.enabled = false;
        }
    }

}
