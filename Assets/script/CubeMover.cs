using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMover : MonoBehaviour
{
    public float moveDistance = 5f; // Set the distance to move
    public float moveSpeed = 2f;    // Set the movement speed

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMovingForward = true;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.forward * moveDistance;
    }

    private void Update()
    {
        InvokeRepeating("CubeMotion", 5, 10);
    }

    private void CubeMotion()
    {
        // Move the object between start and target positions
        if (isMovingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // Check if the object has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMovingForward = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            // Check if the object has returned to the starting position
            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                isMovingForward = true;
            }
        }
    }
}
