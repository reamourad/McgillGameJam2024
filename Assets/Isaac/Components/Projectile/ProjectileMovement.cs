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

    //collision box must be larger than the actual projectile
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enableStructureBreak(collision.gameObject);
    }

    public void enableStructureBreak(GameObject obj)
    {
        if (obj.transform.parent != null)
        {
            if (obj.transform.parent.name == "Parent")
            {
                StickComponents sc = obj.transform.parent.GetComponent<StickComponents>();
                sc.setComponentBreakForce(sc.jointBreakForce, sc.jointTorqueForce);
            }
        }
    }


}
