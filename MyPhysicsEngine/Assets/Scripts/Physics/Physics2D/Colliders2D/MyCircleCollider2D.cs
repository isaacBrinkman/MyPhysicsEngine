using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

/// <summary>
/// This class is the collider object for a circle
/// IT HANDLES ALL COLLISIONS
/// </summary>
public class MyCircleCollider2D : MyCollider2D
{
    //TODO: LOOK AT THE GJK ALG WHICH ALLEGEDLY MAKES IT EASIER TO TEST OBJECTS LIKE
    //      CONES, CYLINDERS, ETC.
    // THIS SHOULD GET ALL COLLIDERS IN THE SCENE AND CHECK THEM BUT IT ISNT THERE YET
    // https://www.toptal.com/game/video-game-physics-part-iii-constrained-rigid-body-simulation
    /// <summary>
    /// this will allow to only check on objects that are close to one another
    /// </summary>
    [Range(0,25)]
    public float radius;                    // radius of the circle collider

    // Start is called before the first frame update
    void Start()
    {
        type = "Circle";
        if(radius == 0)
        {
            radius = GetComponent<SpriteRenderer>().sprite.rect.x;
        }
        allColliders = FindObjectsOfType<MyCollider2D>();
        rb = GetComponent<MyRGB>();
        collisionObjs = new MyCollider2D[allColliders.Length-1];

    }

    void Update()
    {
        if(allColliders.Length < FindObjectsOfType<MyCollider2D>().Length)
        {
            allColliders = FindObjectsOfType<MyCollider2D>();
        }
    }

    /// <summary>
    /// Checks for collisions
    /// </summary>
    public override void CollisionHandler()
    {
        if(collisionObjs.Length == 0)
        {
            return;
        }
        Array.Clear(collisionObjs, 0, collisionObjs.Length);
        foreach(MyCollider2D col in allColliders)
        {
            if(col == this)
            {
                continue;
            }
            else if (CollisionCheck(col))
            {
                for(int i =0; i <= collisionObjs.Length; i++)
                { 
                    if(collisionObjs[i] == null)
                    {
                        collisionObjs[i] = col;
                        break;
                    }
                }
            }
        }
        if(collisionObjs[0] != null)
        {
    
            foreach(MyCollider2D c in collisionObjs)
            {
                if(c != null)
                {
                    print(c.name);
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

    
    /// <summary>
    /// Checks if this is colliding with a collider obj
    /// Called by: CollisionHandler
    /// </summary>
    /// <param name="cc">cc the obj to check </param>
    /// <returns>true if colliding</returns>
    private bool CollisionCheck(MyCollider2D cc)
    {      
        // check if colliding
        if(cc.type == "Circle")
        {
            MyCircleCollider2D circleCollider = cc.GetComponent<MyCircleCollider2D>();

            // check if colliding with distance method
            if (Vector2.Distance(transform.position, circleCollider.transform.position) <= (radius + circleCollider.radius))
            {
                //rb.Pause();
                //cc.GetComponent<MyRGB>().Pause();

                float d1 = Vector2.Distance(transform.position, circleCollider.transform.position);
                float d2 = Vector2.Distance(circleCollider.transform.position, transform.position);
               
                //print(name + "is colliding with" + cc.name + " " + rb.count);

                // then the two overlap and have to change all the stuff
                return true;
            }
        }
        if(cc.type == "Box")
        {
            MyBoxCollider2D boxCollider = cc.GetComponent<MyBoxCollider2D>();
            if (BoxOnCircleColCheck(boxCollider, this))
            {
                //CirOnCirCollision(boxCollider);
                // then change all the stuff
                //print(name + "is colliding with" + cc.name + " " + rb.count + "B on C");
                return true;
            }
        }
        return false;
    }




    /// <summary>
    /// Not using angles doesnt work
    /// </summary>
    /// <param name="bc"></param>
    /// <returns></returns>
    private Vector2 CirOnCirCollision(MyCollider2D cc)
    {
       
        MyRGB ccRb = cc.GetComponent<MyRGB>();
        Vector2 v1 = new Vector2(rb.velocity.x, rb.velocity.y);
        Vector2 v2 = new Vector2(ccRb.velocity.x, ccRb.velocity.y);
        Vector2 p1 = new Vector2(this.transform.position.x, transform.position.y);
        Vector2 p2 = new Vector2(cc.transform.position.x, cc.transform.position.y);
        float m1 = rb.mass;
        float m2 = ccRb.mass;
        float left = (2 * m1) / (m1 + m2);
        float midTop = Vector2.Dot(v1 - v2, p1 - p2);
        float midB = Vector2.SqrMagnitude(p2 - p1);
        Vector2 right = (p1 - p2);
        Vector3 tempVel = v1 - (left * (midTop / midB) * right);

        rb.futureStatues.futVelocity = tempVel;
        //rb.futureStatues.updated = true;
        rb.futureStatues.futurePosition = transform.position /* + tempVel * Time.deltaTime*/;
        //rb.Resume();
        return tempVel;
        //StartCoroutine(rb.FutureUpdate());

    }



    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, radius);
    }
    
}
