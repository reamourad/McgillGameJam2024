using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boosters : MonoBehaviour
{
    public float boosterStrength;
    public float boostTime;
    Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //stop boosting after a while
        Invoke("cancelBoosters", boostTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (boosterStrength > 0)
        {
            rb.AddForce(transform.up * boosterStrength);
        }
    }

    public void cancelBoosters()
    {
        boosterStrength = 0;
    }
}
