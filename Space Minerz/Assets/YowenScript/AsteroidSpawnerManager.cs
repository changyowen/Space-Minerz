using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawnerManager : MonoBehaviour
{
    [Header("Reference")]
    public GameObject[] asteroid_objList;
    public Transform asteroidHolder_transform;

    [Header("Data Value")]
    public int regionIndex = 1;
    public Vector3 originPoint = Vector3.zero;
    public float asteroidRegenerationTime = 30f;
    public int maximumAsteroid = 1000;
    public float[] probability;

    public float[] regionArea = new float[4] { 15, 200, 550, 1000 };

    float asteroidRefresh = 0;
    public List<GameObject> asteroidHolder = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maximumAsteroid; i++)
        {
            SpawnAsteroid();
        }
        asteroidRefresh = 0;
    }

    private void Update()
    {
        if(asteroidRefresh > 0)
        {
            asteroidRefresh -= Time.deltaTime;
            Debug.Log(asteroidRefresh);
        }

        if (asteroidHolder.Count < maximumAsteroid) //if havent reach max asteroid total
        {
            if(asteroidRefresh <= 0)
            {
                SpawnAsteroid();
            }
        }
    }

    void SpawnAsteroid()
    {
        ///GET SPAWN LOCATION
        Vector3 _spawnCor = GetSpawnLocation();

        ///ROLL ASTEROID TYPE
        int getIndex = RollingProbability();

        ///SPAWN ASTEROID
        GameObject newAsteroid = Instantiate(asteroid_objList[getIndex], _spawnCor, Quaternion.identity) as GameObject;
        newAsteroid.transform.SetParent(asteroidHolder_transform, false);
        asteroidHolder.Add(newAsteroid);

        ///ASSIGN DATA
        AsteroidInformationScript informationScript = newAsteroid.GetComponent<AsteroidInformationScript>();
        if(informationScript != null)
        {
            informationScript.spawnManager = this;
        }

        ///ADD REFRESH TIME
        asteroidRefresh += asteroidRegenerationTime;
    }

    Vector3 GetSpawnLocation()
    {
        Vector3 spawnLocation = Vector3.zero;
        switch (regionIndex)
        {
            case 1:
                {
                    float _dist = 0;
                    while (spawnLocation == Vector3.zero || _dist <= regionArea[0])
                    {
                        Vector2 _randomPoint = (Random.insideUnitCircle * regionArea[1]) + Vector2.zero;
                        spawnLocation = new Vector3(_randomPoint.x, 0, _randomPoint.y);
                        _dist = Vector3.Distance(spawnLocation, originPoint);
                    }
                    break;
                }
            case 2:
                {
                    float _dist = 0;
                    while (spawnLocation == Vector3.zero || _dist <= regionArea[1])
                    {
                        Vector2 _randomPoint = (Random.insideUnitCircle * regionArea[2]) + Vector2.zero;
                        spawnLocation = new Vector3(_randomPoint.x, 0, _randomPoint.y);
                        _dist = Vector3.Distance(spawnLocation, originPoint);
                    }
                    break;
                }
            case 3:
                {
                    float _dist = 0;
                    while (spawnLocation == Vector3.zero || _dist <= regionArea[2])
                    {
                        Vector2 _randomPoint = (Random.insideUnitCircle * regionArea[3]) + Vector2.zero;
                        spawnLocation = new Vector3(_randomPoint.x, 0, _randomPoint.y);
                        _dist = Vector3.Distance(spawnLocation, originPoint);
                    }
                    break;
                }
        }
        spawnLocation = new Vector3(spawnLocation.x, 0, spawnLocation.z);
        return spawnLocation;
    }

    int RollingProbability()
    {
        //get total rate
        float _totalRate = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            _totalRate += probability[i];
        }
        //get random weight
        float randomWeight = Random.Range(0, _totalRate);
        //loop all type of probability and return one
        for (int i = 0; i < probability.Length; i++)
        {
            if (randomWeight < probability[i])
            {
                return i;
            }
            else
            {
                randomWeight -= probability[i];
            }
        }
        return 0;
    }
}
