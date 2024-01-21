using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public Transform spawnReference;

    public float exitForce;
    public float aimAngle;
    public float fireRate;
    float time;

    void Start()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        Vector3 center = (gm.attackersPosition.position + gm.defendersPosition.position)/ 2;

        if (transform.parent.transform.position.x > center.x)
        {
            aimAngle += 180;
            spawnReference.transform.localPosition = new Vector3(-spawnReference.transform.localPosition.x, spawnReference.transform.localPosition.y, spawnReference.transform.localPosition.z);
        } 
         
        spawnReference.localEulerAngles = new Vector3(0, 0, -aimAngle); //assuming rotations are clockwise from the x axis
        
    }

    public void Update()
    {

        //simple implementation to call shoot function every "fire rate" seconds
        time += Time.deltaTime;

        if (time > fireRate)
        {
            Shoot();
            time = 0;
        }
    }

    void Shoot()
    {
        //set exit velocity and any other properties here
        GameObject instatiatedProjectile = Instantiate(projectile, spawnReference.position, spawnReference.rotation);
        instatiatedProjectile.GetComponent<ProjectileMovement>().exitForce = exitForce;
    }
}
