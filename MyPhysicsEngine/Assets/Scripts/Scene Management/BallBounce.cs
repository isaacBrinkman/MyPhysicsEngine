using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallBounce : MonoBehaviour
{
    public unityBall unBall;
    public MyRGB myBall;
    public int count;
    public float maxRange;
    public Text amount;
    public Text dropAmount;
    public Text sceneName;
    public Slider slide;
    public MyRGB ballMyPhysics;

    public void DropButton()
    {
  
        int curr = int.Parse(amount.text);
        curr += count;
        amount.text = curr.ToString();
        for (int i = 0; i < count; i++)
        {
            //float posX = Random.Range(-maxRange, maxRange);
            float posY = Random.Range(-5f, 3f);
            float posX = Random.Range(-5.7f, 5.7f);
            Vector3 pos = new Vector3(posX, posY);

            if (sceneName.text == "Unity Physics")
            {

                unityBall uball = Instantiate(unBall, pos, Quaternion.identity);
                uball.velocity = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            }
            else
            {
                MyRGB mball = Instantiate(myBall, pos, Quaternion.identity);
                if(mball.cc.allColliders.Length > 0)
                {
                    foreach (MyCollider2D c in mball.cc.allColliders)
                    {

                    }
                }

                mball.velocity = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            }

        }
        
    }

    public void Slider()
    {
        float v = slide.value;
        count = (int)Mathf.Round((v * 19)) + 1;
        dropAmount.text = count.ToString();
    }

    public void Reset(string scene)
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    
    }

    public void SwitchPhysics()
    {
        if(sceneName.text == "My Physics")
        {
            sceneName.text = "Unity Physics";
            
        }
        else
        {
            sceneName.text = "My Physics";

        }

    }

}
