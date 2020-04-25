using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unityBall : MonoBehaviour
{
    public Vector3 velocity;
    public Rigidbody2D rgb;
    private bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        rgb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!trigger)
        {
            rgb.velocity = velocity;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        trigger = true;
    }

}
