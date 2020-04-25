using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    public bool unityPhysics;
    public StacksFloor sf;
   
   /// <summary>
   ///  this is in charge of: 
   ///  1) removing game objects when they are out side of the field of view
   ///  2) incrementing score if objects are stacked
   ///  3) ending game if out of screen?
   ///  
   /// </summary>
   /// 

    // Start is called before the first frame update
    void Start()
    {
        StacksFloor[] sfs = FindObjectsOfType<StacksFloor>();
        if (sfs[0].unityPhysics)
        {
            sf = sfs[0];
        }
        else
        {
            sf = sfs[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        // TODO remove the unityPhysics once the colliders work
        if (unityPhysics)
        {
            sf.Lose();

        }
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (unityPhysics)
        {
            if(transform.parent == sf.transform)
            {
                if (collision.gameObject.CompareTag("Shape"))
                {
                    sf.IncrementScore();
                    collision.transform.SetParent(sf.transform);
                    collision.gameObject.tag = ("Untagged");
                }
                
            }
        }
    }
}
