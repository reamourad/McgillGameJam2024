using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    public DataManager data;

    public Transform attackersPosition;
    public Transform defendersPosition;
    public List<Transform> parentListing = new List<Transform>();
    
    Vector3 targetPosition;
    float focusedCameraSize = 5f;
    float wideCameraSize = 7f;
    float targetSize;

    public Canvas UI;
    public string[] roundDisplay;
    public TMP_Text roundText;

    public float turnTimer;
    public TMP_Text timerText;
    float timer;

    public TMP_Text playerOneScore;
    public TMP_Text playerTwoScore;

    Canvas currCanva;

    public int stage = 1; //let us represent 1 = attacking set up, 2 = defense set up, 3 = watch them attack
    bool showUI;

    [HideInInspector] public bool isPlayer1 = true;
    [HideInInspector] public static float scorePlayer1 = 0;
    [HideInInspector] public static float scorePlayer2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        attackersPosition.position = new Vector3(attackersPosition.position.x, attackersPosition.position.y, mainCamera.transform.position.z);
        defendersPosition.position = new Vector3(defendersPosition.position.x, defendersPosition.position.y, mainCamera.transform.position.z);
        
        targetPosition = (defendersPosition.position + attackersPosition.position)/2;
        mainCamera.transform.position = targetPosition;
        targetSize = wideCameraSize;

        proceedToNextStage();
    }


    // Update is called once per frame
    void Update()
    {
        playerOneScore.text = "White: " + scorePlayer1.ToString();
        playerTwoScore.text = "Black: " + scorePlayer2.ToString();
       //this camera lerping is horrendous
        if (showUI && Vector2.Distance(mainCamera.transform.position, targetPosition) < 0.5f)
        {
            if (stage != 0 && currCanva != null)
            {
                Destroy(currCanva.gameObject);
            }

            currCanva = Instantiate(UI, mainCamera.transform);

            currCanva.GetComponent<Canvas>().worldCamera = mainCamera;
            showUI = false;

            if (stage == 1 || stage == 4)
            {
                foreach(Transform child in currCanva.transform)
                {
                    if (child.transform.name == "Grid menu")
                    {
                        child.GetComponent<GridMenu>().isDefending = true;
                    }

                    if (child.transform.name == "Block menu")
                    {
                        child.GetComponent<BlocksMenu>().isDefending = true;
                    }
                }
            }

            mainCamera.orthographicSize = (int)mainCamera.orthographicSize;
        } else
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, Time.deltaTime * 2.5f);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * 2.5f);
        }

        //round timer
        if (stage % 3 == 0 && stage >= 3)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                timerText.text = timer.ToString("F0");
            } else
            {
                timer = turnTimer;
                proceedToNextStage();
                timerText.gameObject.SetActive(false);

                //reset map
                foreach (Transform p in parentListing) { Destroy(p.gameObject); }
                parentListing = new List<Transform>();

            }
        }

    }

    public void proceedToNextStage()
    {
        Debug.Log("stage is: " + stage);
        if (stage < 6)
        {
            roundText.text = roundDisplay[stage];
        }

        roundText.transform.parent.gameObject.SetActive(true);
        if (stage == 1 || stage == 4)
        {
            Invoke("setAttacking", 3f);
        }
        else if (stage == 0 || stage == 3)
        {
            if (stage == 3)
            {
                isPlayer1 = false;
            }
            Invoke("setDefending", 3f);

        } else if (stage == 2 || stage == 5)
        {
            Invoke("setCamMiddle",3f);
        }
        else if (stage == 6)
        {
            SceneManager.LoadScene("end");
        }

        
    }

    public void setAttacking()
    {
        targetPosition = attackersPosition.position;
        targetSize = focusedCameraSize;
        showUI = true;

        roundText.transform.parent.gameObject.SetActive(false);

        stage++;
    }

    public void setCamMiddle()
    {
        timerText.gameObject.SetActive(true);
        timer = turnTimer;

        targetPosition = (defendersPosition.position + attackersPosition.position) / 2;
        targetSize = wideCameraSize;

        roundText.transform.parent.gameObject.SetActive(false);
        
        foreach (Transform p in parentListing) { p.gameObject.SetActive(true); }
        stage++;
    }

    public void setDefending()
    {
        targetPosition = defendersPosition.position;
        targetSize = focusedCameraSize;
        showUI = true;

        roundText.transform.parent.gameObject.SetActive(false);

        stage++;
    }

}
