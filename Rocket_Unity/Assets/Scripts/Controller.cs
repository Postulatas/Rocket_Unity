using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    Rigidbody rBody;

    [SerializeField] float Main_Thrust = 3000.0f;
    [SerializeField] float rcs_Thrust = 200f;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
    }

    void Update()
    {
        Controll();
        Thrust();
    }

    protected void LateUpdate()
     {
         transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
         transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
            rBody.AddRelativeForce(Vector3.up * Main_Thrust * Time.deltaTime);
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
