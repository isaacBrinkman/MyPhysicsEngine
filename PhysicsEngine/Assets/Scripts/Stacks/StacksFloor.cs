using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StacksFloor : MonoBehaviour
{
    public Text scoreText;
    public bool testTrigger;
    public bool unityPhysics;
    public PauseManager pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
      
        if (testTrigger)
        {
            IncrementScore();
            testTrigger = false;
        }
        //if (GetComponent<MyCollider>().MyOnCollision(shape))
        //{
        //    IncrementScore();
        //}

    }

    void OnCollisionEnter(Collision collision)
    {
        if (unityPhysics)
        {
            if (collision.gameObject.CompareTag("Shape"))
            {
                IncrementScore();
                collision.transform.SetParent(this.transform);
                collision.gameObject.tag = ("Untagged");
            }
        }
    }

    public void IncrementScore()
    {
        int score = int.Parse(scoreText.text);
        score++;
        scoreText.text = score.ToString();

    }

    public void Lose()
    {

        pauseMenu.LoseScreen();
    }
}
