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
        transform.localEulerAngles = new Vector3(0, 0, -aimAngle); //assuming rotations are clockwise from the x axis
    }

    public void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, -aimAngle);

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
