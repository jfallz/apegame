using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public CannonFire scriptRef;
    public int distanceTraveled = 0;
    public int maxDistance = 0;
    private float speed = 0f;
    private int startingDistance;
    [SerializeField] public float currency;
    public GameObject prefabToInstantiate;
    public bool restart = false; // if true, will restart the player and then reset bool to false
    public float TimeT = 0f;
    private bool inFlight = false; 
    private bool launched = false;
    public GameObject speedometer;
    public float min = 311f;
    public float max = 50f;

    // public void Start() {
    //     scriptRef = GameObject.Find("Rotation").GetComponent<CannonFire>();
    // }

    public void FixedUpdate() {
        scriptRef = GameObject.Find("Rotation").GetComponent<CannonFire>();
        inFlight = !scriptRef.Aiming;
        if(inFlight) {
            Rigidbody rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
            if (!launched) {
                startingDistance = (int)GameObject.FindWithTag("Player").transform.position.z;
                launched = true;

            }
            if(Physics.Raycast(GameObject.FindWithTag("Player").transform.position, Vector3.down, 7f)) {
                if(rb.velocity.magnitude < 1) 
                    Death();
            }
            float angle = min - rb.velocity.magnitude / 2f;

            speedometer.transform.eulerAngles = new Vector3(speedometer.transform.eulerAngles.x,
                                                            speedometer.transform.eulerAngles.y,
                                                            angle);
            speed = rb.velocity.magnitude;
            statTrack();
        }
    }

    public void statTrack() {
        GameObject p = GameObject.FindWithTag("Player");
        distanceTraveled = (int)(p.transform.position.z) - startingDistance;
        TextMeshProUGUI d = GameObject.Find("DISTANCE").GetComponent<TextMeshProUGUI>();
        d.text = "DISTANCE: " + distanceTraveled;
    }
    public void Death() {
        GameObject targetObject = GameObject.FindWithTag("Cart");
        Vector3 targetPosition = targetObject.transform.position;
        Quaternion targetRotation = targetObject.transform.rotation;
        Destroy(targetObject);
        Destroy(GameObject.FindWithTag("Player"));

            // Instantiate the prefab at the target position with the same rotation.
        GameObject newObject = Instantiate(prefabToInstantiate, targetPosition, targetRotation);

        currency = distanceTraveled * .25f;
        TextMeshProUGUI c = GameObject.Find("CURRENCY").GetComponent<TextMeshProUGUI>();
        c.text = "CURRENCY: " + (int)currency;
        distanceTraveled = 0;
        launched = false;

    }

    public void ShopMenu() {
    }

    public void Restart() {
            // // tell gamemanager we're no longer dead
            // dead = false;

            // Destroy(GameObject.FindWithTag("CartCracked"));
            // Destroy(GameObject.FindWithTag("GameController"));
            // Destroy(GameObject.FindWithTag("Cart"));
            // Destroy(GameObject.Find("Ragdoll"));
            // GameObject player = Instantiate(prefabToInstantiate, spawnPoint.position, Quaternion.identity); // instantiate new cannon
            // Shop.GetComponent<ShopManager>().PerformUpgrades(player);
            // restart = false;
    }
}