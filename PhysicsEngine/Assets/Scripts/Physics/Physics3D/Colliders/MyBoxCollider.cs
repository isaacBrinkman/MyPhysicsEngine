using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxCollider : MyCollider
{
    public bool isTrigger;
    public float sizeX = 0;
    public float sizeY = 0;
    public float sizeZ = 0;
    private Vector3 size;
    
    


    // Start is called before the first frame update
    void Start()
    {
        // TODO: rotate the vector when the object rotates
        type = "Box";
        size = new Vector3(sizeX, sizeY, sizeZ);
        // if the size of the three together is 0 than the user never entered anything so just auto fill
        if(sizeX + sizeY + sizeZ == 0)
        {
            size = GetComponent<Renderer>().bounds.size;
            sizeX = size.x;
            sizeY = size.y;
            sizeZ = size.z;
        }
        // this is just for stacks in order to make the floor valid
        //b1 = c;
        // find all objects that are of my collider
        allC = FindObjectsOfType<MyCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (MyCollider cd in allC)
        {
            MyOnCollision(cd);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }


    // returns true if there is a collision
    public override bool MyOnCollision(MyCollider bx)
    {
        if (bx.type.Equals("Box"))
        {
            return CubeCheck((MyBoxCollider)bx);
        }
        if (bx.type.Equals("Circle"))
        {
            return SphereCheck((MyCircleCollider)bx);
        }
        return true;
    }

    /// <summary>
    /// Takes a box collider and checks if the two are touching
    /// </summary>
    /// <param name="bx"></param> 
    /// <returns></returns> returns true if the colliders are touching
    public override bool CubeCheck(MyBoxCollider bx)
    {
        // if you can draw a line between the two colliders they are not touching
        Vector3 b1Pos = this.transform.position;
        Vector3 b2Pos = bx.transform.position;
        // get the width, height, and length
        float b1X = sizeX/2;
        float b1Y = sizeY/2;
        float b1Z = sizeZ/2;
        float b2X = bx.sizeX/2;
        float b2Y = bx.sizeY/2;
        float b2Z = bx.sizeZ/2;

        // get the max distance of height width or length
        float maxDistX = b1X + b2X;
        float maxDistY = b1Y + b2Y;
        float maxDistZ = b1Z + b2Z;

        // see if the the distance between the two is less than the max meaning they are overlapping
        bool xCond = Mathf.Abs(b1Pos.x - b2Pos.x) < maxDistX;
        bool yCond = Mathf.Abs(b1Pos.y - b2Pos.y) < maxDistY;
        bool zCond = Mathf.Abs(b1Pos.z - b2Pos.z) < maxDistZ;

       
        if (xCond && yCond && zCond)
        {
            print(gameObject.name);
            return true;
        }

        return false;
    }

    public override bool SphereCheck(MyCircleCollider bx)
    {
        return false;
    }
    


}
