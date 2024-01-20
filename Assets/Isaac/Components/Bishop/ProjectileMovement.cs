using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float exitForce;

    public bool canExplode;
    public float explosionForce;
    public float explosionRadius;

    public float damage = 1;
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
                if (c == transform.gameObject.GetComponent<Collider2D>())
                    continue;

                Component[] component = c.gameObject.GetComponents(typeof(FixedJoint2D));
                foreach (Component comp in component)
                {
                    Destroy(comp);
                }


                Vector2 dir = (c.gameObject.transform.position - transform.position).normalized;
                c.transform.gameObject.GetComponent<Rigidbody2D>().AddForce(dir * explosionForce);
            }
        }

        if (collision.gameObject.GetComponent<HealthComponent>() != null)
        {
            collision.gameObject.GetComponent<HealthComponent>().takeDamage(damage);
        }

        Invoke("destroyProjectile", 0.5f);
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

    public void destroyProjectile()
    {
        Destroy(gameObject);
    }

}
