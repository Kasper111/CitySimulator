using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class VectorField : MonoBehaviour
{



    private int rowsPoints = 10;
    private ArrayList points = new ArrayList();
    public GameObject pointObject;
    public Material blackMaterial;

    public PerlinNoise perlinNoise;
    private ArrayList vectorPoints = new ArrayList();

    public int value = 360;
    

    //public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        perlinNoise = gameObject.AddComponent<PerlinNoise>();

        CreatePoints();
        //RenderVectorLines();
        DrawVectors();

        InvokeRepeating("UpdateVectors", 0, 2.0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePoints()
    {
        int spacing = 10;
        Camera cam = Camera.main;
        int height = (int) Mathf.Round(2 * cam.orthographicSize);
        int width = (int) Mathf.Round(height * cam.aspect);

        Vector3 positionOfPoint = new Vector3((width / 2) * -1, 1, (height / 2) * -1);


        while (positionOfPoint.x < width/2)
        {
            while (positionOfPoint.z < height/2)
            {


                int x = (int)Mathf.Round(positionOfPoint.x);
                int y = (int)Mathf.Round(positionOfPoint.z);

                float angle = perlinNoise.CalculateVectorAngle(x, y);

                //Debug.Log(angle);

                angle = angle * 720;

                //Debug.Log(angle);
                //Debug.Log(x + " " + y);

                Point point = new Point(x, y, angle);
                positionOfPoint.z += spacing;
                points.Add(point);
            }

            positionOfPoint.x += spacing;
            positionOfPoint.z = (height / 2) * -1;
        }
    }

    public void DrawVectors()
    {
        foreach (Point aPoint in points)
        {

            Vector3 position = new Vector3(aPoint.getX, 1, aPoint.getY);

            GameObject vectorPoint = Instantiate(pointObject, position, Quaternion.Euler(0, aPoint.getAngle, 0));

            vectorPoints.Add(vectorPoint);
/*
            float angle = perlinNoise.CalculateVectorAngle(aPoint.getX, aPoint.getY);

            angle = angle % 360 * 100;

            GameObject newPoint = Instantiate(pointObject, position, Quaternion.Euler(0, angle, 0));
            newPoint.name = "Point";
            newPoint.transform.parent = this.transform;*/
        }
    }

    public void UpdateVectors()
    {
        foreach (GameObject vectorPoint in vectorPoints)
        {
            //perlinNoise.offsetX = Random.Range(0f, 99999f);
            //perlinNoise.offsetY = Random.Range(0f, 99999f);

            float angle = perlinNoise.CalculateVectorAngle((int)vectorPoint.transform.position.x, (int)vectorPoint.transform.position.z);
            angle = angle * 720;
            //Debug.Log(angle);
            vectorPoint.transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        IncludeCircle();

    }


    public void IncludeCircle()
    {
        Vector3 circlePosition = new Vector3(0, 3, 0);
        int radius = 50;

        foreach (GameObject aVector in vectorPoints)
        {
            
            if (Vector3.Distance(aVector.transform.position, circlePosition) < radius)
            {
                var relativePos = aVector.transform.position - circlePosition;
                float angle;
                var forward = Vector3.forward;
                if (Vector3.Cross(forward, relativePos).y < 0)
                {
                    angle = Vector3.Angle(relativePos, forward) * -1;
                } else
                {
                    angle = Vector3.Angle(relativePos, forward);
                }

                aVector.transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
    }

    //https://rmarcus.info/blog/2018/03/04/perlin-noise.html



    public ArrayList getVectorPoints
    {
        get { return vectorPoints; }
    }


}