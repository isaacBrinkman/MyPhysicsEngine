using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * this is in charge of turning on the pause menu when esc is pressed and removing it when pressed again
 * DOES NOT FREEZE THE GAME
 */
public class PauseManager : MonoBehaviour
{
    private bool pause = false;
    public GameObject menu;
    public bool pauseable = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseable)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = !pause;
            }
            if (pause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
        print(Time.timeScale);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        menu.SetActive(true);
        pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        pause = false;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    
    // load the ui for when you lose currently called by tacksFloor
    public void LoseScreen()
    {
        Time.timeScale = 0;
        pauseable = false;
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
