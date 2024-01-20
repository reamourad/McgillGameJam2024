using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickComponents : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> buildingBlocks;
    public float jointBreakForce;
    public float jointTorqueForce;
    public float gridDistance;

    void Start()
    {
        //get all the children contained inside the parent
        foreach (Transform curr in transform)
        {
            buildingBlocks.Add(curr.gameObject);
            foreach (Transform otherChild in transform)
            {
                //ISSUE: double fixed joint with each other
                if (Vector2.Distance(curr.position, otherChild.position) <= gridDistance + 0.05f
                    && otherChild != curr)
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
                    f.breakForce = jointBreakForce;
                    f.breakTorque = jointTorqueForce;
                    f.connectedBody = otherChild.gameObject.GetComponent<Rigidbody2D>();

                    //ISSUE: Oscillation needs to be reduced further
                    f.dampingRatio = 1;
                    f.frequency = 0;
                }
                
            }
        }
    }

    private void Update()
    {
        //very inefficient but, 
        foreach (Transform curr in transform)
        {
            curr.GetComponent<Rigidbody2D>().angularVelocity *= 0.5f;
        }
    }

}
