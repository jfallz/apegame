using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int distanceTraveled;
    public int maxDistance = 0;
    [SerializeField] public float currency;
    public Transform spawnPoint;
    public GameObject prefabToInstantiate;
    public bool dead = false; // used to show player shop or restart option (They Died lol)
    public bool restart = false; // if true, will restart the player and then reset bool to false
    

    public void Update() {
        if(restart)
            Restart();
        if(dead)
            Death();
    }


    public void Death() {
        // currency += Mathf.RoundToInt(distanceTraveled * .75f);  // add currency
        // if(Mathf.RoundToInt(distanceTraveled) > maxDistance)
        //     maxDistance = Mathf.RoundToInt(distanceTraveled);
        // distanceTraveled = 0;
        // dead = false;
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