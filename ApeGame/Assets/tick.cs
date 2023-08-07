using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tick : MonoBehaviour
{
    private float start, curr; 
    private bool actionExecuted = false;
    [SerializeField] public AudioSource[] tickArray;
    
    void Start() {
        start = transform.eulerAngles.x;
        curr = start;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (!actionExecuted)
                {
                // Execute your action here
                    start = transform.eulerAngles.x;
                    curr = start;
                    tickArray[Random.Range(0, 3)].Play();
                    actionExecuted = true;
                }
            }

        curr = transform.eulerAngles.x;
        if(Mathf.Abs(Mathf.Abs(start) - Mathf.Abs(curr)) >= 15) {
            tickArray[Random.Range(0, 3)].Play();
            start = curr;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            actionExecuted = false;
        
        if(Input.GetKeyDown(KeyCode.Space)) {
            Destroy(this);
        }

    }
    
}
