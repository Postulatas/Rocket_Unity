using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Controll();
        Thrust();
    }

    [SerializeField] float Main_Thrust = 10.0f;

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rBody.AddRelativeForce(Vector3.up * Main_Thrust);
        }
    }

    [SerializeField] float rcs_Thrust = 200f;

    private void Controll()
    {
        rBody.freezeRotation = true;

        float rotationSpeed = rcs_Thrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        rBody.freezeRotation = false;
    }
}
