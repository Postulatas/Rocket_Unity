using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickObstacle : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();
    public GameObject ast_1;
    public GameObject ast_2;
    public GameObject ast_3;

    public void Start()
    {
        prefabList.Add(ast_1);
        prefabList.Add(ast_2);
        prefabList.Add(ast_3);
        ast_1.layer = 9;
        ast_2.layer = 9;
        ast_3.layer = 9;
    }

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
                    int prefabIndex = UnityEngine.Random.Range(0,3);
                    Instantiate(prefabList[prefabIndex], position, Quaternion.identity);
                    
                }
            }
        }
    }
}