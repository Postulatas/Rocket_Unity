using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public Rigidbody rb;
    const float G = 667.4f;

    public static List<Attractor> Attractors;

    private void FixedUpdate()
    {
        foreach (Attractor attractor in Attractors)
        {
            if(attractor != this)
            {
                Attract(attractor);
            }
        }
    }

    private void OnEnable() // nesuprantu
    {
        if (Attractors == null)
            Attractors = new List<Attractor>();

        Attractors.Add(this);
    }

    private void OnDisable() // irgi nesuprantu
    {
        Attractors.Remove(this);
    }

    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}
