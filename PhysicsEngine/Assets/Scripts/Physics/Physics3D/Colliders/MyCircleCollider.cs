using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCircleCollider : MyCollider
{
    public float radius;
    public int dir;
    // should probably look for all the colliders in the area
    //public MyCollider c1;
    

    // Start is called before the first frame update
    void Start()
    {
        type = "Circle";
    }

    // Update is called once per frame
    void Update()
    {
        //colliders = FindObjectsOfType<MyCollider>();
        //transform.position += Vector3.right * .01f* dir;
     
        //MyOnCollision(c1);


        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }


    // if the radius overlaps then the spheres are touching
    public override bool SphereCheck(MyCircleCollider c)
    {
        // distance < radius + radius
        float xDist = Mathf.Abs(transform.position.x - c.transform.position.x);
        float yDist = Mathf.Abs(transform.position.y - c.transform.position.y);
        float zDist = Mathf.Abs(transform.position.z - c.transform.position.z);

        // also need to say the x y and z are touching
        bool xCond = xDist < radius + c.radius;
        bool yCond = yDist < radius + c.radius;
        bool zCond = zDist < radius + c.radius;
        if (xCond && yCond && zCond)
        {
            return true;
        }
        return false;
    }

    public override bool CubeCheck(MyBoxCollider bx)
    {
        return true;
    }

    public override bool MyOnCollision(MyCollider c)
    {
        if (c.type.Equals("Box"))
        {
            return CubeCheck((MyBoxCollider)c);
        }
        if (c.type.Equals("Circle"))
        {
            return SphereCheck((MyCircleCollider)c);
        }
        return true;
    }
    public void MoveAway()
    {
        // needs to get the direction of the collision and move away from it
        // velocity = -velocity?

    }
   
}
