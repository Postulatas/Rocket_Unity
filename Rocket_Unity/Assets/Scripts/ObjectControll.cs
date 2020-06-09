using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickObstacle : MonoBehaviour
{
    List<GameObject> prefabList = new List<GameObject>();
    public GameObject ast_1;
    public GameObject ast_2;
    public GameObject ast_3;
    public GameObject Forrest;

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
        AsteroidClick();
        MoveForrest();
    }

    private void MoveForrest()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 temp = Input.mousePosition;
            temp.z = -Camera.main.transform.position.z; // Set this to be the distance you want the object to be placed in front of the camera.
            Forrest.transform.position = Camera.main.ScreenToWorldPoint(temp);
        }
    }

    private void AsteroidClick()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.name == "Background")
                {
                    Vector3 position = new Vector3(hit.point.x, hit.point.y, 0);
                    int prefabIndex = UnityEngine.Random.Range(0, 3);
                    Instantiate(prefabList[prefabIndex], position, Quaternion.identity);

                }
            }
        }
    }
}