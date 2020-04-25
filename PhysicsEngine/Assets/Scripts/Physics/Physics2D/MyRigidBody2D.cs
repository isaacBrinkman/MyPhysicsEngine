//using System.Collections;
//using UnityEngine;

//public class MyRigidBody2D : MonoBehaviour
//{
//    public enum Types
//    {
//        Dynamic, Kinematic
//    }
//    public float xFriction;
//    public float yFriction; // maybe gravity

//    public float mass = 1;
//    public bool useGravity = true;
//    public Types type;
//    public Vector3 velocity;
//    private float width;
//    private float height;
//    private float momentOfInertia;

//    [Header("   Freeze Rotation")]
//    [Header("Constraints")]
//    public bool xRot;  // if this is true in update make sure the og xRot = current xRot
//    public bool yRot;
//    [Header("    Freeze Position")]
//    public bool xPos;  // if this is true in update make sure the og xPos = current xPos
//    public bool yPos;

//    [Range(0, 1)]
//    public float fallSpeed;

//    private MyCircleCollider2D col; // this needs to be changed to a more basic thing
//    public MyCircleCollider2D c;

//    Vector2 linearVelocity;
//    float angle;
//    float angularVelocity;
//    Vector2 force;
//    float torque;

//    public float colForce; // F=ma
//    public bool testTrigger;

//    // Start is called before the first frame update
//    void Start()
//    {
//        //velocity = Vector3.zero;
//        Rect rec = GetComponent<SpriteRenderer>().sprite.rect;
//        width = rec.width;
//        height = rec.height;
//        col = GetComponent<MyCircleCollider2D>();
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        Fall();
//        CalcInertia();
//        if(velocity != Vector3.zero) // once velocity is 0 no longer have to do this and should just fall
//        {
//            AddContinousForce(velocity);

//        }
//        //print(velocity);
//        Vector2 linearAcc = new Vector2(force.x / mass, force.y / mass);
//        linearVelocity += linearAcc * Time.deltaTime;
//        transform.position += (Vector3)linearVelocity * Time.deltaTime;
//        float angularAcc = torque / momentOfInertia;
//        angularVelocity += angularAcc * Time.deltaTime;
//        angle += angularVelocity * Time.deltaTime;

//        transform.RotateAround(transform.position,Vector3.forward,angle);
//        TwoCircleColHandler();
        
//        // decrease the velocity adding a friction affect
//        velocity = velocity / 1.2f;
//        if(velocity.x <= .001)
//        {
//            velocity.x = 0;
//        }
//        colForce = velocity.magnitude;

//    }

//    /// <summary>
//    /// Adds a continous force to this object
//    /// </summary>
//    /// <param name="aForce"></param>
//    public void AddContinousForce(Vector3 aForce)
//    {
//        ComputeForceTorque(aForce);
//    }

//    /// <summary>
//    /// applies gravity to an object if using gravity
//    /// TODO: CHECK IF THIS WORKS WHILE ADDING ANOTHER CONTINOUS FORCE (pretty sure it does)
//    /// </summary>
//    private void Fall()
//    {
//        if (useGravity)
//        {
//            AddContinousForce(new Vector3(0, mass * -.981f));
//            velocity.y += mass * -.981f;
//        }
//    }

  

//    /// <summary>
//    /// calculates the inertia of a 2D cube
//    /// </summary>
//    private void CalcInertia()
//    {
//        var m = mass;
//        var h = height;
//        var w = width;
//        momentOfInertia = m * (w * w + h * h) / 12;
        
//    }

//    /// <summary>
//    /// computes the force and torque of an object
//    /// </summary>
//    /// <param name="f"></param>
//    private void ComputeForceTorque(Vector2 f)
//    {
//        force = f;
//        Vector2 r = new Vector2(width / 2, height / 2);
       
//        torque = r.x * f.y - r.y * f.x;
//    }

//    public void TwoCircleColHandler()
//    {
//        if (col.CollisionCheck(/*c*/))
//        {
//            // bounce them using collision force
//            // need the add force method to add the collision force
//            // maybe can just do velocity.x * mass in order to get that collsion force
//            // then have them bounce
//            // if kinematic dont bounce
//            if(!(this.type == Types.Kinematic) && !(c.GetComponent<MyRigidBody2D>().type == Types.Kinematic))
//            {
//                // this is making the velocity go to 0 and then nothing
//                //this.velocity *= -1+Mathf.Abs(c.GetComponent<MyRigidBody2D>().colForce);
//                //useGravity = false;
//                transform.position += Vector3.up;
//                //AddContinousForce(-velocity * Mathf.Abs(c.GetComponent<MyRigidBody2D>().colForce));
//                //c.GetComponent<MyRigidBody2D>().velocity *= -1*Mathf.Abs(colForce);
//                this.velocity = c.GetComponent<MyRigidBody2D>().velocity;
//                c.GetComponent<MyRigidBody2D>().velocity = velocity;

//            }
//            // TODO: IF ONE IS KINEMATIC BOUNCE OFF WITHOUT MOVING THE OTHER (LIKE A WALL)
//        }
//    }


//    /// <summary>
//    ///  Adds a force amount of force to an object body after float seconds
//    /// </summary>
//    /// <param name="force"></param> force added in the x direction rn I think
//    /// <param name="body"></param> body to be pushed
//    /// <param name="seconds"></param> seconds until body is pushed
//    /// <returns></returns>
//    IEnumerator AddForce(Vector2 force, MyRigidBody2D body, float seconds)
//    {
//        yield return new WaitForSeconds(seconds);
//        StartCoroutine(FakeAddForceMotion(body, force));
//    }

//    // this only goes in the x direction
//    private IEnumerator FakeAddForceMotion(MyRigidBody2D _rigidbody2D, Vector2 forceAmount)
//    {
//        float i = 0.01f;
//        while (forceAmount.y > i && forceAmount.x > i)
//        {
//            _rigidbody2D.velocity = new Vector2(forceAmount.x / i, forceAmount.y / i);
//            i = i + Time.deltaTime;
//            yield return new WaitForEndOfFrame();
//        }
//        _rigidbody2D.velocity = Vector2.zero;
//        yield return null;
//    }

   

//    void OnDrawGizmos()
//    {
//        // draw a line in the direction of veloctiy
//        Gizmos.DrawLine(transform.position, transform.position + velocity);  // this isnt working ofc

//    }
//}
