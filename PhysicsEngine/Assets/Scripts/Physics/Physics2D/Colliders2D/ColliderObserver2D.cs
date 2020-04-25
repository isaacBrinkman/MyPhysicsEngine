using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this may not be neccisary
// there may not be a concurrency issue
// get a list of all a single colldiers colliders[] 
// a single obj calculates for itself 
// look up concurrency for current unity
// implemented the physics laws into the object 


/// <summary>
/// This class is in charge of overseeing collisions
/// should have a list of all the colliders and check if they are colliding with a nested for loop
/// THIS MAKES IT SO INDIVIDUAL COLLIDERS ARENT CHECKING FOR COLLISIONS
/// </summary>
public class ColliderObserver2D : MonoBehaviour
{
    public MyCollider2D[] colliders;        // list of all the colliders


    // Start is called before the first frame update
    void Start()
    {
        colliders = FindObjectsOfType<MyCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(colliders.Length != FindObjectsOfType<MyCollider2D>().Length)
        {
            colliders = FindObjectsOfType<MyCollider2D>();
        }

        for(int i = 0; i < colliders.Length; i++){
            for(int j = i+1; j < colliders.Length; j++)
            {
                // if i and j already collided need a way not to check j and i 
                if(i == j)
                {
                    continue;   // this should never happen but just in case put it in
                }
                // check if the two objects are colliding
                CollisionCheck(colliders[i], colliders[j]);
            }
        }
        // put all the collisions here and NOT in the rgb test
        // fixes the concurency issues

    }

    /// <summary>
    ///  changes the values in c1 and c2 colliding and colObj if the two objects are colliding
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    public void CollisionCheck(MyCollider2D c1, MyCollider2D c2)
    {
        // if they are already recorded as touching don't do everything
        if (c1.collisionObj != c2)
        {


            if (c1.type == "Circle" && c2.type == "Circle")
            {
                // then do a circle on circle collsion check
                CirlceOnCircleCheck(c1.GetComponent<MyCircleCollider2D>(), c2.GetComponent<MyCircleCollider2D>());
            }

            if (c1.type == "Box" && c2.type == "Box")
            {
                BoxOnBoxCheck(c1.GetComponent<MyBoxCollider2D>(), c2.GetComponent<MyBoxCollider2D>());
            }

            if (c1.type == "Circle" && c2.type == "Box")
            {
                CircleOnBoxCheck(c1.GetComponent<MyCircleCollider2D>(), c2.GetComponent<MyBoxCollider2D>());
            }
            if (c2.type == "Circle" && c1.type == "Box")
            {
                CircleOnBoxCheck(c2.GetComponent<MyCircleCollider2D>(), c1.GetComponent<MyBoxCollider2D>());
            }
        }
    }

    private void CirlceOnCircleCheck(MyCircleCollider2D c1, MyCircleCollider2D c2)
    {
        // get position of the two objects
        Vector3 c1Pos = c1.transform.position;
        Vector3 c2Pos = c2.transform.position;

        float x = c1Pos.x - c2Pos.x;
        float y = c1Pos.y - c2Pos.y;
        // take the distance^2 and see if the radius^2 is greater than that
        // if so touching
        float disSquared = x * x + y * y;
        float rad = c1.radius + c2.radius;
        float rdSquared = rad * rad;

        if(disSquared <= rdSquared)
        {
            
            // then collidSing
            c1.colliding = true;
            c2.colliding = true;
            c1.collisionObj = c2;
            c2.collisionObj = c1;

            RGBUtil.CirOnCirCol(c1, c2);

        }

    }


    /// <summary>
    /// checks box on box collision with this and a second box
    /// </summary>
    /// <returns> 
    /// returns true if the boxes are colliding
    /// </returns>
    private void BoxOnBoxCheck(MyBoxCollider2D b1, MyBoxCollider2D b2)
    {
        // if you can draw a line between the two colliders they are not touching
        Vector3 b1Pos = b1.transform.position;
        Vector3 b2Pos = b2.transform.position;
        // get the width, height, and length
        float b1X = b1.sizeX / 2;
        float b1Y = b1.sizeY / 2;
        float b2X = b2.sizeX / 2;
        float b2Y = b2.sizeY / 2;

        // get the max distance of height width or length
        float maxDistX = b1X + b2X;
        float maxDistY = b1Y + b2Y;

        // see if the the distance between the two is less than the max meaning they are overlapping
        bool xCond = Mathf.Abs(b1Pos.x - b2Pos.x) < maxDistX;
        bool yCond = Mathf.Abs(b1Pos.y - b2Pos.y) < maxDistY;

        if(xCond && yCond)
        {
            // then colliding
            b1.colliding = true;
            b2.colliding = true;
            b1.collisionObj = b2;
            b2.collisionObj = b1;
        }
    }


    /// <summary>
    /// checks if a this (a circle) is touching a box
    /// </summary>
    /// <param name="bc"></param> box obj
    /// <returns></returns> true if colliding
    private void CircleOnBoxCheck(MyCircleCollider2D cc, MyBoxCollider2D bc)
    {
        // THIS ONLY WORKS ON NON ROTATED BOXES
        float width = bc.sizeX;
        float height = bc.sizeY;

        Vector3 cPos = cc.transform.position;
        Vector3 bPos = bc.transform.position;

        float x = Mathf.Abs(cPos.x - bPos.x);
        float y = Mathf.Abs(cPos.y - bPos.y);

        bool xBool = (x <= cc.radius + width / 2);
        bool yBool = (y <= cc.radius + height / 2);
        // then the two are overlapping on the x and y radius
        if(xBool && yBool)
        {
            // then colliding
            bc.colliding = true;
            cc.colliding = true;
            bc.collisionObj = cc;
            cc.collisionObj = bc;

            RGBUtil.BoxOnCirCol(bc, cc);
            // also need to get it out of the box
            //cc.transform.position = new Vector3(cc.transform.position.x, cc.transform.position.y + .002f, 0);
            //StartCoroutine(ClearCoroutine(bc));
            //StartCoroutine(ClearCoroutine(cc));
        }

       

    }


  


}
