using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
   
    public string nextScene; // name of the next scene
    public Canvas prompt; // this is the change scene prompt
    private Vector2 scale;
    private bool onDoor;
    private LevelChange fader;


    // Start is called before the first frame update
    void Start()
    {
        
        onDoor = false;
        prompt.enabled = false;
        scale = prompt.transform.localScale;
        fader = GameObject.FindObjectOfType<LevelChange>();
        //prompt.transform.localScale = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (onDoor)
        {
            if (Input.GetKeyDown(KeyCode.Return) && prompt.enabled)
            {
                fader.nextLevel = nextScene;
                fader.change = true;
                //SceneManager.LoadScene(nextScene);

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            // make onDoor true so player can press enter to go to next scene
            onDoor = true;
            // Load the text
            DisplayText();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // once out of the collision make it false
        onDoor = false;
        // also make it so the text goes away
        RemoveText();
    }

    public void DisplayText()
    {
        prompt.enabled = true;
    }

    public void RemoveText()
    {
        prompt.enabled = false;
    }
}
