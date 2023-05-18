using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] public Vector3 jumpBoost = new Vector3(0f, .05f, 0f);
    [SerializeField] public Vector3 forwardBoost = new Vector3(0f, 0f, .05f);
    private bool boosted = false;
    private float desiredTime = 1f;
    private float timer = 0f;

    public void OnTriggerEnter(Collider a)
    {
        if(boosted)
            return;
        boosted = true;
        Rigidbody rb = a.GetComponentInParent<Rigidbody>();
        print("boosted");
        rb.AddForce(Vector3.up * 55f, ForceMode.VelocityChange);
        rb.AddForce(Vector3.forward * 35f, ForceMode.VelocityChange);

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
