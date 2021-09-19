using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHolderManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject[] asteroidHolder;

    private void Update()
    {
        float _dist = Vector3.Distance(playerTransform.position, Vector3.zero);

        if(_dist < 150f )
        {
            asteroidHolder[0].SetActive(true);
            asteroidHolder[1].SetActive(false);
            asteroidHolder[2].SetActive(false);
        }
        else if(_dist >= 150f && _dist < 400f)
        {
            asteroidHolder[0].SetActive(true);
            asteroidHolder[1].SetActive(true);
            asteroidHolder[2].SetActive(false);
        }
        else if (_dist >= 400f && _dist < 750f)
        {
            asteroidHolder[0].SetActive(false);
            asteroidHolder[1].SetActive(true);
            asteroidHolder[2].SetActive(true);
        }
        else if (_dist >= 750f)
        {
            asteroidHolder[0].SetActive(false);
            asteroidHolder[1].SetActive(false);
            asteroidHolder[2].SetActive(true);
        }
    }
}
