using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{
    public float tick = 0.2f;
    public float damage = 0.1f;
    float time;

    public Vector3 shootDirection;

    private void Start()
    {
        shootDirection = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position + shootDirection * 1.5f, shootDirection);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<HealthComponent>() != null && time > tick)
            {
                hit.collider.gameObject.GetComponent<HealthComponent>().takeDamage(damage);
            }

        }


    }
}
