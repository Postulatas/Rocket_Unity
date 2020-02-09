using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    Rigidbody rBody;

    [SerializeField] float Main_Thrust = 10.0f;
    [SerializeField] float rcs_Thrust = 200f;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Controll();
        Thrust();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Obstacle":
                print("dead");
                break;

        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rBody.AddRelativeForce(Vector3.up * Main_Thrust);
        }
    }

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
