using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject earth;

    [SerializeField] float speed = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        OrbitAround();
    }

    private void OrbitAround()
    {
        transform.RotateAround(earth.transform.position, -Vector3.forward, speed * Time.deltaTime * UnityEngine.Random.Range(0.0f, 1.0f));
    }
}
