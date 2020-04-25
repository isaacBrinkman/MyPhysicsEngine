using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRGBTest : MonoBehaviour
{
    public Vector3 velocity;
    public float mass;
    private MyCollider2D cc;
    private float timerCol;
    public bool isKinematic;
    public float gravityScale;
    public float gravTemp;
    [Range(0.0f, 1.0f)]
    public float bounciness;        // 1 implies a full bounce
    public bool applyFriction;
    public float frictionForce;     // this should always be greater than 1
    // this will be a list of this form:
    // [future/present, xpos, ypos, xvel, yvel]
    public List<float> presentInfo;
    // collider y will use the present to calc y.future
    // then this will use y.present to calc this.future
    public List<float> futureInfo;


    public enum Wall
    {
        Vertical, Horizontal, NA
    }

    public Wall wallside;
    // Start is called before the first frame update
    void Start()
    {
        if (!isKinematic)
        {
            wallside = Wall.NA;
        }
        cc = GetComponent<MyCollider2D>();
        timerCol = 0;
        gravTemp = gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += (velocity)*Time.deltaTime;
        if (!isKinematic)
        {
            Gravity();
        }
        // CollisionCheck();   // a and b could be updating at the same time causing calulations to be off
        // ApplyFriction();
    }

    private void Gravity()
    {
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
        if(velocity.y == 0)
        {
            gravTemp = 0;
        }
        else
        {
            gravTemp = gravityScale;
        }
    }


    /// <summary>
    /// applies friction using fricitonForce
    /// if the vecolicty is less than .01 stop
    /// </summary>
    private void ApplyFriction()
    {
        if (applyFriction)
        {
            velocity /= frictionForce;
        }
        bool cond1 = Mathf.Abs(velocity.x) < .01;
        bool cond2 = Mathf.Abs(velocity.y) < .01;
        if (cond1 && cond2)
        {
            applyFriction = false;
            velocity = Vector3.zero;
        }
    }

    // still colliding a lot 
    private void CollisionCheck()
    {   
        if(cc.colliding)
        {
            // if either object is kinematic then need to do a collision test
            if (cc.collisionObj.GetComponent<MyRGBTest>().isKinematic || this.isKinematic)
            {
                print("KINEMATIC");
                //KinematicCollision(cc.collisionObj.GetComponent<MyRGBTest>());
            }
            else if (!cc.collisionObj.GetComponent<MyRGBTest>().isKinematic && !this.isKinematic)
            {

                ColCalc(this.cc, cc.collisionObj);
                
            }

            // after the collision is handled make it so the objects are not colliding
            //cc.colliding = false;
            cc.collisionObj.collisionObj = null;
            this.cc.collisionObj.colliding = false;
            this.cc.colliding = false;
            this.cc.collisionObj = null;
        }
        
    }

    private void ColCalc(MyCollider2D c1, MyCollider2D c2)
    {
        // this should only be called once but for some reason is a lot
        RGBUtil.ComputeAngle(c1, c2);
    }

    


  


}
