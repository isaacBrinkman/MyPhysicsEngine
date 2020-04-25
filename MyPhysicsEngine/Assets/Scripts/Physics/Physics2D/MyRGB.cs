using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRGB : MonoBehaviour
{
    public Vector3 velocity;    // velocity of this obj
    public float mass = 1;          // mass of this obj
    public bool isKinematic;    // whether the obj is kinematic
    public MyCollider2D cc;     // this obj's collider
    public float gravityScale;  // amount of gravity on this obj
    [Range(0.0f, 1.0f)]
    public float bounciness;    // the bounciness 1 is full bounce 0 us none
    public Vector2 tempVel;
    public bool paused;

    private float gravTemp;     // used to change gravity


    public enum Wall    //used to decide if a kinematic wall is vertical or horizontal
    {
        Vertical, Horizontal, NA
    }
    public Wall wallside;

    public class FutureStatus   // This is used as a tempory storage for velocity and position
    {
        public Vector3 futVelocity;
        public Vector3 futurePosition;
        public FutureStatus(Vector3 v, Vector3 p)
        {
            futVelocity = v;
            futurePosition = p;
        }
        public void Wipe()      // sets values equal to 0;
        {
            futurePosition = Vector3.zero;
            futVelocity = Vector3.zero;
        }
        public bool updated = false;
    }

    public FutureStatus futureStatues;

    void Start()
    {
        if (!isKinematic)       // if this obj is not kinematic then wallside is none
        {
            wallside = Wall.NA;
        }
        futureStatues = new FutureStatus(velocity, transform.position);
        gravTemp = gravityScale;
        cc = GetComponent<MyCollider2D>();
    }

    public int count = 0;
    void Update()
    {
        // move is changing
        // print the distances here in update
        // if they are still changing something is going on
        // need to see if something else is moving
        // print here
        futureStatues.futVelocity = velocity;
        //transform.position = futureStatues.futurePosition;
        // print here
        Gravity();
        cc.CollisionHandler();      // on a collision need to start a coroutine
        StartCoroutine(this.FutureUpdate());

        // print here
        //print(name + "is updating" + count);
        count++;
    }

    public void Resume()
    {
        //velocity = tempVel;
    }

    public void Pause()
    {
   //     tempVel = velocity;
   //     velocity = Vector2.zero;
    }

    private void Move()
    {
        
        transform.position += (velocity) * Time.deltaTime;  // change the transform based on the velocity
        
    }

    public IEnumerator FutureUpdate()
    {
        yield return new WaitForEndOfFrame();
        //print(name + "corourtine is running");
        velocity = futureStatues.futVelocity;
        Move();

        futureStatues.updated = false;
    }

    /// <summary>
    /// Applies gravity to this object
    /// NEEDS IMPROVEMENT
    /// </summary>
    private void Gravity()
    {
        if (!isKinematic)
        {
            // this should be changed
            if (gravTemp != 0)
            {
                // looking at unity y increases by .2 then jumps to 3.5 then increases by .2
                // if colliding from the bottom then dont need to but that can be figured out later;
                //velocity.y += (mass * -.981f)*Time.deltaTime*gravityScale);
                if (-velocity.y > 1f && -velocity.y < 3.5f)
                {
                    velocity.y = -3.6f;
                }
                // eventually need a way to make it speed up slower as time goes on
                velocity.y -= .2f * gravTemp;
            }
            if (velocity.y == 0)
            {
                gravTemp = 0;
            }
            else
            {
                gravTemp = gravityScale;
            }
        }
    }


}