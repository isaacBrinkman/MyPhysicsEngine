using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawnerMover : MonoBehaviour
{
    public List<GameObject> shapesMY;
    public List<GameObject> shapesUn;
    private GameObject shape1, shape2;
    private Rigidbody rgb;
    public float speed;
    private bool spawn = true;
    private GameObject currentShape;
    public int spawnTime;
    private GameObject mySpawn;
    private GameObject unitySpawn;
    public int range = 10;


    // Start is called before the first frame update
    void Start()
    {
        spawn = true;
        mySpawn = transform.GetChild(0).gameObject;
        unitySpawn = transform.GetChild(1).gameObject;
        rgb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CalcSpeed();
        Spawn();
    }


    private void CalcSpeed()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (speed > 0)
        {
            // check if transform is greater than range
            if (transform.position.x < -range)
            {
                speed = -speed;
            }
        }
        else
        {
            if (transform.position.x > range)
            {
                speed = -speed;
            }
        }
      
    }

    private void Spawn()
    {
        if (spawn)
        {
            int randIndex = Random.Range(0, shapesMY.Capacity);
            currentShape = shapesMY[randIndex];
            shape1 = Instantiate(currentShape, mySpawn.transform.position, Quaternion.identity, transform);
            shape2 = Instantiate(shapesUn[randIndex], unitySpawn.transform.position, Quaternion.identity, transform);
            shape1.GetComponent<MyRigidBody>().useGravity = false;
            shape2.GetComponent<Rigidbody>().useGravity = false;
            //shape1.GetComponent<MyCollider>().c = StacksFloor.floor.GetComponent<MyCollider>();
            spawn = false;
            //StacksFloor.floor.shape = shape.GetComponent<MyCollider>();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("here");
            shape1.transform.parent = null;
            shape1.GetComponent<MyRigidBody>().useGravity = true;
            shape2.transform.parent = null;
            shape2.GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(SpawnCorutine());
        }

    }

    private IEnumerator SpawnCorutine()
    {
        yield return new WaitForSeconds(spawnTime);
        spawn = true;
    }




}
 

