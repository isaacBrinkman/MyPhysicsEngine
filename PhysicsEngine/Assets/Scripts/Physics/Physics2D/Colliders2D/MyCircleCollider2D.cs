using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyCircleCollider2D : MyCollider2D
{
    //TODO: LOOK AT THE GJK ALG WHICH ALLEGEDLY MAKES IT EASIER TO TEST OBJECTS LIKE
    //      CONES, CYLINDERS, ETC.
    // THIS SHOULD GET ALL COLLIDERS IN THE SCENE AND CHECK THEM BUT IT ISNT THERE YET
    // https://www.toptal.com/game/video-game-physics-part-iii-constrained-rigid-body-simulation
    /// <summary>
    /// this will allow to only check on objects that are close to one another
    /// </summary>
    public class AABB
    {
        public Vector2 min;
        public Vector2 max;
    }

    [Range(0,25)]
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        type = "Circle";
        if(radius == 0)
        {
            radius = GetComponent<SpriteRenderer>().sprite.rect.x;
        }
        //allColliders = FindObjectsOfType<MyCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, radius);
    }
    
}
