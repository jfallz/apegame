using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    // public Transform target;          // The subject to follow
    public float smoothTime = 0.2f;   // The smoothing time
    public Vector3 offset;            // The offset from the target's position
    private Vector3 velocity;         // Velocity for damping effect
    private Transform target;
    private GameObject player;

    private void LateUpdate()
    {
        player = GameObject.FindWithTag("Player");
        target = player.GetComponent<Transform>();
            // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;
            // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
            // Update the camera's position
        transform.position = smoothedPosition;
    }
}