using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntraScene : MonoBehaviour
{
    public bool triggered = false;      // pauses the scene
    public bool resume = false;         // resumes the scene
    private Animator ani;

    public bool active;                 // player disappears
    public bool invisible;              // All moving objects disappear
    public bool stopped;                // everything freezes

    public string[] activeTags;
    public string[] invisibleTags;
    public string[] stoppedTags;

    public float waitTime;

    [SerializeField] private GameObject pausePanel;


    private List <GameObject> activeObj;
    private List<GameObject> invisibleObj;
    private List<GameObject> stoppedObj;

    //public PlayerPlatformerController player;
    private float ogMaxSpeed, ogSprintSpeed, ogJumpSpeed;
    
    // Start is called before the first frame update
    void Start()
    { 
        ani = GetComponent<Animator>();
        activeObj = FindTags(activeTags);
        invisibleObj = FindTags(invisibleTags);
        stoppedObj = FindTags(stoppedTags);
        //ogMaxSpeed = player.regMaxSpeed;
        //ogSprintSpeed = player.sprintMaxSpeed;
        //ogJumpSpeed = player.jumpTakeOffSpeed;
        pausePanel.SetActive(false);
        //player = FindObjectOfType<PlayerPlatformerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)      // Set this to true inorder to trigger the intra-scene fader
        {
            print("here");
            ani.SetBool("Fade", true);      //This triggers an animation that calls the Triggered() function
                                            // which then will do the appropriate action

        }

        if (resume)                         // set this to true in order to fade again and resume the game
        {
            ani.SetBool("Resume", true);
        }
    }

    // Takes an array of tags and finds all the gameobjects with those tags
    private List <GameObject> FindTags(string[] tags)
    {
        List<GameObject> objs = new List <GameObject>();
        foreach(string tag in tags)
        {
            GameObject[] aObj = GameObject.FindGameObjectsWithTag(tag);
            foreach(GameObject go in aObj)
            {
                objs.Add(go);
            }
        }
        return objs;
    }

    // Freeze the player
    private void FreezePlayer()
    {
        //player.regMaxSpeed = 0;
        //player.sprintMaxSpeed = 0;
        //player.jumpTakeOffSpeed = 0;
    }

    // Un-freeze the player
    private void UnFreezePlayer()
    {
        //player.regMaxSpeed = ogMaxSpeed;
        //player.sprintMaxSpeed = ogSprintSpeed;
        //player.jumpTakeOffSpeed = ogJumpSpeed;
    }

    // makes all objects in a list not visible
    private void removeObj(List <GameObject> objs)
    {
        foreach (GameObject obj in objs)
        {
            if (obj.GetComponent<SpriteRenderer>() != null)
            {
                obj.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    // makes all objects of a list visible
    private void bringBack(List <GameObject> objs)
    {
        foreach(GameObject obj in objs)
        {
            if(obj.GetComponent<SpriteRenderer>() != null)
            {
                obj.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    // When this is activated (inside the animation) makes the affect happen
    public void Triggered()
    {
        triggered = false;
        print("false");

        ani.SetBool("Fade", false);
        
        FreezePlayer();

        // one of theses bools has to be true and will do the appropriate effect
        if (active)
        {
            removeObj(activeObj);
        }

        if (invisible)
        {
            removeObj(invisibleObj);
        }

        if (stopped)
        {

            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
          
        }

    }


    //Reverses the affects of Triggered()
    public void Resume()
    {
        resume = false;
        ani.SetBool("Resume", false);
        ani.SetBool("Fade", false);
        print("Resume false");

        UnFreezePlayer();
        if (active)
        {
            bringBack(activeObj);
            active = false;
        }

        if (invisible)
        {
            bringBack(invisibleObj);
            invisible = false;
        }
        if (stopped)
        {
            if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
            stopped = false;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

    public void PauseAnimation()
    {
        ani.enabled = false;
        StartCoroutine(PauseCoroutine());
    }

    private IEnumerator PauseCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        ani.enabled = true;
    }
}
