﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTest : MonoBehaviour
{
    Rigidbody2D rb;
    public bool test = false;
    public Vector2 vel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            rb.velocity = vel;
            test = false;
        }
    }
}
