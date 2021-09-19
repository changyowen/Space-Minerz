using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Reference")]
    public GameObject[] enemy_objList;
    public Transform enemyHolder_transform;

    [Header("Data Value")]
    public int regionIndex = 1;
    public Vector3 originPoint = Vector3.zero;
    public float enemyRegenerationTime = 60f;
    public int maximumEnemy = 10;

    public float[] regionArea = new float[4] { 15, 200, 550, 1000 };

    float enemyRefresh = 0;
    public List<GameObject> enemyHolder = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maximumEnemy; i++)
        {
            SpawnEnemy();
        }
        enemyRefresh = 0;
    }

    private void Update()
    {
        if (enemyRefresh > 0)
        {
            enemyRefresh -= Time.deltaTime;
        }

        if (enemyHolder.Count < maximumEnemy) //if havent reach max asteroid total
        {
            if (enemyRefresh <= 0)
                SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        ///GET SPAWN LOCATION
        Vector3 _spawnCor = GetSpawnLocation();

        ///SPAWN ASTEROID
        GameObject newEnemy = Instantiate(enemy_objList[regionIndex - 1], _spawnCor, Quaternion.identity) as GameObject;
        newEnemy.transform.SetParent(enemyHolder_transform, false);
        enemyHolder.Add(newEnemy);

        ///ASSIGN DATA
        

        ///ADD REFRESH TIME
        enemyRefresh += enemyRegenerationTime;
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
}
