using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LaserGunScript : MonoBehaviour
{
    [Header("Reference")]
    public CinemachineVirtualCamera vcam;
    public GameObject laserBeam_obj;
    public GameObject miningLaserBeam_obj;
    public Transform gunPoint_transform;

    [Header("Value Data")]
    public float gunRefreshTime = 0.5f;
    public float miningGunRefreshTime = 0.5f;

    Vector3 mousePos;
    float gunRefresh = 0;
    float miningGunRefresh = 0;

    private void Update()
    {
        GunRefresh();

        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;
        if (playerPlane.Raycast(ray, out hitDist))
        {
            mousePos = ray.GetPoint(hitDist);
        }
        Quaternion targetRotation = Quaternion.LookRotation(mousePos - transform.position);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = targetRotation;

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

    //private void FixedUpdate()
    //{
    //    Vector3 _adjustedMousePos = new Vector3(mousePos.x, 0, mousePos.z);
    //    Vector3 lookDir = _adjustedMousePos - transform.position;
    //    float _angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;
    //    Quaternion quaternion = Quaternion.EulerAngles(transform.rotation.x, _angle, transform.rotation.z);
    //    transform.rotation = quaternion;
    //}

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
