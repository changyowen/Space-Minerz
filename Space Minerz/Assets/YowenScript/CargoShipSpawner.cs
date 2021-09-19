using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CargoShipSpawner : MonoBehaviour
{
    [Header("Reference")]
    public GameObject cargoShip_obj;
    public GameObject escortShip_obj;
    public GameObject pathCargoShip_obj;
    public Transform enemyHolder_transform;
    //public Transform spawnPosition_temp;
    //public Transform destPosition_temp;

    [Header("Data Value")]
    public float cargoSpawnTime_minute = 3f; 

    float spawnerTimer = 2f * 60f;

    public List<GameObject> enemyHolder = new List<GameObject>();

    private void Update()
    {
        spawnerTimer += Time.deltaTime;
        if(spawnerTimer >= (cargoSpawnTime_minute * 60))
        {
            SpawnCargoShip();
            spawnerTimer = 0;
        }
    }

    //public void SpawnCargoShip()
    //{
    //    GameObject newCargoShip = Instantiate(cargoShip_obj, spawnPosition_temp.position, Quaternion.identity) as GameObject;
    //    CargoShipScript cargoShipScript = newCargoShip.GetComponent<CargoShipScript>();
    //    NavMeshAgent cargoShipAgent = newCargoShip.GetComponent<NavMeshAgent>();
    //    //set destination
    //    cargoShipAgent.destination = destPosition_temp.position;
    //    //spawn escort
    //    SpawnEscortShip(newCargoShip.transform , cargoShipScript.formation_transform);
    //}

    void SpawnCargoShip()
    {
        ///GET SPAWN LOCATION
        Vector3 _spawnCor = RandomPointOnCircleEdge(750);

        ///SPAWN Cargo
        //get rotation 
        Quaternion targetRotation = Quaternion.LookRotation(_spawnCor - Vector3.zero);
        GameObject newEnemyPath = Instantiate(pathCargoShip_obj, _spawnCor, targetRotation) as GameObject;
        newEnemyPath.transform.SetParent(enemyHolder_transform, false);
        GameObject newCargo = Instantiate(cargoShip_obj, newEnemyPath.transform.GetChild(0).position, Quaternion.identity) as GameObject;
        newCargo.transform.SetParent(enemyHolder_transform, false);
        enemyHolder.Add(newCargo);

        ///ASSIGN DATA
        EnemyInformationScript enemyScript = newCargo.GetComponent<EnemyInformationScript>();
        CargoShipScript enemyAI = newCargo.GetComponent<CargoShipScript>();
        if (enemyScript != null)
        {
            enemyScript.cargoSpawnManager = this;
            enemyAI.targetPos = newEnemyPath.transform.GetChild(1).position;
        }

        //spawn escort
        SpawnEscortShip(newCargo.transform, enemyAI.formation_transform);
    }

    private Vector3 RandomPointOnCircleEdge(float radius)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, 0, vector2.y);
    }

    void SpawnEscortShip(Transform _cargoShipTransform, Transform formation_transform)
    {
        //get all children
        List<Transform> childrenList = new List<Transform>();
        for (int i = 0; i < formation_transform.childCount; i++)
        {
            childrenList.Add(formation_transform.GetChild(i));
        }

        for (int i = 0; i < childrenList.Count; i++)
        {
            GameObject newEscortShip = Instantiate(escortShip_obj, childrenList[i].position, Quaternion.identity) as GameObject;
            newEscortShip.transform.SetParent(enemyHolder_transform, false);
            enemyHolder.Add(newEscortShip);
            EscortShipScript escortShipScript = newEscortShip.GetComponent<EscortShipScript>();
            escortShipScript.cargoShipTransform = _cargoShipTransform;
            escortShipScript.formationPos_transform = childrenList[i];
        }
    }
}
