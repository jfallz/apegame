using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int distanceTraveled;
    public static float currency;
    public Transform spawnPoint;
    public GameObject DeathMenu;
    public GameObject Shop;
    public GameObject prefabToInstantiate;
    public static bool dead = false; // used to show player shop or restart option (They Died lol)
    public static bool restart = false; // if true, will restart the player and then reset bool to false
    

    public void Update() {
        if(distanceTraveled != 0) {
            TMPro.TextMeshProUGUI textMeshPro = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
            textMeshPro.text = "Distance: " + distanceTraveled;
        }
        if(restart)
            Restart();
        if(dead)
            Death();
    }


    public void Death() {
        DeathMenu.SetActive(true);
        dead = false;
    }

    public void ShopMenu() {
        DeathMenu.SetActive(false);
        Shop.SetActive(true);
    }


    public void Restart() {
            // tell gamemanager we're no longer dead
            dead = false;
            // destroy all relevant gameobjects
            Shop.SetActive(false);
            // destroy all relevant gameobjects
            DeathMenu.SetActive(false);
            Destroy(GameObject.FindWithTag("CartCracked"));
            Destroy(GameObject.FindWithTag("GameController"));
            Destroy(GameObject.FindWithTag("Cart"));
            Destroy(GameObject.FindWithTag("Player"));
            Instantiate(prefabToInstantiate, spawnPoint.position, Quaternion.identity); // instantiate new player
            currency += Mathf.RoundToInt(distanceTraveled * .75f);  // add currency
            distanceTraveled = 0;
            restart = false;
    }
}
