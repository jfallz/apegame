using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
    [SerializeField] float force = 0f;
    [SerializeField] float maxSpeed = 0f;

    [SerializeField] public float acceleration = 1f;

    private Rigidbody rb;


    private void Force(bool forwards) {
        rb = GetComponent<Rigidbody>();
        if(forwards)
            rb.AddForce(Vector3.forward * acceleration, ForceMode.Acceleration);   // move forwards
        else
            rb.AddForce((Vector3.forward - (Vector3.forward*2)) * acceleration, ForceMode.Acceleration); // move backwards
    }

    // Update is called once per frame
    private void Update() {
        if(Input.GetKey(KeyCode.D)) // if pressing d
            Force(true);
        else if(Input.GetKey(KeyCode.A))    // if pressing a
            Force(false);
    }
}
