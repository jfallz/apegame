using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static int distanceTraveled;
    public static float currency;
    public Transform spawnPoint;
    public GameObject prefabToInstantiate;
    public static bool restart = false; // if true, will restart the player and then reset bool to false

    public void Update() {
        if(distanceTraveled != 0) {
            TMPro.TextMeshProUGUI textMeshPro = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
            textMeshPro.text = "Distance: " + distanceTraveled;
        }
        if(restart) {
            Destroy(GameObject.FindWithTag("CartCracked"));
            Destroy(GameObject.FindWithTag("GameController"));
            Destroy(GameObject.FindWithTag("Cart"));
            Destroy(GameObject.FindWithTag("Player"));
            Instantiate(prefabToInstantiate, spawnPoint.position, Quaternion.identity);
            currency += Mathf.RoundToInt(distanceTraveled * .75f);
            distanceTraveled = 0;
            restart = false;
        }
    }
}
