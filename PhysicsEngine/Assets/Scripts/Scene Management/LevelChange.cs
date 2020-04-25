using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator ani;
    public string nextLevel = "LevelChooser";
    [HideInInspector]
    public bool change;

    // Update is called once per frame
    void Update()
    {
        if (change)
        {
            Fade();
        }

    }

    public void Fade()
    {
        //nextLevel = levelName;
        ani.SetTrigger("FadeOut");
    }

    public void LoadLevel(/**string level*/)
    {
        print("getrr");
        SceneManager.LoadScene(nextLevel);
    }

    public void LoadWorld()
    {
        SceneManager.LoadScene("World");
    }
}
