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
                {
                    Vector3 position = new Vector3(hit.point.x, hit.point.y, 0);
                    Instantiate(enemy, position, Quaternion.identity);
                }
            }
        }
    }
}