using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepletedMethods : MonoBehaviour
{
    bool useGravity;
    float mass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// from rigidbody 2d
    /// applies gravity to the object *******DEPLETED
    /// </summary>
    /// <returns></returns> returns mass times gravity constant (MIGHT HAVE TO REMOVE THE TIME.DELTATIME)
    ///                     else returns nothing
    private Vector2 ComputeForce()
    {
        if (useGravity)
        {
            return new Vector2(0, mass * -.981f * Time.deltaTime);
        }
        else return Vector2.zero;
    }


    //FROM  MyRBGTest
    /**
    // dont think this is needed anymore
    private void CollisionCalc(MyRGBTest c)
    {
        float v1i = velocity.x;
        float v2i = c.velocity.x;
        float v1f, v2f;
        // calculate intial momentum
        float p1i = v1i * mass;
        float p2i = v2i * c.mass;
        // v1f = (((m1-m2)/(m1+m2))*v1)+(((2*m2)/(m1+m2))*v2)
        v1f = (((mass - c.mass) / (mass + c.mass)) * v1i) + (((2 * c.mass) / (mass + c.mass)) * v2i);
        v2f = (((2 * mass) / (mass + c.mass)) * v1i) - (((mass - c.mass) / (mass + c.mass)) * v2i);
        velocity = new Vector3(v1f, velocity.y);
        c.velocity = new Vector3(v2f, c.velocity.y);


        //TODO: FIX THE 2D PORTION THE 1D IS WORKING BUT THIS ISNT
        v1i = velocity.y;
        v2i = c.velocity.y;
        // calculate intial momentum
        //p1i = v1i * mass;
        //p2i = v2i * c.mass;
        // v1f = (((m1-m2)/(m1+m2))*v1)+(((2*m2)/(m1+m2))*v2)
        v1f = (((mass - c.mass) / (mass + c.mass)) * v1i) + (((2 * c.mass) / (mass + c.mass)) * v2i);
        v2f = (((2 * mass) / (mass + c.mass)) * v1i) - (((mass - c.mass) / (mass + c.mass)) * v2i);
        velocity = new Vector3(velocity.x, v1f);
        c.velocity = new Vector3(c.velocity.x, v2f);

        
    }
    **/


    /// <summary>
    /// computes the angle between two velocities
    /// </summary>
    /// <returns></returns>
    //private float ComputeAngle(Vector3 obj1, Vector3 obj2)
    //{

    //    float x1 = obj1.x;
    //    float y1 = obj1.y;
    //    float x2 = obj2.x;
    //    float y2 = obj2.y;

    //    float top = x1 * x2 + y1 * y2;
    //    float bottom1 = Mathf.Sqrt(Mathf.Pow(x1, 2) * Mathf.Pow(y1, 2));
    //    float bottom2 = Mathf.Sqrt(Mathf.Pow(x2, 2) * Mathf.Pow(y2, 2));
    //    float bot = bottom1 * bottom2;
    //    return Mathf.Acos(top / bot);

    //}


    // FROM MYCIRCLECOLLIDER2D
    //public override bool CollisionCheck()
    //{
    //    foreach (MyCollider2D c in allColliders)
    //    {
    //        if (c == this)
    //        {
    //            continue;
    //        }
    //        else if (CollisionCheckHelp(c))
    //        {

    //            collisionObj = c;

    //            return true;
    //        }
    //    }
    //    return false;
    //}


    //private bool CollisionCheckHelp(MyCollider2D c)
    //{
    //    if(c.type == "Circle")
    //    {
    //        return CirlceOnCircleCollisionCheck(c.GetComponent<MyCircleCollider2D>());
    //    }
    //    if(c.type == "Box")
    //    {
    //        return CircleOnBoxCollisionCheck(c.GetComponent<MyBoxCollider2D>());
    //    }

    //    return false;

    //}

    //// TODO: implement either the sort and sweep algorithm or dynamic bouding volume trees algorithm
    //// to make this much more effiecnt

    ///// <summary>
    ///// checks if two circle colliders are touching
    ///// </summary>
    ///// <param name="c2"></param> other object to test
    ///// <returns></returns> true if the two objects are touching
    //private bool CirlceOnCircleCollisionCheck(MyCircleCollider2D c2)
    //{
    //    MyCircleCollider2D c1 = this;
    //    // get position of the two objects
    //    Vector3 c1Pos = c1.transform.position;
    //    Vector3 c2Pos = c2.transform.position;

    //    float x = c1Pos.x - c2Pos.x;
    //    float y = c1Pos.y - c2Pos.y;
    //    // take the distance^2 and see if the radius^2 is greater than that
    //    // if so touching
    //    float disSquared = x * x + y * y;
    //    float rad = c1.radius + c2.radius;
    //    float rdSquared = rad * rad;

    //    return disSquared <= rdSquared;
    //}

    ///// <summary>
    ///// checks if a this (a circle) is touching a box
    ///// </summary>
    ///// <param name="bc"></param> box obj
    ///// <returns></returns> true if colliding
    //public bool CircleOnBoxCollisionCheck(MyBoxCollider2D bc)
    //{
    //    // THIS ONLY WORKS ON NON ROTATED BOXES
    //    float width = bc.sizeX;
    //    float height = bc.sizeY;

    //    Vector3 cPos = transform.position;
    //    Vector3 bPos = bc.transform.position;

    //    float x = Mathf.Abs(cPos.x - bPos.x);
    //    float y = Mathf.Abs(cPos.y - bPos.y);

    //    bool xBool = (x <= radius + width / 2);
    //    bool yBool = (y <= radius + height / 2);
    //    // then the two are overlapping on the x and y radius
    //    return xBool && yBool;

    //}

}
