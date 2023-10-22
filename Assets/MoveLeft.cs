using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float Speed = 2;
    private FirstPersonController FirstPersonController;
    private float leftBounds = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        

        if (gameObject.transform.position.z < leftBounds && gameObject.CompareTag("FireBall"))
        {
            Destroy(gameObject);
        }

    }
}
