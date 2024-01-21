using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{ 

    public float tick = 0.2f;
    public float damage = 0.1f;

    float time;
    LineRenderer laserBeamVFX;

    public Transform laserStart;
    public Vector3 shootDirection;

    private void Start()
    {
        //init laser;
        laserBeamVFX = GetComponent<LineRenderer>();
        laserBeamVFX.positionCount = 2;

        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Vector3 center = (gm.attackersPosition.position + gm.defendersPosition.position) / 2;

        if (transform.parent.transform.position.x > center.x)
        {
            shootDirection = -transform.right;
            
        } else
        {
            shootDirection = transform.right;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        laserBeamVFX.SetPosition(0, laserStart.position);
        RaycastHit2D hit = Physics2D.Raycast(laserStart.position, shootDirection);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<HealthComponent>() != null && time > tick)
            {
                hit.collider.gameObject.GetComponent<HealthComponent>().takeDamage(damage);
            }

            laserBeamVFX.SetPosition(1, hit.point);
            
        } else
        {
            laserBeamVFX.SetPosition(1, laserStart.position + shootDirection * 9999f);
        }


    }
}
