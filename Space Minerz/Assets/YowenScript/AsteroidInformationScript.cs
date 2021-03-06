using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidInformationScript : MonoBehaviour
{
    public AsteroidSpawnerManager spawnManager = null;
    public int asteroidIndex = 0;
    public int asteroidHealth = 100;

    private void Update()
    {
        if(asteroidHealth <= 0)
        {
            if(spawnManager != null)
            {
                spawnManager.asteroidHolder.Remove(this.gameObject);
            }

            PlayerProfile.resourcesTotal[asteroidIndex] += 5;

            Destroy(this.gameObject);
        }
    }
}
