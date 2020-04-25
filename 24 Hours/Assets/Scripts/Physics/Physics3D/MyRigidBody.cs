using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRigidBody : MonoBehaviour
{
    public enum Types
    {
        Dynamic, Kinematic
    }

    public float mass;
    public bool useGravity = true;
    public Types type;
    public Vector3 velocity;
    private float width;
    private float height;
    private float depth;
    private Vector3 momentOfInertia;

    [Header("   Freeze Rotation")]
    [Header("Constraints")]
    public bool xRot;
    public bool yRot;
    public bool zRot;
    [Header("    Freeze Position")]
    public bool xPos;
    public bool yPos;
    public bool zPos;

    [Range(0, 1)]
    public float fallSpeed;


    Vector3 linearVelocity;
    float angle;
    float angularVelocity;
    Vector3 force;
    float torque;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;   
    }

    // Update is called once per frame
    void Update()
    {
        //if (false) // dont want this to run
        //{
        //    // this is the code to calculate velocity on a particle
        //    // it doesn't factor in rotation
        //    Vector3 force = ComputeForce();
        //    Vector3 acceleration = new Vector3(force.x / mass, force.y / mass, force.z / mass);
        //    velocity += acceleration;
        //    transform.position += velocity;
        //}

        ComputeForceTorque();
        Vector3 linearAcc = new Vector3(force.x / mass, force.y / mass, force.z / mass);
        linearVelocity += linearAcc * Time.deltaTime;
        transform.position += linearVelocity * Time.deltaTime;
        float angularAcc = torque / momentOfInertia.x;
        angularVelocity += angularAcc * Time.deltaTime;
        angle += angularVelocity * Time.deltaTime;
        //transform.RotateAround(Vector3.zero,Vector3.up,angle);       
        
    }
   
    /// <summary>
    ///  Adds a force amount of force to an object body after float seconds
    /// </summary>
    /// <param name="force"></param> force added in the x direction rn I think
    /// <param name="body"></param> body to be pushed
    /// <param name="seconds"></param> seconds until body is pushed
    /// <returns></returns>
    IEnumerator AddForce(float force, Rigidbody2D body, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(FakeAddForceMotion(body, force));
    }

    private IEnumerator FakeAddForceMotion(Rigidbody2D _rigidbody2D, float forceAmount)
    {
        float i = 0.01f;
        while (forceAmount > i)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, forceAmount / i);
            i = i + Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _rigidbody2D.velocity = Vector2.zero;
        yield return null;
    }

    /// <summary>
    /// Makes the object fall
    /// </summary>
    //private void Fall()
    //{
    //    // move the transfrom.pos with respect to time
    //    transform.position += Vector3.down*fallSpeed;
    //    if(fallSpeed<= 1)
    //    {
    //        // compute out that number
    //        fallSpeed += .24f * Time.deltaTime;
    //    }
    //}

    /// <summary>
    /// applies gravity to the object
    /// </summary>
    /// <returns></returns> returns mass times gravity constant (MIGHT HAVE TO REMOVE THE TIME.DELTATIME)
    ///                     else returns nothing
    private Vector3 ComputeForce()
    {
        if (useGravity)
        {
            return new Vector3(0, mass * -9.81f * Time.deltaTime, 0);
        }
        else return Vector3.zero;
    }

    /// <summary>
    /// calculates the inertia of a 3D cube
    /// </summary>
    private void CalcInertia()
    {
        var m = mass;
        var h = height;
        var w = width;
        var d = depth;
        float I_h = m * (w * w + h * h) / 12;
        float I_w = m * (d * d + h * h) / 12;
        float I_d = m * (w * w + h * h) / 12;
        momentOfInertia = new Vector3(I_h, I_w, I_d);
    }

    private void ComputeForceTorque()
    {
        Vector3 f = new Vector3(0, 1, 0); // upward force
        force = f;
        Vector3 r = new Vector3(width / 2, height / 2, depth / 2);
        torque = r.x * f.y - r.y * f.x;
    }
}
