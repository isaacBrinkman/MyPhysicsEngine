using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntraSceneTrigger : MonoBehaviour
{
    // These decide what type of stop will happen
    public bool invisible;          
    public bool stopped;
    public bool active;

    public float waitTime;          // time while the screen is black

    public float resumeTime;         // time until resume


   // [SerializeField] private GameObject go;
    private IntraScene intraScene;      
    private bool onDoor;

   

    void Start()
    {
      //  IntraScene intraScene = go.GetComponent<IntraScene>();
        intraScene = FindObjectOfType<IntraScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onDoor)
        {
            print("onDoor");
            print("Enter2");

            if (active)
            {
                intraScene.active = true;
                intraScene.triggered = true;
                intraScene.waitTime = waitTime;

            }
            if (invisible)
            {
                intraScene.invisible = true;
                intraScene.triggered = true;
                intraScene.waitTime = waitTime;

            }
            if (stopped)
            {
                intraScene.stopped = true;
                intraScene.triggered = true;
                intraScene.waitTime = waitTime;

            }
            onDoor = false;
            StartCoroutine(Resume());
            GetComponent<Collider2D>().enabled = false;
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Enter");
            onDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onDoor = false;
    }



    private void OnDrawGizmos()
    {
        // once out of the collision make it false
        Gizmos.color = Color.red;
        onDoor = false;
        Vector3 cube = new Vector3(GetComponent<BoxCollider2D>().size.x, GetComponent<BoxCollider2D>().size.y);
        
        Gizmos.DrawCube(transform.position,cube);
    }

    private IEnumerator Resume()
    {
        yield return new WaitForSeconds(resumeTime);
        intraScene.resume = true;


    }
}
