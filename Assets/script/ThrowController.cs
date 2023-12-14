using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using Cinemachine;


public class ThrowController : MonoBehaviour
{

    
    private FirstPersonController input;
    private Rigidbody weaponRb;
    private WeaponScript weaponScript;
    private float returnTime;

    private Vector3 origLocPos;
    private Vector3 origLocRot;
    private Vector3 pullPosition;

    [Header("Public References")]
    public Transform weapon;
    public Transform hand;
    public Transform spine;
    public Transform curvePoint;
    [Space]
    [Header("Parameters")]
    public float throwPower = 30;
    [Space]
    [Header("Bools")]
    public bool walking = true;
    public bool aiming = false;
    public bool hasWeapon = true;
    public bool pulling = false;
    [Space]
    [Header("Particles")]
    public ParticleSystem glowParticle;
    public ParticleSystem ImpactParticle;
    public ParticleSystem catchParticle;
    public ParticleSystem trailParticle;

    void Start()
    {
        Cursor.visible = false;
        glowParticle.Play();
        catchParticle.Stop();
        trailParticle.Stop();
        ImpactParticle.Stop();
        
        input = GetComponent<FirstPersonController>();
        weaponRb = weapon.GetComponent<Rigidbody>();
        weaponScript = weapon.GetComponent<WeaponScript>();
        origLocPos = weapon.localPosition;
        origLocRot = weapon.localEulerAngles;


    }

    void Update()
    {
        if (input.GameOver)
        {
            glowParticle.Stop();
            trailParticle.Stop();
            catchParticle.Stop();
        }

        if (weaponScript.HitRegistered == true)
        {
            ImpactParticle.Play();
        }
        else
        {
            ImpactParticle.Stop();
        }


        if (Input.GetMouseButtonDown(0) && hasWeapon && input.GameOver == false)
        {
            WeaponThrow();
            
        }


        if (hasWeapon)
        {
            ImpactParticle.Stop();
        }

        else
        {
            if (Input.GetMouseButtonDown(1) && input.GameOver == false)
            {
                WeaponStartPull();
            }
        }

        if (pulling)
        {
            if(returnTime < 1)
            {
                weapon.position = GetQuadraticCurvePoint(returnTime, pullPosition, curvePoint.position, hand.position);
                returnTime += Time.deltaTime * 1.5f;
            }
            else
            {
                WeaponCatch();
                
            }
        }

        if (Input.GetKeyDown(KeyCode.R))//to reload the map for testing
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

    public void WeaponThrow()
    {
        pulling = false;
        hasWeapon = false;
        weaponScript.activated = true;
        weaponRb.isKinematic = false;
        weaponRb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        weapon.parent = null;
        weapon.eulerAngles = new Vector3(0, -90 +transform.eulerAngles.y, 0);
        weapon.transform.position += transform.right/5;
        weaponRb.AddForce(Camera.main.transform.forward * throwPower + transform.up * 2, ForceMode.Impulse);
        glowParticle.Stop();
        trailParticle.Play();
        input.animator.SetTrigger("Cube thrown");
    }

    public void WeaponStartPull()
    {
        pullPosition = weapon.position;
        weaponRb.Sleep();
        weaponRb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        weaponRb.isKinematic = true;
        weapon.DORotate(new Vector3(-90, -90, 0), .2f).SetEase(Ease.InOutSine);
        weapon.DOBlendableLocalRotateBy(Vector3.right * 90, .5f);
        weaponScript.activated = true;
        pulling = true;
        glowParticle.Stop();
        trailParticle.Stop();
        input.animator.SetTrigger("Cube Pulled");
    }

    public void WeaponCatch()
    {
        returnTime = 0;
        pulling = false;
        weapon.parent = hand;
        weaponScript.activated = false;
        weapon.localEulerAngles = origLocRot;
        weapon.localPosition = origLocPos;
        hasWeapon = true;

        
        catchParticle.Play();
        glowParticle.Play();

    }

    public Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        return (uu * p0) + (2 * u * t * p1) + (tt * p2);
    }

   
}
