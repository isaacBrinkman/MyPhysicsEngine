using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyCollider : MonoBehaviour
{
    //public MyCollider c;
    protected MyCollider[] allC;
    public string type;
    // Start is called before the first frame update
    void Start()
    {
        allC = FindObjectsOfType<MyCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (MyCollider cd in allC)
        {
            print("testing");
            MyOnCollision(cd);
        }
    }

    public abstract bool MyOnCollision(MyCollider c);

    // checks to see if the collider is touching a cube
    public abstract bool CubeCheck(MyBoxCollider bx);

    // checks to see if the collider is touching a box
    public abstract bool SphereCheck(MyCircleCollider bx);


}
