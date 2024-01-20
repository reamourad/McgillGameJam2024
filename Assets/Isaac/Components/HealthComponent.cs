using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float currHealth;
    public float maxHealth;

    public Color[] damageColors;

    public void Start()
    {
        currHealth = maxHealth;
    }

    public void takeDamage(float damage)
    {
        currHealth -= damage;

        if (currHealth < 0)
        {
            Destroy(gameObject);
        } else
        {
            Color color = new Color(currHealth / maxHealth * 255, 0, 0, 255);
            GetComponent<SpriteRenderer>().color = color;
        }

    }
}
