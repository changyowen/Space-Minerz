using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EscortShipScript : MonoBehaviour
{
    [Header("Reference")]
    NavMeshAgent agent;
    public Transform gunPoint_transform;
    public GameObject laserBeam_obj;

    [Header("Data Value")]
    public float maxCargoShipDist = 150f;
    public float shootRange = 20f;
    public float stopRange = 10f;
    public float gunRefreshTime = 0.5f;

    bool isAlert = false;
    Transform playerTransform = null;
    [System.NonSerialized] public Transform cargoShipTransform = null;
    [System.NonSerialized] public Transform formationPos_transform;
    float gunRefresh = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        GunRefresh();

        if(isAlert) //ALERT
        {
            if(playerTransform != null && cargoShipTransform != null) //check if get player Transform
            {
                //calculate distance
                float dist = Vector3.Distance(transform.position, cargoShipTransform.position);

                if(dist <= maxCargoShipDist) //still in close with cargo ship
                {
                    TargetingPlayer();
                }
                else //too far from cargo ship
                {
                    //formation moving
                    if (formationPos_transform != null)
                    {
                        FormationMoving();
                    }
                }
            }
        }
        else //NOT ALERT
        {
            //formation moving
            if (formationPos_transform != null)
            {
                FormationMoving();
            }
        }
    }

    void TargetingPlayer()
    {
        //calculate distance
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if(dist <= shootRange) //if in shoot range
        {
            //start shooting
            Shooting();

            if(dist > stopRange) //if havent reach stop range
            {
                agent.isStopped = false;
                agent.destination = playerTransform.position;
            }
            else //reach stop range
            {
                agent.isStopped = true; 
            }
        }
        else //if our of shot range
        {
            agent.isStopped = false;
            agent.destination = playerTransform.position;
        }
    }

    void FormationMoving()
    {
        agent.isStopped = false;
        agent.destination = formationPos_transform.position;
    }

    void Shooting()
    {
        //rotation look toward player
        Vector3 targetPostition = new Vector3(playerTransform.position.x,
                                       transform.position.y,
                                       playerTransform.position.z);
        transform.LookAt(targetPostition);

        //if gun refreshed, shoot!
        if (gunRefresh <= 0)
            ShootRay();
    }

    void ShootRay()
    {
        GameObject newLaserBeam = Instantiate(laserBeam_obj, gunPoint_transform.position, transform.rotation) as GameObject;
        ShotBehavior shotBehavior = newLaserBeam.GetComponent<ShotBehavior>();
        if (shotBehavior != null)
        {
            shotBehavior.speed = agent.speed * 4;
        }

        gunRefresh += gunRefreshTime;
    }

    void GunRefresh()
    {
        if (gunRefresh > 0)
        {
            gunRefresh -= Time.deltaTime;
        }
    }

    public void AlertSystem(bool trigger)
    {
        if(trigger)
        {
            isAlert = true;
        }
        else
        {
            isAlert = false;
        }
    }

    public void RefreshPlayerTransform(Transform _playerTransform)
    {
        playerTransform = _playerTransform;
    }
}
