using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Material red;
    public Material blue;
    public float delay = 3.0f; // Delay between cube creation
    public int maxSize = 8;

    private List<GameObject> gameObjects;
    private int pointer;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new List<GameObject>();
        pointer = -1;
        //createCube();
        StartCoroutine(spawnObject());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Tried to create new cube when collision happens but it did not work :(
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (pointer == maxSize - 1)
    //    {
    //        destroyObjects(2);
    //    }

    //    if (pointer >= 2)
    //    {
    //        if (checkMatch())
    //        {
    //            destroyObjects(pointer);
    //        }
    //    }

    //    createCube();
    //}

    // Coroutine that spawns a cube every second
    IEnumerator spawnObject()
    {
        if (pointer == maxSize - 1)
        {
            destroyObjects(2);
        }
        createCube();
        yield return new WaitForSeconds(delay);
        if (pointer >= 2)
        {
            if (checkMatch())
            {
                destroyObjects(pointer);
            }
        }

        StartCoroutine(spawnObject());
    }

    public Boolean checkMatch()
    {
        if (gameObjects[pointer].tag == gameObjects[pointer-1].tag && gameObjects[pointer].tag == gameObjects[pointer - 2].tag)
        {
            return true;
        }
        return false;
    }
    public void destroyObjects(int index)
    {
        for (int i = index; i > index - 3; i--)
        {
            Destroy(gameObjects[i]);
            gameObjects.RemoveAt(i);
            pointer--;
        }
    }

    public void createCube()
    {
        int randomNum = UnityEngine.Random.Range(0, 2);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(0, 8, 0);
        cube.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        Rigidbody currentRb = cube.AddComponent<Rigidbody>();
        currentRb.detectCollisions = true;
        Collider collider = currentRb.GetComponent<Collider>();
        collider.material.bounciness = 0;
        cube.transform.parent = this.transform;

        switch (randomNum)
        {
            case 0:
                cube.GetComponent<MeshRenderer>().material = red;
                cube.tag = "red";
                break;
            case 1:
                cube.GetComponent<MeshRenderer>().material = blue;
                cube.tag = "blue";
                break;
        }

        gameObjects.Add(cube);
        pointer++;
    }
}
