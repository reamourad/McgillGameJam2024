using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public float damage = 0.5f;
    public float tick = 1f;
    float time;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > tick)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, Mathf.Sqrt(1.5f * 1.5f + 1.5f * 1.5f));

            foreach (Collider2D c in col)
            {
                HealthComponent h = c.gameObject.GetComponent<HealthComponent>();

                //c.transform.parent != transform.parent???
                if (h != null && c.gameObject != this.gameObject && c.transform.parent != transform.parent)
                {
                    h.takeDamage(damage);
                }
            }

            time = 0;
        }

    }

}
