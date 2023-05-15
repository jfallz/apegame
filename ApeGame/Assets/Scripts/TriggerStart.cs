using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStart : MonoBehaviour
{
    public float init_distance = 0f;
    public TMPro.TextMeshProUGUI textMeshPro;

    Collider other = null;
    public static bool timer = false;

    public void OnTriggerEnter(Collider a)
    {
        other = a;
        Debug.Log(other.gameObject.name);
        init_distance = other.gameObject.transform.position.z;
        timer = true;
    }

    public void Update() {
        if(timer) {
            float d = other.gameObject.transform.position.z - init_distance;
            GameManager.distanceTraveled = d;
        }
           // textMeshPro = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
            //textMeshPro.text = "Distance: " + distance;
    }
}
