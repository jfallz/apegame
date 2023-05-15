using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    public float distance = 0f;
    public float init_distance = 0f;
    public TMPro.TextMeshProUGUI textMeshPro;

    Collider other = null;

    public void OnTriggerEnter(Collider a)
    {
        other = a;
        Debug.Log(other.gameObject.name);
        init_distance = other.gameObject.transform.position.z;
        textMeshPro = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
        textMeshPro.text = "Distance: " + distance;
        LateUpdate();
    }

    public void LateUpdate() {
            distance = other.gameObject.transform.position.z - init_distance;
            textMeshPro = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
            textMeshPro.text = "Distance: " + distance;
    }
}
