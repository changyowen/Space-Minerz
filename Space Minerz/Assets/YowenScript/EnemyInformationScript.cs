using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInformationScript : MonoBehaviour
{
    public EnemySpawnManager spawnManager = null;
    public CargoShipSpawner cargoSpawnManager = null;
    public int enemyIndex = 0;
    public int enemyHealth = 100;

    private void Update()
    {
        if (enemyHealth <= 0)
        {
            if(enemyIndex == 2)
            {
                PatrolAI enemyAI = GetComponent<PatrolAI>();
                if (spawnManager != null)
                {
                    spawnManager.enemyHolder.Remove(this.gameObject);
                }
                if(enemyAI != null)
                {
                    Destroy(enemyAI.path_transform.gameObject);
                }
                Destroy(this.gameObject);
            }
            else if(enemyIndex == 3)
            {
                if (cargoSpawnManager != null)
                {
                    for (int i = cargoSpawnManager.enemyHolder.Count; i >= 1; i--)
                    {
                        Destroy(cargoSpawnManager.enemyHolder[i]);
                    }
                    cargoSpawnManager.enemyHolder.Clear();
                    Destroy(this.gameObject);
                }
            }
            else
            {
                if (spawnManager != null)
                {
                    spawnManager.enemyHolder.Remove(this.gameObject);
                }
                Destroy(this.gameObject);
            }
        }

        
        if (enemyIndex == 3)
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            if (agent.remainingDistance <= 5f)
            {
                if (cargoSpawnManager != null)
                {
                    for (int i = cargoSpawnManager.enemyHolder.Count - 1; i >= 1; i--)
                    {
                        Destroy(cargoSpawnManager.enemyHolder[i]);
                    }
                    cargoSpawnManager.enemyHolder.Clear();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
