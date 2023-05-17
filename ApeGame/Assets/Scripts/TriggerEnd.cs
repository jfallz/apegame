using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    public GameObject fracturedPrefab;
    public GameObject playerPrefab;

    private float desiredTime = 1f;
    private float timer = 0f;

    private Rigidbody p;
    private bool moving = false;

    public void OnTriggerEnter(Collider a)
    {
        string collidedObjectTag = a.gameObject.tag;
        Debug.Log("Collided object tag: " + collidedObjectTag);
        if(!a.CompareTag("Cart"))
            return;

        GameObject cartObject = GameObject.FindWithTag("Cart");
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 cartPos = cartObject.transform.position;
        Vector3 playerPos = player.transform.position;

        Rigidbody otherRb = a.GetComponentInParent<Rigidbody>();

        if(cartObject != null) {
            Destroy(cartObject);
            Destroy(player);

            GameObject newPlayer;
            
            Instantiate(fracturedPrefab, cartPos, Quaternion.identity);
            newPlayer = Instantiate(playerPrefab, playerPos, Quaternion.identity);

            Rigidbody rb = newPlayer.GetComponent<Rigidbody>();
            Vector3 vel = otherRb.velocity;
            rb.velocity = vel * 1.25f; // this will adjust how much velocity is kept  after  cart explosion
            p = rb;
            moving = true;
        }
    }


    public void Update() {
        if(moving) {
            if(p.velocity.magnitude < 1) {
                timer += Time.deltaTime;
                if(timer >= desiredTime) {
                    TriggerStart.timer = false;
                    print("Telling GameManager to restart");
                    moving = false;
                    GameManager.restart = true;
                }
            } else {
                timer = 0f;
            }
        }
    }
}
