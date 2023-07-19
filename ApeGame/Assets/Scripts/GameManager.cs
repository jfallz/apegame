using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CannonFire scriptRef;
    public int distanceTraveled;
    public int maxDistance = 0;
    [SerializeField] public float currency;
    public GameObject prefabToInstantiate;
    public bool restart = false; // if true, will restart the player and then reset bool to false
    public float TimeT = 0f;
    private bool inFlight = false; 
    
    // public void Start() {
    //     scriptRef = GameObject.Find("Rotation").GetComponent<CannonFire>();
    // }

    public void FixedUpdate() {
        scriptRef = GameObject.Find("Rotation").GetComponent<CannonFire>();
        inFlight = !scriptRef.Aiming;
        print(inFlight);
        if(inFlight) {
            if(Physics.Raycast(GameObject.FindWithTag("Player").transform.position, Vector3.down, 7f)) {
                Rigidbody rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
                if(rb.velocity.magnitude < 1) 
                    Death();
            }
        }
    }


    public void Death() {
        GameObject targetObject = GameObject.FindWithTag("Cart");
        Vector3 targetPosition = targetObject.transform.position;
        Quaternion targetRotation = targetObject.transform.rotation;
        Destroy(targetObject);
        Destroy(GameObject.FindWithTag("Player"));

            // Instantiate the prefab at the target position with the same rotation.
        GameObject newObject = Instantiate(prefabToInstantiate, targetPosition, targetRotation);
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