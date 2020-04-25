using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxCollider2D : MyCollider2D
{
    /// <summary>
    /// this will allow to only check on objects that are close to one another
    /// </summary>
    public class AABB 
    {
        public Vector2 min;
        public Vector2 max;
    }
    public float sizeX = 0;
    public float sizeY = 0;
    private Vector2 size;

    // Start is called before the first frame update
    void Start()
    {
        type = "Box";
        size = new Vector3(sizeX, sizeY);
        //allColliders = FindObjectsOfType<MyCollider2D>();
        // if the size of the three together is 0 than the user never entered anything so just auto fill
        if (sizeX + sizeY  == 0)
        {
            size = GetComponent<Renderer>().bounds.size;
            sizeX = size.x;
            sizeY = size.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //CollisionCheck();
    }



    //// TODO: implement either the sort and sweep algorithm or dynamic bouding volume trees algorithm
    //// to make this much more effiecnt

    ///// <summary>
    ///// Checks if two boxes are overlapping
    ///// Calculates the difference min and max of two AABB and 
    ///// if any of the values are greater than zero not overlapping
    ///// </summary>
    ///// <param name="a"></param> One object
    ///// <param name="b"></param> a second object
    ///// <returns></returns> returns whether or not the two objects overlap
    //private bool TestOverlap(AABB a, AABB b)
    //{
    //    float d1x = b.min.x - a.max.x;
    //    float d1y = b.min.y - a.max.y;
    //    float d2x = a.min.x - b.max.x;
    //    float d2y = a.min.y - b.max.y;
    //    if (d1x > 0|| d1y > 0)
    //        return false;

    //    if (d2x > 0|| d2y > 0)
    //        return false;
    //    return true;
    //}
    ///// <summary>
    ///// checks box on box collision with this and a second box
    ///// </summary>
    ///// <param name="b2"></param> second collider
    ///// <returns></returns>
    //private bool BoxOnBoxCollisionCheck(MyBoxCollider2D b2)
    //{
    //    MyBoxCollider2D b1 = this;
    //    // if you can draw a line between the two colliders they are not touching
    //    Vector3 b1Pos = b1.transform.position;
    //    Vector3 b2Pos = b2.transform.position;
    //    // get the width, height, and length
    //    float b1X = sizeX / 2;
    //    float b1Y = sizeY / 2;
    //    float b2X = b2.sizeX / 2;
    //    float b2Y = b2.sizeY / 2;

    //    // get the max distance of height width or length
    //    float maxDistX = b1X + b2X;
    //    float maxDistY = b1Y + b2Y;

    //    // see if the the distance between the two is less than the max meaning they are overlapping
    //    bool xCond = Mathf.Abs(b1Pos.x - b2Pos.x) < maxDistX;
    //    bool yCond = Mathf.Abs(b1Pos.y - b2Pos.y) < maxDistY;


    //    if (xCond && yCond)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(sizeX, sizeY, 0));
    }

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

    //public bool CollisionCheckHelp(MyCollider2D c)
    //{
    //    if(c.type == "Box")
    //    {
    //        return BoxOnBoxCollisionCheck(c.GetComponent<MyBoxCollider2D>());
    //    }
    //    if(c.type == "Circle")
    //    {
    //        return c.GetComponent<MyCircleCollider2D>().CircleOnBoxCollisionCheck(this);
    //    }

    //    return false;
    //}
}
