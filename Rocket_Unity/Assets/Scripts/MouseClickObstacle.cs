using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickObstacle : MonoBehaviour
{
    public GameObject enemy;

    public void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.name == "Background")
                    Debug.Log(hit.point.z-5);
                    Vector3 position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 5);
                    Instantiate(enemy, position, Quaternion.identity);
            }
        }
    }
}