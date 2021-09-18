using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBorder : MonoBehaviour
{
    int roidCount = 500;
    int radius = 2005;
    public GameObject asteroid;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRoidBarrier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnRoidBarrier()
    {
        for (int i = 0; i < roidCount; i++)
        {
            float theta = i * 2 * Mathf.PI / roidCount;
            float x = Mathf.Sin(theta) * radius;
            float z = Mathf.Cos(theta) * radius;
            float directionFacing = Random.Range(0f, 360f);
            Vector3 point = new Vector3(x, 0, z);

            GameObject Roids = Instantiate(asteroid, point, Quaternion.Euler(new Vector3(0f, directionFacing, 0f)));
            Roids.transform.parent = transform;
        }
    }
}
