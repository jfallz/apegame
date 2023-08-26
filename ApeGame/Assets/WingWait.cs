using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingWait : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndExecute());
    }

    	private IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 2 seconds
        
        // Execute the action after waiting
        Debug.Log("Action executed after 2 seconds!");
        transform.GetComponent<SimpleWing>().enabled = true;
    }
}
