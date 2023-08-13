using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    GameObject player;
    float speed;
    public float angle;
    public float maxSpeed; // Maximum speed value for full rotation
    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation = Quaternion.Euler(0f, 0f, -270f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate rotation angle based on player's speed
            float normalizedSpeed = Mathf.Clamp01(player.GetComponent<Rigidbody>().velocity.magnitude / maxSpeed);
            float targetRotation = 270 + (normalizedSpeed * 180f); // 180 degrees for full rotation

            // Apply rotation to the needle
            transform.localRotation = Quaternion.Euler(0f, 0f, -targetRotation);
        } else {
            player = GameObject.FindWithTag("Player");
        }
    }
}
