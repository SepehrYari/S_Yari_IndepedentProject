using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private FirstPersonController PM;
    public Transform cam;
    public Transform GunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer Line;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 GrapplePoint;


    [Header("CoolDown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool grappling;

    private void Start()
    {
        PM = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if (grapplingCd > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            Line.SetPosition(0, GunTip.position);
        }
    }


    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;

        grappling = true;

        PM.freeze = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            GrapplePoint = hit.point;

            Invoke(nameof(ExcuteGrapple), grappleDelayTime);
        }
        else
        {
            GrapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }
        Line.enabled = true;
        Line.SetPosition(1, GrapplePoint);
    }

    private void ExcuteGrapple()
    {
        PM.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = GrapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;


        Invoke(nameof(StopGrapple), 1f);

    }

    public void StopGrapple()
    {
        PM.freeze = false;

        grappling = false;

        grapplingCdTimer = grapplingCd;

        Line.enabled = false;
    }


}
