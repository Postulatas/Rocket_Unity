using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float tumble = 2f;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Start":
                Destroy(gameObject);
                break;
            case "Target":
                Destroy(gameObject);
                break;
        }
    }
}