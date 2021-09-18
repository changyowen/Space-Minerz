using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunScript : MonoBehaviour
{
    [Header("Reference")]
    public GameObject laserBeam_obj;
    public GameObject miningLaserBeam_obj;
    public Transform gunPoint_transform;

    [Header("Value Data")]
    public float gunRefreshTime = 0.5f;
    public float miningGunRefreshTime = 0.5f;

    float gunRefresh = 0;
    float miningGunRefresh = 0;

    private void Update()
    {
        GunRefresh();

        if(Input.GetMouseButton(0))
        {
            if(gunRefresh <= 0)
                ShootRay();
        }
        else if(Input.GetMouseButton(1))
        {
            if (miningGunRefresh <= 0)
                ShootMiningRay();
        }
    }

    void GunRefresh()
    {
        if (gunRefresh > 0)
        {
            gunRefresh -= Time.deltaTime;
        }
        if(miningGunRefresh > 0)
        {
            miningGunRefresh -= Time.deltaTime;
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

    void ShootMiningRay()
    {
        GameObject newMiningLaserBeam = Instantiate(miningLaserBeam_obj, gunPoint_transform.position, transform.rotation) as GameObject;
        ShotBehavior shotBehavior = newMiningLaserBeam.GetComponent<ShotBehavior>();
        MainMovement mainMovement = GetComponent<MainMovement>();
        if (shotBehavior != null && mainMovement != null)
        {
            shotBehavior.speed = mainMovement.playerSpeed * 4;
        }

        miningGunRefresh += miningGunRefreshTime;
    }
}
