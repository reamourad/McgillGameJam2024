using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float exitForce;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * exitForce);
    }

    //collision box must be alrger than the actual projectile
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.name == "Parent")
            {
                StickComponents sc = collision.gameObject.transform.parent.GetComponent<StickComponents>();
                sc.setComponentBreakForce(sc.jointBreakForce, sc.jointTorqueForce);
            }
        }

    }


}
