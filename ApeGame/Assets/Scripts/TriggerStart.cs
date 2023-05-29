using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStart : MonoBehaviour
{
    public GameObject GameMan;
    public int init_distance;
    public TMPro.TextMeshProUGUI textMeshPro;

    public static bool timer = false;

    public void OnTriggerEnter(Collider a)
    {
        if(!a.CompareTag("Cart"))
            return;
        GameObject other = GameObject.FindWithTag("Player");
        Debug.Log(other.gameObject.name);
        init_distance = Mathf.RoundToInt(other.gameObject.transform.position.z);
        timer = true;
    }

    public void Update() {
        if(timer) {
            GameObject other = GameObject.FindWithTag("Player");
            int d = Mathf.RoundToInt(other.gameObject.transform.position.z) - init_distance;
            GameManager script = GameMan.GetComponent<GameManager>();
            script.distanceTraveled = d;
        }
           // textMeshPro = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
            //textMeshPro.text = "Distance: " + distance;
    }
}
