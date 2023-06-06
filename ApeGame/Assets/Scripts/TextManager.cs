using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public GameManager man;
    public GameObject distance;
    public GameObject currency;

    void FixedUpdate()
    {
        man = FindObjectOfType<GameManager>();
        TMPro.TextMeshProUGUI d = distance.GetComponent<TMPro.TextMeshProUGUI>();
        d.text = "Distance: " + man.distanceTraveled;

        TMPro.TextMeshProUGUI c = currency.GetComponent<TMPro.TextMeshProUGUI>();
        c.text = "Currency: " + man.currency;
    }
}
