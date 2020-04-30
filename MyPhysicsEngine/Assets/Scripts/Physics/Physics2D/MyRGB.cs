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
    public float frictionScale; // amount of friction on this obj
    public Vector2 tempVel;
    public bool verPause;
    public float bounciness = 1;
    private float gravTemp;     // used to change gravity
    //public float frictionScale;

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
        cc.CollisionHandler();      // on a collision need to start a coroutine
        Gravity();
        //Friction();
        StartCoroutine(this.FutureUpdate());

        // print here
        //print(name + "is updating" + count);
        count++;
    }

   // public void Resume()
   // {
   //     //velocity = tempVel;
   // }

   // public void Pause()
   // {
   ////     tempVel = velocity;
   ////     velocity = Vector2.zero;
   // }

    private void Move()
    {
        //if (verPause)
        //{
        //    velocity.y = 0;
        //}

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
        // terminal velocity has been reached
        if(velocity.y == -136)
        {
            // dont add more
            futureStatues.futVelocity += new Vector3(0, 0);
            return;

        }
        // need way for the amount added slows down as it gets faster
        futureStatues.futVelocity += new Vector3(0, gravityScale * (-.098f));
        //futureStatues.futVelocity += new Vector3(0, gravityScale * (-.15f));


    }

    /// <summary>
    /// Applies friction to this object lowering value of velocity until 0
    /// </summary>
    private void Friction()
    {
        //float fricForce = frictionScale * 100;
        if(frictionScale != 0)
        {
            futureStatues.futVelocity.x /= (.01f * frictionScale);
        }
        //futureStatues.futVelocity /= new Vector3(1, 1) * (1 / frictionScale);
    }


}