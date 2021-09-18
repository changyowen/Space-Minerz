using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunScript : MonoBehaviour
{
    [Header("Reference")]
    public GameObject laserBeam_obj;
    public Transform gunPoint_transform;

    [Header("Value Data")]
    public float gunRefreshTime = 0.5f;

    float gunRefresh = 0;

    private void Update()
    {
        GunRefresh();

        if(Input.GetMouseButton(0))
        {
            if(gunRefresh <= 0)
                ShootRay();
        }
    }

    void GunRefresh()
    {
        if (gunRefresh > 0)
        {
            gunRefresh -= Time.deltaTime;
        }
    }

    void ShootRay()
    {
        GameObject newLaserBeam = Instantiate(laserBeam_obj, gunPoint_transform.position, transform.rotation) as GameObject;
        ShotBehavior shotBehavior = newLaserBeam.GetComponent<ShotBehavior>();
        MainMovement mainMovement = GetComponent<MainMovement>();
        if(shotBehavior != null && mainMovement != null)
        {
            shotBehavior.speed = mainMovement.playerSpeed * 4;
        }

        gunRefresh += gunRefreshTime;
    }
}
