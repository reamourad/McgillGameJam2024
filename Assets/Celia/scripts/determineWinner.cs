using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class determineWinner : MonoBehaviour
{
    private float scorePlayer1;
    private float scorePlayer2;

    public TMP_Text scoreText;
    public TMP_Text player1;
    public TMP_Text player2;

    // Start is called before the first frame update
    void Start()
    {
        scorePlayer1 = GameManager.scorePlayer1;
        scorePlayer2 = GameManager.scorePlayer2;
        if (scorePlayer2 < scorePlayer1)
        {
            scoreText.text = "White Wins";
        }
        else
        {
            scoreText.text = "Black Wins";
        }
        player1.text = "White score: " + scorePlayer1;
        player2.txt = "Black score: " + scorePlayer2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
