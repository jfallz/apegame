using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float distanceTraveled = 0;

    public void Update() {
        if(distanceTraveled != 0) {
            TMPro.TextMeshProUGUI textMeshPro = GameObject.Find("DISTANCE").GetComponent<TMPro.TextMeshProUGUI>();
            textMeshPro.text = "Distance: " + distanceTraveled;
        }
    }
}
