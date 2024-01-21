using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : MonoBehaviour
{ 

    public float tick = 0.2f;
    public float damage = 0.1f;

    float time;
    LineRenderer laserBeamVFX;


    public Vector3 shootDirection;

    private void Start()
    {
        //init laser;
        laserBeamVFX = GetComponent<LineRenderer>();
        laserBeamVFX.positionCount = 2;

        shootDirection = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        Vector3 laserStart = transform.position + shootDirection * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(laserStart, shootDirection);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<HealthComponent>() != null && time > tick)
            {
                hit.collider.gameObject.GetComponent<HealthComponent>().takeDamage(damage);
            }

            laserBeamVFX.SetPosition(0, laserStart);
            laserBeamVFX.SetPosition(1, hit.point);

            AudioManager.Instance.PlaySFX("QueenLaser");
            
        } else
        {
            laserBeamVFX.SetPosition(1, laserStart + shootDirection * 9999f);
        }


    }
}
