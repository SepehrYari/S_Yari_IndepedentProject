using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obsPrefab;
    private Vector3 spawnPos = new Vector3(3, 3, -20);
    private FirstPersonController Playercontroller;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObjects", 2, 2);
        Playercontroller = GameObject.Find("Player").GetComponent<FirstPersonController>();
    }


    void SpawnObjects()
    {
       if (Playercontroller.GameOver == false)
       {
            Instantiate(obsPrefab, spawnPos, obsPrefab.transform.rotation);
       }
       
    }
}
