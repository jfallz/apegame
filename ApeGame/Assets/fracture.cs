using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fracture : MonoBehaviour
{
    public GameObject fractured;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 oldPos = transform.position;
            Instantiate(fractured, oldPos, Quaternion.identity);
            Destroy(gameObject);
        }


    }
}
