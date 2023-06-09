using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    public GameObject fracturedPrefab;
    public GameObject playerPrefab;
    public GameObject GameMan;

    private float desiredTime = 1f;
    private float timer = 0f;

    private Rigidbody p;
    private bool moving = false;

    public void OnTriggerEnter(Collider a)
    {
        string collidedObjectTag = a.gameObject.tag;
        if(!a.CompareTag("Cart") || moving)
            return;

        GameObject cartObject = GameObject.FindWithTag("Cart");
        GameObject gameController = GameObject.FindWithTag("GameController");
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 cartPos = cartObject.transform.position;
        Vector3 playerPos = player.transform.position;

        Rigidbody otherRb = a.GetComponentInParent<Rigidbody>();

        if(cartObject != null) {
            Vector3 vel = otherRb.velocity;
            vel += new Vector3(0f, 10f, 2f);
            Destroy(player);
            Destroy(gameController);

            GameObject newPlayer;
            Vector3 newCartPos = new Vector3(cartPos.x, cartPos.y - 2.26f, cartPos.z);
            GameObject tempCart = Instantiate(fracturedPrefab, newCartPos, Quaternion.identity);
            //tempCart = Instantiate(fracturedPrefab, newCartPos, Quaternion.identity);


            newPlayer = Instantiate(playerPrefab, playerPos, Quaternion.identity);
            Rigidbody rb = newPlayer.GetComponent<Rigidbody>();
            vel.y = Mathf.Abs(vel.y) / 2f;
            vel.z = Mathf.Abs(vel.z);
            rb.velocity = vel * 1.25f;
            rb.AddTorque(Vector3.forward * rb.velocity.z);


            for(int i = 0; i < tempCart.transform.childCount; ++i) {
                GameObject child = tempCart.transform.GetChild(i).gameObject;
                Rigidbody childRb = child.GetComponent<Rigidbody>();
                childRb.velocity = vel;
            }
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
                    print("Telling GameManager that we're dead");
                    moving = false;
                    GameManager script = GameMan.GetComponent<GameManager>();
                    script.dead = true;
                }
            } else {
                timer = 0f;
            }
        }
    }
}
