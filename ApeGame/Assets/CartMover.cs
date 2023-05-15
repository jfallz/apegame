using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartMover : MonoBehaviour
{
    [SerializeField] float force = 0f;
    [SerializeField] bool applyForce = false;
    private Rigidbody rb;


    private void Force() {
        applyForce = false;

        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    private void Update() {
        if(applyForce)
            Force();
          
        
    }
}
