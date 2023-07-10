using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] public float boosterSpeed;
    [SerializeField] public float boosterFuel;

    [SerializeField] public float acceleration = 1f;
    public Vector3 torque = new Vector3(.07f, 0f, 0f);

    private Rigidbody rb;

    void Start() {
        Rigidbody rb = GetComponent<Rigidbody>();
    }


     public void TurnLeft() {
        Rigidbody rb = GetComponent<Rigidbody>();
        // Calculate the torque vector based on the desired torque amount and the object's up direction
        //Vector3 torque = new Vector3(-.07f, 0f, 0f);
        rb.angularVelocity += -1 * torque;
     }

     public void TurnRight() {
        
        Rigidbody rb = GetComponent<Rigidbody>();
        // Calculate the torque vector based on the desired torque amount and the object's up direction
        rb.angularVelocity += torque;
     }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.D)) // if pressing d
            TurnRight();
        else if(Input.GetKey(KeyCode.A))    // if pressing a
            TurnLeft();
    }

}
