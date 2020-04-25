using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityTestHandler : MonoBehaviour
{
    public GameObject myBall;
    public GameObject unityBall;
    public Text myDepth;
    public Text unityDepth;
    public Camera myCamera;
    public Camera unityCamera;
    private int myDepthCounter;
    private int unityDepthCounter;
    private int myStart;
    private int unityStart;

    // Start is called before the first frame update
    void Start()
    {
        myDepthCounter = 0;
        unityDepthCounter = 0;
        // get the start position
        myStart = (int)Mathf.Round(myBall.transform.position.y);
        unityStart = (int)Mathf.Round(unityBall.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        myDepthCounter = myStart - (int)Mathf.Round(myBall.transform.position.y);
        unityDepthCounter = unityStart - (int)Mathf.Round(unityBall.transform.position.y);
        UpdateText();
        UpdateColor(myCamera, myDepthCounter);
        UpdateColor(unityCamera, unityDepthCounter);

    }

    /// <summary>
    /// Updates the depth meters for the balls
    /// </summary>
    private void UpdateText()
    {
        myDepth.text = "Distance: " + myDepthCounter + " m \nSpeed: " + Mathf.Round(myBall.GetComponent<MyRGB>().velocity.y) + " m/s";
        unityDepth.text = "Distance: " + unityDepthCounter + " m \nSpeed: " + Mathf.Round(unityBall.GetComponent<Rigidbody2D>().velocity.y) + " m/s";

    }
    private void UpdateColor(Camera c, int d)
    {
        if (d < 255)
        {
            float conversion = 1 - ((d) / 255f);
            c.backgroundColor = new Color(conversion, 1,1);
        }
        else if(d > 255 && d < 510)
        {
            float conversion = 1 - ((d - 256) / 255f);
            c.backgroundColor = new Color(0, conversion, 1);
        }
        else if(d > 510 && d < 765)
        {
            float conversion = 1 - ((d - 510) / 255f);
            c.backgroundColor = new Color(0,0, conversion);
        }
        else
        {
            c.backgroundColor = new Color(0,0,0);
        }
    }
}
