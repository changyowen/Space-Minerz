using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public int spawnRadius = 2000; // not sure how large this is yet..
    public GameObject Asteroid; // added in Unity GUI
    public int RoidCount;
    Vector3 originPoint;

    // Start is called before the first frame update
    void Start()
    {
        originPoint = Vector3.zero;
        RoidCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(RoidCount < 400)
        {
            SpawnRoid();
        }
    }



    public void SpawnRoid()
    {
        float directionFacing = Random.Range(0f, 360f);

        // need to pick a random position around originPoint but inside spawnRadius
        // must not be too close to another agent inside spawnRadius
        Vector3 point = (Random.insideUnitSphere * spawnRadius) + originPoint;
        if(Vector3.Distance (point, Vector3.zero) > 400)
        {
            point.y = 0f;
            GameObject Roids = Instantiate(Asteroid, point, Quaternion.Euler(new Vector3(0f, directionFacing, 0f)));
            Roids.transform.parent = transform;
            RoidCount++;
        }
        else
        {
            point = (Random.insideUnitSphere * spawnRadius) + originPoint;
        }

    }
}
