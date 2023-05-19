using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    public GameObject previousRoad;
    public GameObject parentRoadObject;
    public GameObject terrain;
    [SerializeField] bool reBuild;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("aef");
        //RoadGeneration();
    }


    public void RoadGeneration()
    {
        for (int i = 0; i < 5000; i++)
        {
            GameObject currentRoad = Instantiate(previousRoad, previousRoad.transform.position, GetRoadRotation(previousRoad.transform.rotation));
            currentRoad.name = "Road";
            currentRoad.transform.parent = this.transform;
            currentRoad.transform.Translate(Vector3.forward * 10);

            if (!LocalConstraints(currentRoad))
            {
                Destroy(currentRoad);
                Debug.Log("aefaef");
                continue;
            }

            previousRoad = currentRoad;

        }
    }



    public Quaternion GetRoadRotation(Quaternion rotation)
    {
        Quaternion resultRotation = rotation;

        int num = Random.Range(0, 10);
        if (num == 0)
        {
            resultRotation.eulerAngles += new Vector3(0, 90, 0);
        } else if (num == 1)
        {
            resultRotation.eulerAngles += new Vector3(0, -90, 0);
        }

        return resultRotation;
    }

    public void resetRoadGeneration()
    {
        foreach (Transform road in transform)
        {
            Destroy(road.gameObject);
        }

        previousRoad.transform.position = new Vector3(0, 1, 0);
        RoadGeneration();
    }

    // Update is called once per frame
    void Update()
    {
        if (reBuild)
        {
            reBuild = false;
            resetRoadGeneration();

        }
    }



    public bool LocalConstraints(GameObject road)
    {
        Vector3 boundary = terrain.transform.TransformPoint(Vector3.Scale((terrain.transform.localScale / 2) / 10, new Vector3(1, 1, -1)));
        Vector3 boundary2 = terrain.transform.TransformPoint(Vector3.Scale((terrain.transform.localScale / 2) / 10, new Vector3(-1, -1, 1)));

        if (road.transform.position.x > boundary.x || road.transform.position.z < boundary.z)
        {
            return false;
        } else if (road.transform.position.x < boundary2.x || road.transform.position.z > boundary2.z) {
            return false;
        }

        return true;
    }
}
