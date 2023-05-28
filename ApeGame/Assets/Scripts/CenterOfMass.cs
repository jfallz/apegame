using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody r = GameObject.FindWithTag("GameController").GetComponent<Rigidbody>();
        //r.centerOfMass = r.centerOfMass * 1.2f;
    }
}
