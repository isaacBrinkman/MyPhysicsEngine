using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRGB : MonoBehaviour
{
    public Vector3 velocity;    // velocity of this obj
    public float mass = 1;          // mass of this obj
    public bool isKinematic;    //  whether or not the obj is kinematic
    public MyCollider2D cc;     // this obj's collider
    public float gravityScale;  // amount of gravity on this obj usually 0 or 1
    [HideInInspector]
    public Vector2 tempVel; 

    /// <summary>
    /// Used as a temporary storage for velocity and position
    /// </summary>
    public class FutureStatus 
    {
        public Vector3 futVelocity;
        public Vector3 futurePosition;
        public FutureStatus(Vector3 v, Vector3 p)
        {
            futVelocity = v;
            futurePosition = p;
        }
        // sets values equal to 0
        public void Wipe()      
        {
            futurePosition = Vector3.zero;
            futVelocity = Vector3.zero;
        }
    }

    public FutureStatus futureStatues;

    void Start()
    {

        futureStatues = new FutureStatus(velocity, transform.position);

        cc = GetComponent<MyCollider2D>();
    }

    void Update()
    {
        // update futureVelcoity to current velocity at the start of the frame
        futureStatues.futVelocity = velocity;
        // check for collisions
        cc.CollisionHandler();      
        // Add gravity
        Gravity();
        // Start the Corutine
        StartCoroutine(this.FutureUpdate());
    }



    private void Move()
    {
        // change the transform based on the velocity
        transform.position += (velocity) * Time.deltaTime;  
        
    }

    /// <summary>
    /// This function is called once all collisions have been accounted for
    /// this allows for accurate collision updating
    /// </summary>
    /// <returns></returns>
    public IEnumerator FutureUpdate()
    {
        // wait until all objects have updated
        yield return new WaitForEndOfFrame();
        // velocity = the future velocity
        velocity = futureStatues.futVelocity;
        // move the object
        Move();
        

    }

    /// <summary>
    /// Applies gravity to this object
    /// </summary>
    private void Gravity()
    {
        // terminal velocity has been reached
        if(velocity.y == -136)
        {
            futureStatues.futVelocity += new Vector3(0, 0);
            return;

        }
        print("adding gravity");
        // this time.deltatime should always be accounted for since not all systems have same power
        futureStatues.futVelocity += new Vector3(0, gravityScale * (-9.8f)*Time.deltaTime);


    }

}