using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropBallUI : MonoBehaviour
{
    public GameObject ball;
    public int count;
    public float maxRange;
    public Text amount;
    public Text dropAmount;
    public Slider slide;
    public GameObject ballMyPhysics;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropButton()
    {
        if (SceneManager.GetActiveScene().name == "Ball Drop Test")
        {
            int curr = int.Parse(amount.text);
            curr += count;
            amount.text = curr.ToString();
            for (int i = 0; i < count; i++)
            {
                float posX = Random.Range(-maxRange, maxRange);
                Vector3 pos = new Vector3(posX, 2.5f);
                Instantiate(ball, pos, Quaternion.identity);
            }
        }
        else
        {
            int curr = int.Parse(amount.text);
            curr += count;
            amount.text = curr.ToString();
            for (int i = 0; i < count; i++)
            {
                float posX = Random.Range(-maxRange, maxRange);
                Vector3 pos = new Vector3(posX, 2.5f);
                Instantiate(ballMyPhysics, pos, Quaternion.identity);
            }
        }
    }

    public void Slider()
    {
        float v = slide.value;
        count = (int)Mathf.Round((v * 99)) + 1;
        dropAmount.text = count.ToString();
    }
    
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SwitchPhysics()
    {
        if(SceneManager.GetActiveScene().name == "Ball Drop Test")
        {
            SceneManager.LoadScene("MyBallDrop");
        }
        else
        {
            SceneManager.LoadScene("Ball Drop Test");

        }
    }

}
