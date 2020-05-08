using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] float Velocity = 0.05f;

    void Update()
    {
        var x = transform.position.x;
        x -= Velocity;
        transform.position = new Vector2(x, transform.position.y);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}