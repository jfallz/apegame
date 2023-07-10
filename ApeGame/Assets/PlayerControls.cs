using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] public float boosterSpeed;
    [SerializeField] public float boosterFuel;

    [SerializeField] public float acceleration = 1f;

    private Rigidbody rb;

    void Start() {
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
