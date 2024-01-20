using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float exitForce;

    public bool canExplode;
    public float explosionForce;
    public float explosionRadius;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * exitForce);
    }

    //collision box must be larger than the actual projectile
    private void OnCollisionEnter2D(Collision2D collision)
    {
        enableStructureBreak(collision.gameObject);

        if (canExplode)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (Collider2D c in col)
            {
                Vector2 dir = -(c.gameObject.transform.position - transform.position);
                c.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * explosionForce);
            }
        }

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
