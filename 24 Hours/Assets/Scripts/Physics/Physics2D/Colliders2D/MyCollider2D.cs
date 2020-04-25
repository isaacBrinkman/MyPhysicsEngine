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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
