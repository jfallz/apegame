using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    void FixedUpdate()
    {
        TMPro.TextMeshProUGUI d = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
        d.text = "Distance: " + GameManager.distanceTraveled;

        TMPro.TextMeshProUGUI c = GameObject.Find("CURRENCY").GetComponent<TMPro.TextMeshProUGUI>();
        c.text = "Currency: " + GameManager.currency;
    }
}
