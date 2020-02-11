using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float tumble = 2;
    [SerializeField] float speed = 10f;

    public GameObject earth;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
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