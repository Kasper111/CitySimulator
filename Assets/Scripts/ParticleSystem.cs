using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{

    private ArrayList vectorPoints;
    public GameObject particle;
    public int particleAmount;
    private ArrayList particles = new ArrayList();
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

        GameObject field = GameObject.Find("VectorField");
        VectorField vectorField = field.GetComponent<VectorField>();
        vectorPoints = vectorField.getVectorPoints;


        CreateParticles();
    }

    // Update is called once per frame
    void Update()
    {
        MoveParticle();
    }

    void CreateParticles()
    {

        for (int i = 0; i < particleAmount; i++)
        {

            Vector3 position = new Vector3(100, 3, 100);

            GameObject particleCopy = Instantiate(particle, position, Quaternion.identity);

            particles.Add(particleCopy);
        }
    }


    void MoveParticle()
    {

        foreach (GameObject particle in particles)
        {
            Rigidbody2D rigidbody = particle.GetComponent<Rigidbody2D>();

            Debug.Log("AEFAEF");

            float step = speed * Time.deltaTime;
            Transform target = GetClosestVectorPoint(particle.transform);
            //Vector3 target = new Vector3(-90, 0, -20);

            //particle.transform.position = Vector3.MoveTowards(particle.transform.position, target, step);
            //rigidbody.MoveRotation(target.rotation);
            Debug.Log(target.position);
            //rigidbody.AddForce(target.position);
        }

    }


    private Transform GetClosestVectorPoint(Transform particle)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = particle.position;
        foreach (GameObject potentialTarget in vectorPoints)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }
}
