using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class with some utillity functions for 
/// physics
/// </summary>
public static class RGBUtil
{
    // computes the angle between two collision objects
    // only called if two objects already collideded
    public static void ComputeAngle(MyCollider2D c1, MyCollider2D c2)
    {

        if (c1.type == "Circle" && c2.type == "Circle")
        {
            CirOnCirCol(c1.GetComponent<MyCircleCollider2D>(), c2.GetComponent<MyCircleCollider2D>());
        }
    }

    // need to compute the velocity for the future but DONT CHANGE THE VELOCITY
    // do the rest of the updates by checking the now states NOT THE FUTURE
    // after all objects are done update to future
    public static void CirOnCirCol(MyCircleCollider2D c1, MyCircleCollider2D c2)
    {
        //if (c1.GetComponent<MyRGBTest>().velocity == Vector3.zero)
        //{
        //    CirOnCirCol(c2, c1);
        //}


        // get the rigid body components for velocity mainly
        MyRGBTest r1 = c1.GetComponent<MyRGBTest>();
        MyRGBTest r2 = c2.GetComponent<MyRGBTest>();

        // this is the direction the object should bounce away
        // each velocities
        Vector3 v1 = r1.velocity;
        Vector3 v2 = r2.velocity;
        Vector3 v1Dir = Vector3.zero;
        Vector3 v2Dir = Vector3.zero;

        Vector3 c1Pos = c1.transform.position;
        Vector3 c2Pos = c2.transform.position;


        // only works if one isn't moving
        if (r2.velocity == Vector3.zero)
        {

            v2Dir = (c1.transform.position - c2.transform.position).normalized;

            // angle that c2 will bounce away at
            float theta2 = Vector3.Angle(v1, v2Dir);
            v1Dir = Vector3.Reflect(v2Dir, v1).normalized;     // this is the direction of v1'
            float theta1 = Vector3.Angle(v1, v1Dir);


            // different cases need to change the angles
            if (c1Pos.y > c2Pos.y)
            {
                if (c1Pos.x < c2Pos.x)
                {
                    Debug.Log("case1");
                    theta2 -= 180;
                    theta1 -= 180;
                }
                else
                {
                    Debug.Log("case2");
                    theta2 -= 180;
                    theta1 -= 180;
                }

            }

            if (c1Pos.y < c2Pos.y)
            {
                if (c1Pos.x < c2Pos.x)
                {
                    Debug.Log("case3");
                    theta1 -= 180;

                }
                else
                {
                    Debug.Log("case4");
                    theta1 -= 180;
                }

            }


            // Cos returns in rads
            // need to convert to degrees
            
            // taking the absolute value fixed the random direction
            // something about the magnitudes is throwing everything off
            Debug.Log("t2: " + theta2);
            Debug.Log("cos: " + Mathf.Cos(theta2));
            Debug.Log("mag: " + v1.magnitude);
            var v2Finalmag = Mathf.Abs(v1.magnitude / Mathf.Cos(theta2));
            v2 = v2Finalmag * v2Dir;
            var v1Finalmag = Mathf.Abs(v1.magnitude / Mathf.Cos(theta1));
            v1 = v1Finalmag * v1Dir;
            Debug.Log("r1 mag: " + v1Finalmag);
            Debug.Log("r2 mag: " + v2Finalmag);






            if (c1Pos.y > c2Pos.y)
            {
                if (c1Pos.x < c2Pos.x)
                {
                    Debug.Log("case1");
                    //v1.x *= -1;
                    //v1.y *= -1;
                    v2.x *= -1;
                    v2.y *= -1;
                }
                else
                {
                    Debug.Log("case2");
                    //v1.x *= -1;
                    //v1.y *= -1;
                    v2.x *= -1;
                    v2.y *= -1;
                }

            }

            if (c1Pos.y < c2Pos.y)
            {
                if (c1Pos.x < c2Pos.x)
                {
                    Debug.Log("case3");
                    //v1.x *= -1;
                    //v1.y *= -1;
                    v2.x *= -1;
                    v2.y *= -1;
                }
                else
                {
                    Debug.Log("case4");
                    //v1.x *= -1;
                    //v1.y *= -1;
                    v2.x *= -1;
                    v2.y *= -1;
                }

            }

        }


        // sometimes the negative is right sometimes the pos is need some way if telling whether it is or not
        r1.velocity = v1;
        r2.velocity = v2;
        // print out all values prior and after
        // manually do the math and seem if it works

    }

    public static void BoxOnCirCol(MyBoxCollider2D bc, MyCircleCollider2D cc)
    {
        MyRGBTest rb = bc.GetComponent<MyRGBTest>();
        MyRGBTest rc = cc.GetComponent<MyRGBTest>();

        // first kinematic case
        if (bc.GetComponent<MyRGBTest>().isKinematic)
        {
            //TODO: IMPLEMENT THIS TO WORK IN A GENERAL CASE 
            /*
            // calculate the normal of an object

            // use the normal and current velocity to figure the vounce angle
            velocity =  velocity - 2 * Vector2.Dot(velocity, normal) * normal;
            */
            //if (rc.velocity.magnitude != 0)
            //{
            //    if (!rc.isKinematic)
            //    {
            //        // then should add that force
            //        this.velocity += c.velocity;
            //    }

            //}
            // if this is a vertical wall then flip x
            if (rb.wallside == MyRGBTest.Wall.Vertical)
            {
                rc.velocity = new Vector3(-rc.velocity.x * (rc.bounciness - .2f), rc.velocity.y, rc.velocity.z);
            }
            else if (rb.wallside == MyRGBTest.Wall.Horizontal)
            {
                rc.velocity = new Vector3(rc.velocity.x, -rc.velocity.y * (rc.bounciness-.2f), rc.velocity.z);
            }
        }
        
        bc.collisionObj = null;
        cc.collisionObj = null;
        cc.colliding = false;
        bc.colliding = false;
    }


}
