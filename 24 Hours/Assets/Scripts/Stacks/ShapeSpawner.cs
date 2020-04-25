using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{
    //public List<GameObject> shapes;
    //public float speed;
    //public bool testTrigger;
    //[Range(-0, 20f)]
    //public float range;
    //public float spawnTime;

    //private GameObject currentShape;
    //[HideInInspector]
    //public GameObject shape;
    //private bool spawn = true;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // spawn a shape only if there isn't one already
    //    if (spawn)
    //    {
    //        int randIndex = Random.Range(0, shapes.Capacity);
    //        currentShape = shapes[randIndex];
    //        shape = Instantiate(currentShape, transform.position, Quaternion.identity, transform);
    //        shape.GetComponent<MyRigidBody>().useGravity = false;
    //        shape.GetComponent<MyCollider>().c = StacksFloor.floor.GetComponent<MyCollider>();
    //        spawn = false;
    //        StacksFloor.floor.shape = shape.GetComponent<MyCollider>();
    //    }
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        shape.transform.parent = null;
    //        shape.GetComponent<MyRigidBody>().useGravity = true;
    //        StartCoroutine(SpawnCorutine());
    //    }
        
    //    //CalcSpeed();
              
    //}

    //private IEnumerator SpawnCorutine()
    //{
    //    yield return new WaitForSeconds(spawnTime);
    //    spawn = true;
    //}

    //private void CalcSpeed()
    //{
    //    transform.position += Vector3.left * speed * Time.deltaTime;
    //    if (speed > 0)
    //    {
    //        // check if transform is greater than range
    //        if (transform.position.x < -range)
    //        {
    //            speed = -speed;
    //        }
    //    }
    //    else
    //    {
    //        if (transform.position.x > range)
    //        {
    //            speed = -speed;
    //        }
    //    }
    //    if (testTrigger)
    //    {
    //        speed = -speed;
    //        testTrigger = false;
    //    }
    //}


}
