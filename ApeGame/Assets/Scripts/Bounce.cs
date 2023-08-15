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
            rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
            rb.velocity += new Vector3(0f, upBoost * 10, forwardBoost * 10);
            print("player boosted");
        } else {
            return;
        }

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