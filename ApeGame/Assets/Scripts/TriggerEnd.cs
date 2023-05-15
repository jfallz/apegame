using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnd : MonoBehaviour
{
    public void OnTriggerEnter(Collider a)
    {
        TriggerStart.timer = false;
    }
}
