using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    Enemy Enemy;
    public bool activated;
    public int damage;
    private float rotationSpeed;
    public bool HitRegistered = false;
    SpawnManager GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("Game Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {

        if (activated)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Debug.Log("the Great Cube is stuck on " + collision.gameObject.name + "!!!");
            GetComponent<Rigidbody>().Sleep();
            GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            GetComponent<Rigidbody>().isKinematic = true;
            activated = false;
        }
        if (collision.gameObject.name == "Enemy(Clone)")
        {
            
            Destroy(collision.gameObject);
            GameManager.UpdateScore(1);
            HitRegistered = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Breakable"))
        {
            if(other.GetComponent<BreakBoxScript>() != null)
            {
                other.GetComponent<BreakBoxScript>().Break();
            }
        }

        if (other.tag == "Enemy")
        {
            GameManager.UpdateScore(1);
            Enemy.DestroyEnemy();
        }
        Debug.Log(other.name);

    }
}
