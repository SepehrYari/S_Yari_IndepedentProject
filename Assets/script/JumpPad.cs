using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public FirstPersonController Player;
    public float NewGravity = 17.0f;
    public float NewJumpSpeed = 20.0f;
    public GameObject CPU;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            other.GetComponent<FirstPersonController>() ;
            Player.gravity = NewGravity;
            Player.jumpSpeed = NewJumpSpeed;
            CPU.SetActive(false);
        }
        
    }
}
