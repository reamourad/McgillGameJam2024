using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;
    public float value;

    public void Start()
    {
        currHealth = maxHealth;
    }

    public void takeDamage(float damage)
    {
        currHealth -= damage;
        //Debug.Log(currHealth);

        if (currHealth <= 0)
        {
            Destroy(gameObject);
            if (GameObject.Find("GameManager").GetComponent<GameManager>().isPlayer1 == true)
            {
                GameManager.scorePlayer1 += value;
                Debug.Log("score is: "+ GameManager.scorePlayer1);
            }
            else
            {
                GameManager.scorePlayer2 += value;
                Debug.Log("score is: " + GameManager.scorePlayer2);
            }

            AudioManager.Instance.PlaySFX("PawnDeath");

        } else
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, (byte) ((currHealth / maxHealth) * 255), (byte) ((currHealth / maxHealth) * 255), 255);
            Debug.Log( (currHealth/maxHealth) * 255);
        }

    }
}
