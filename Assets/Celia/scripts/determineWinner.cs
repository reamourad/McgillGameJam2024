using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class determineWinner : MonoBehaviour
{
    private float scorePlayer1;
    private float scorePlayer2;

    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scorePlayer1 = GameManager.scorePlayer1;
        scorePlayer2 = GameManager.scorePlayer2;
        scoreText.text = "score of player 1 is: " + scorePlayer1 + "score 2 is: " + scorePlayer2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
