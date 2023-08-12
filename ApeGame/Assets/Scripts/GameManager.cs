using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public CannonFire scriptRef;
    public int distanceTraveled = 0;
    // ----
    public float maxDistance = 0f;
    public float maxSpeed = 0f;
    public float maxAltitude = 0f;
    // ----
    private int startingDistance;
    [SerializeField] public float currency = 0f;
    public GameObject prefabToInstantiate;
    public GameObject playerMenu;
    public bool restart = false; // if true, will restart the player and then reset bool to false
    public float TimeT = 0f;
    private bool inFlight = false; 
    private bool launched = false;
    public GameObject speedometer;
    public float min = 317f;
    public float max = 45f;
    private bool dead = false;

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
                if(rb.velocity.magnitude < 1 && !dead) 
                    Death();
            }

            // speedometer
            // float angle = min - (rb.velocity.magnitude / 100f);

            // speedometer.transform.eulerAngles = new Vector3(speedometer.transform.eulerAngles.x,
            //                                                 speedometer.transform.eulerAngles.y,
            //                                                 speedometer.transform.eulerAngles.z - angle);
            // speed = rb.velocity.magnitude;
            statTrack();
        }
    }

    public void statTrack() {
        GameObject p = GameObject.FindWithTag("Player");
        float speed = p.GetComponent<Rigidbody>().velocity.magnitude;
        distanceTraveled = (int)(p.transform.position.z) - startingDistance;
        TextMeshProUGUI d = GameObject.Find("DISTANCE").GetComponent<TextMeshProUGUI>();
        d.text = "DISTANCE: " + distanceTraveled;
        if(speed > maxSpeed) {
            maxSpeed = speed;
        }

        if(distanceTraveled > maxDistance)
            maxDistance = distanceTraveled;
    }
    public void Death() {
        currency += distanceTraveled * .25f;
        TextMeshProUGUI c = GameObject.Find("CURRENCY").GetComponent<TextMeshProUGUI>();
        c.text = "CURRENCY: " + (int)currency;
        playerMenu.SetActive(true);
        dead = true;
    }

    public void ShopMenu() {
    }

    public void Restart() {
        playerMenu.SetActive(false);
        GameObject targetObject = GameObject.FindWithTag("Cart");
        Vector3 targetPosition = targetObject.transform.position;
        Quaternion targetRotation = targetObject.transform.rotation;
        Destroy(targetObject);
        Destroy(GameObject.FindWithTag("Player"));
            // Instantiate the prefab at the target position with the same rotation.
        GameObject newObject = Instantiate(prefabToInstantiate, targetPosition, targetRotation);
        launched = false;
        dead = false;
        distanceTraveled = 0;

    }
}