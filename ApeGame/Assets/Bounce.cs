using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    //[SerializeField] public Vector3 jumpBoost = new Vector3(0f, .05f, 0f);
    //[SerializeField] public Vector3 forwardBoost = new Vector3(0f, 0f, .05f);
    [SerializeField] public float upBoost = 55f;
    [SerializeField] public float forwardBoost = 55f;
    private bool boosted = false;
    private float desiredTime = .1f;
    private float timer = 0f;

    public void OnTriggerEnter(Collider a)
    {
        if(boosted)
            return;
        boosted = true;
        Rigidbody rb;
        if(a.CompareTag("Player")) {
            rb = a.GetComponent<Rigidbody>();
            print("player boosted");
        } else if(a.CompareTag("Cart")) {
            rb = a.GetComponentInParent<Rigidbody>();
            print("cart boosted");
        } else {
            return;
        }
        Vector3 currentVelocity = rb.velocity;
        Vector3 newVelocity = new Vector3(currentVelocity.x, 1f, currentVelocity.z);

        rb.AddForce(newVelocity - currentVelocity, ForceMode.VelocityChange); 

        rb.AddForce(Vector3.up * upBoost, ForceMode.VelocityChange);         // applying jump boost
        rb.AddForce(Vector3.forward * forwardBoost, ForceMode.VelocityChange);         // applying forward boost

    }

    public void Update() {
        if(boosted) {
            timer += Time.deltaTime;
                if(timer >= desiredTime) {
                    boosted = false;
                    timer = 0f;
                }
        }
    }
}