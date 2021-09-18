using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CargoShipSpawner : MonoBehaviour
{
    [Header("Reference")]
    public GameObject cargoShip_obj;
    public GameObject escortShip_obj;
    public Transform spawnPosition_temp;
    public Transform destPosition_temp;

    [Header("Data Value")]
    public float cargoSpawnTime_minute = 3f; 

    float spawnerTimer = 2f * 60f;

    private void Update()
    {
        spawnerTimer += Time.deltaTime;
        if(spawnerTimer >= (cargoSpawnTime_minute * 60))
        {
            SpawnCargoShip();
            spawnerTimer = 0;
        }
    }

    public void SpawnCargoShip()
    {
        GameObject newCargoShip = Instantiate(cargoShip_obj, spawnPosition_temp.position, Quaternion.identity) as GameObject;
        CargoShipScript cargoShipScript = newCargoShip.GetComponent<CargoShipScript>();
        NavMeshAgent cargoShipAgent = newCargoShip.GetComponent<NavMeshAgent>();
        //set destination
        cargoShipAgent.destination = destPosition_temp.position;
        //spawn escort

    }

    void SpawnEscortShip(Transform formation_transform)
    {

    }
}
