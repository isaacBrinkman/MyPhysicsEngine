using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyCollider2D : MonoBehaviour
{
    
    public string type;
    //protected MyCollider2D[] allColliders;

    public bool colliding;          // value for whether two objects are colliding
    // maybe make this a list to handle multiple collsions at once
    public MyCollider2D collisionObj;     // the object that it is collding with
    public MyRGB rb;
    public MyCollider2D[] allColliders;
    public MyCollider2D[] collisionObjs;
    private float tempGrav;
    public bool stopped;        // when this object stops moving this will be selected

    void Start()
    {
        tempGrav = rb.gravityScale;
    }


    // Update is called once per frame
    void Update()
    {
    }



    protected bool BoxOnCircleColCheck(MyBoxCollider2D bc, MyCircleCollider2D cc)
    {
        // WONT WORK ON TOP AND BOTTOM
        // then col type is circle
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
        return (xBool && yBool);

    }
    
    /// <summary>
    /// Calculate the angle and velocity
    /// CalledBy: CollisionHandler, ManyCollisions
    /// </summary>
    /// <param name="cc">Collider that it is colliding with</param>
    /// <returns>The velocity vector</returns>
    protected Vector2 Collide(MyCollider2D cc)
    {
        // currently all boxes are non moving objects so they are not affected by collisions
        if (this.GetComponent<MyBoxCollider2D>() != null)
        {
            return Vector2.zero;
        }

        if (cc.GetComponent<MyBoxCollider2D>() != null)
        {
            if (cc.GetComponent<MyBoxCollider2D>().pos == MyBoxCollider2D.Pos.bottom)
            {
                if(transform.position.y > cc.transform.position.y)
                {
                    //rb.gravityScale = 0;
                    return (Vector3.Reflect(rb.velocity, Vector3.up));
                }
                else
                {
                    //rb.gravityScale = tempGrav;
                    return Vector3.Reflect(rb.velocity, Vector3.down);

                }
            }
            else
            {
                rb.gravityScale = tempGrav;
                if (transform.position.x < cc.transform.position.x)
                {
                    // then on the left of the collider
                    return Vector3.Reflect(rb.velocity, Vector3.left);
                }
                else
                {
                    return Vector3.Reflect(rb.velocity, Vector3.right);

                }
 
            }
        }
        else
        {
            //if (cc.stopped && cc.transform.position.x.Equals(this.transform.position.x) &&
            //    cc.transform.position.y < this.transform.position.y)
            //{
            //    this.stopped = true;
            //    cc.stopped = false;
            //    return new Vector3(0, 0);
            //}
            print(name);
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
            return (tempVel);
            //StartCoroutine(rb.FutureUpdate());
        }
    }

    /// <summary>
    /// Handles many collisions
    /// Called by: CollisionHandler
    /// </summary>
    /// <param name="cols">All collider objs this is colliding with</param>
    protected void ManyCollisions(MyCollider2D[] cols)
    {
        // I think find the force added by each one and then add it to the resulting velocity
        Vector3 totalVelocity = Vector3.zero;
        // for all colliding objs
        foreach (MyCollider2D c in cols)
        {
            if (c == null)
            {
                continue;
            }
            // callculate the change in velocity
            //Vector3 tempVel = CirOnCirCollision(c.GetComponent<MyCircleCollider2D>());
            Vector3 tempVel = Collide(c);

            //totalVelocity += c.GetComponent<MyRGB>().velocity;
            // add all totals together
            totalVelocity += tempVel;
        }
        // need to set the new velocity for this to total velocity
        rb.futureStatues.futVelocity = totalVelocity;
    }

    public abstract void CollisionHandler();
    /*

// Brute force algorithm
// get a list of all the points and compute how far they are from every other point
// keep a closest variable
// O(n^2)
    private Vector3[] Bruteforce(Vector3[] points){

        //initialize the closest points as the first two points
        float closest_val = Vector3.Distance(points[0], points[1]);
        Vector3[] closest_points = new Vector3[] { points[0], points[1] };
        //closest_points[0] = (points[0]);
        //closest_points[1] = (points[1]);
        // go through each point and compare it with every other point and
        // see if the magnitude is smaller than the current smallest
        for(int i = 0; i < points.Length; i++)
        {
            for (int j = 0; j < points.Length; j++)
            {
                float dis = Vector3.Distance(points[i], points[j]);
                if (dis < closest_val)
                {
                    closest_val = dis;
                    closest_points[0] = points[i];
                    closest_points[1] = points[j];
                }
                   
            }


            // at the end of the loop return the closest points
            return closest_points;
        }



        // divide-and-conquer algorithm
        // works by sorting the list and then splitting the list in half and checking the closest pairs
        // within each section
        // O(nlogn)
        public Vector3[] Dac(Vector3[] points1)
        {
            //sort the list by x-axis
            Vector3[] lst = new Vector3[?];
            //sorting is O(nlogn)
            lst = sorted(points1, key = lambda tup: tup.get_coordinates());
        }


//      distance between closest points over a strip
//       O(n)
        def strip(s: List[Vector], size: int, dist: float) -> Tuple[float, Tuple]:
           min = dist
           coordinates = None
           s = sorted(s)

           # this loop runs at most 7 times so O(n)
           for i in range(size) :
               for j in range(i+1, size) :
                   if (s[j].y - s[i].y) < min:
                       if s[j].magnitude(s[i]) < min:
                           min = s[j].magnitude(s[i])
                           coordinates = (s[j].get_coordinates(), s[i].get_coordinates())
           return min, coordinates

# main function
# splits the list in half and checks the closest pair in each
    def dac_helper(lst: List[Vector], n, coor) -> Tuple[Tuple[float, float], Tuple[float, float], float]:

       # base case:
#if n is less than or equal to 3 compute it brute force
       if n <= 3:
           return bruteforce(lst)

# get a midpoint of the list
       mid = n//2
       mid_point = lst[mid]

# two recursive calls
       d1 = dac_helper(lst[:len(lst) // 2], mid, coor)      # splitting a list is O(1)
       d2 = dac_helper(lst[len(lst) // 2:], n - mid, coor)

# O(1)
# dist = min(d1, d2)
       if d1[1] < d2[1]:
           dist = d1
       else:
           dist = d2

# check values over each strip
# O(n)
       s = []
       for i in range(n):
           if abs(lst[i].x - mid_point.x) < dist[1]:
               s.append(lst[i])

       st = strip(s, len(s), dist[1])
# see which is smaller the new strip min or the old smallest distance
       m = min(dist[1], st[0])
       if m == dist[1]:
           coor = (dist[0])
       else:
           coor = st[1]

       coor = (coor[0], coor[1])
       return coor, m

    return dac_helper(lst, len(lst), (Vector(0, 0), Vector(0, 0)))

  */ 
}
