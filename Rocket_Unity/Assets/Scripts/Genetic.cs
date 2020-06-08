using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genetic : MonoBehaviour
{
    System.Random random = new System.Random();
    Rigidbody rb;
    public NeuralNetwork nn = new NeuralNetwork();    
    public LayerMask mask1 = 1 << 9;//asteroid
    public LayerMask mask2 = 1 << 10;//forest
    private double[] input = new double[14];
    public double fitness;
    public float d;
    public bool collided = false;
    public bool dead = false;
    public double o1;
    public double o2;
    public static float variable1 = 1;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
    }
    void FixedUpdate()
    {
        if(!collided)
        { 
            for (int i = 0; i < 7; i++)
            {
                Vector3 newVector = Quaternion.AngleAxis(i * 30, new Vector3(0, 0, 1)) * transform.right;
                RaycastHit hit;
                Ray Ray = new Ray(transform.position, newVector);

                if (Physics.Raycast(Ray, out hit, 1000, mask1))
                {                   
                    input[i] = (1000 - hit.distance) / 1000;
                    Debug.DrawRay(Ray.origin, hit.point, Color.red);
                }
                else
                {
                    input[i] = 0;                   
                }
            }   
            for (int i = 7; i < 14; i++)
            {
                Vector3 newVector = Quaternion.AngleAxis((i - 7) * 30, new Vector3(0, 0, 1)) * transform.right;
                RaycastHit hit;
                Ray Ray = new Ray(transform.position, newVector);

                if (Physics.Raycast(Ray, out hit, 1000, mask2))
                {
                    input[i] = (1000 - hit.distance) / 1000;
                    Debug.DrawRay(Ray.origin, hit.point, Color.red);
                }
                else
                {
                    input[i] = 0;
                }                
            }

            d = Vector3.Distance(gameObject.transform.position, GameObject.Find("forest").transform.position);
            double[] output = nn.Feedforward(input);
            
            float variable1 = (float)output[0];
            float variable2 = (float)output[1];
            o1 = output[0];
            o2 = output[1];

            transform.Rotate(Vector3.forward * (variable1-0.5f) * 200f*Time.deltaTime);
            rb.AddRelativeForce(Vector3.up *variable2* 3000f * Time.deltaTime);
            fitness = Map.mapping(d, 0, Screen.height, 1, 0);
        }
    }
        


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Target")
        {
            fitness *= 10;
            collided = true;
            //Object.Destroy(gameObject);
        }
        else if (collision.collider.gameObject.tag == "Obstacle")
        {
            dead = true;
            collided = true;
            Object.Destroy(gameObject);
            fitness *=0.9;
        }
    }

}
