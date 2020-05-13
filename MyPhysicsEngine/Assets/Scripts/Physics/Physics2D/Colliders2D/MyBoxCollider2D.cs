using System;
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
    public enum Pos
    {
        side,
        bottom
    }
    public Pos pos;
    public float sizeX = 0;
    public float sizeY = 0;
    private Vector2 size;
    //private MyCollider2D[] allColliders;    // all the colliders
    //private MyRGB rb;                       // this obj rigidbody


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
        allColliders = FindObjectsOfType<MyCollider2D>();
        rb = GetComponent<MyRGB>();
        collisionObjs = new MyCollider2D[allColliders.Length - 1];

    }

    /// <summary>
    /// Checks for collisions
    /// </summary>
    public override void CollisionHandler()
    {
        if (collisionObjs.Length == 0)
        {
            return;
        }
        Array.Clear(collisionObjs, 0, collisionObjs.Length);
        foreach (MyCollider2D col in allColliders)
        {
            if (col == this)
            {
                continue;
            }
            else if (CollisionCheck(col))
            {
                for (int i = 0; i <= collisionObjs.Length; i++)
                {
                    if (collisionObjs[i] == null)
                    {
                        collisionObjs[i] = col;
                        break;
                    }
                }
            }
        }
        if (collisionObjs[0] != null)
        {
            // if it is only 1 long then do it the regular way
            if (collisionObjs.Length == 1 || collisionObjs[1] == null)
            {
                print("calc");
                // THIS IS NOT ACTUALLY CHANGING ANYTHING
                //Collide(collisionObjs[0]);
                /*
                if (collisionObjs[0].type == "Circle")
                {
                    Collide(collisionObjs[0]);
                    //CirOnCirCollision(collisionObjs[0].GetComponent<MyCircleCollider2D>());
                }
                if (collisionObjs[0].type == "Box")
                {
                    Collide(collisionObjs[0]);
                    //BoxOnCirCollision(collisionObjs[0].GetComponent<MyBoxCollider2D>());

                }
                */
            }
            // then need to do it the long way
            else
            {
                foreach (MyCollider2D c in collisionObjs)
                {
                    if (c != null)
                    {
                        print(c.name);
                    }
                    if (c == null)
                    {
                        print("null");
                    }
                }
                MyCollider2D[] cols = new MyCollider2D[collisionObjs.Length];
                for (int i = 0; i < collisionObjs.Length; i++)
                {
                    if (collisionObjs[i] == null)
                    {
                        break;
                    }
                    else
                    {
                        cols[i] = collisionObjs[i];
                    }
                }
                ManyCollisions(cols);
            }

        }

    }

    // Checks if this is colliding with cc
    private bool CollisionCheck(MyCollider2D cc)
    {

        // check if colliding
        if (cc.type == "Box")   // then box on box collision
        {
            MyBoxCollider2D boxCollider = cc.GetComponent<MyBoxCollider2D>();
            //TODO implement this stuff below
            //if (BoxOnBoxColCheck(boxColider))
            //{

            //}
        }

        if (cc.type == "Circle")
        {
            MyCircleCollider2D circleCollider = cc.GetComponent<MyCircleCollider2D>();
            if (BoxOnCircleColCheck(this, circleCollider))
            {
                // return true
                print(name + "is colliding with" + cc.name);
                return true;

            }
        }
        return false;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(sizeX, sizeY, 0));
    }


}
