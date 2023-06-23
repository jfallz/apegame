using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //[SerializeField] public Vector3 jumpBoost = new Vector3(0f, .05f, 0f);
    //[SerializeField] public Vector3 forwardBoost = new Vector3(0f, 0f, .05f);
    [SerializeField] public float upBoost = 25f;
    [SerializeField] public float forwardBoost = 25f;
    private bool boosted = false;
    private float desiredTime = .1f;
    private float timer = 0f;

    public void OnTriggerEnter(Collider a)
    {
        // boosted = true;
        Rigidbody rb;
        if(a.CompareTag("Player")) {
            rb = GameObject.FindWithTag("EditorOnly").GetComponent<Rigidbody>();
            rb.velocity += new Vector3(0f, upBoost * 10, forwardBoost * 10);
            print("player boosted");
        } else if(a.CompareTag("Cart")) {
            rb = a.GetComponentInParent<Rigidbody>();
            Vector3 currentVelocity = rb.velocity;
            Vector3 newVelocity = new Vector3(currentVelocity.x, 1f, currentVelocity.z);
            rb.AddForce(newVelocity - currentVelocity, ForceMode.VelocityChange); 
            rb.AddForce(Vector3.up * upBoost, ForceMode.VelocityChange);         // applying jump boost
            rb.AddForce(Vector3.forward * forwardBoost, ForceMode.VelocityChange);         // applying forward boost
            print("cart boosted");
        } else {
            return;
        }
        Animator animator = GetComponent<Animator>();
        animator.Play("boing");

    }

    // public void Update() {
    //     if(boosted) {
    //         timer += Time.deltaTime;
    //             if(timer >= desiredTime) {
    //                 boosted = false;
    //                 timer = 0f;
    //             }
    //     }
    // }
}