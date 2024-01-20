using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickComponents : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> buildingBlocks;
    public float jointBreakForce = 100;
    public float jointTorqueForce = 100;
    public float gridDistance = 1.5f;

    public void Start()
    {
        stickChildren();
    }

    public void stickChildren()
    {
        //get all the children contained inside the parent
        foreach (Transform curr in transform)
        {
            Debug.Log(curr.gameObject.name);
            buildingBlocks.Add(curr.gameObject);
            foreach (Transform otherChild in transform)
            {
                //ISSUE: double fixed joint with each other
                if (Vector2.Distance(curr.position, otherChild.position) <= gridDistance + 0.05f
                    && otherChild.position != curr.position)
                {
                    //prevent overlap between fixed joints
                    if (otherChild.GetComponent<FixedJoint2D>() != null)
                    {
                        if (otherChild.GetComponent<FixedJoint2D>().connectedBody == curr.GetComponent<Rigidbody2D>())
                        {
                            continue;
                        }
                    }

                    //adding the fixed joint
                    FixedJoint2D f = curr.gameObject.AddComponent<FixedJoint2D>();
                    f.connectedBody = otherChild.gameObject.GetComponent<Rigidbody2D>();

                    //fixed joint properties
                    f.dampingRatio = 1;
                    f.frequency = 0;

                    /*
                    f.breakForce = jointBreakForce;
                    f.breakTorque = jointTorqueForce;
                    */

                }

            }
        }
    }

    private void Update()
    {
        //very inefficient but, does remove the oscillation issue :3
        foreach (Transform curr in transform)
        {
            curr.GetComponent<Rigidbody2D>().angularVelocity *= 0.5f;
        }
    }

    //set all components to a lower break force when hit by a projectile or explosion for better physics!
    public void setComponentBreakForce (float breakForce, float torqueForce)
    {
        foreach (Transform child in transform)
        {
           FixedJoint2D f = child.GetComponent<FixedJoint2D>();
           if (f != null)
            {
                f.breakForce = breakForce;
                f.breakTorque = torqueForce;
            }
        }

        //reset all components to max break force
        if (breakForce != Mathf.Infinity || torqueForce != Mathf.Infinity)
        {
            Invoke("resetComponentBreakForce", 0.5f);
        }
    }

    public void resetComponentBreakForce ()
    {
        setComponentBreakForce(Mathf.Infinity, Mathf.Infinity);
        Debug.Log("Resent break force components");
    }

}
