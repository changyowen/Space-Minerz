using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolAI : MonoBehaviour
{
    [Header("Reference")]
    NavMeshAgent agent;
    public Transform gunPoint_transform;
    public GameObject laserBeam_obj;

    [Header("Data Value")]
    public float shootRange = 25f;
    public float stopRange = 10f;
    public float gunRefreshTime = 0.8f;
    public float destRefreshTime = 8f;

    bool direction = true;
    bool isAlert = false;
    Transform playerTransform = null;
    float gunRefresh = 0;

    int patrolCount = 0;

    public Transform path_transform;
    Vector3 target;
    float destRefresh = 0;
    Vector3[] PatrolPoint = new Vector3[5] {new Vector3(0,0,-100), new Vector3(30, 0, -50), new Vector3(-60, 0, 0), new Vector3(30, 0, 50), new Vector3(0, 0, 100) };
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = path_transform.GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        GunRefresh();

        if (isAlert) //ALERT
        {
            if (playerTransform != null) //check if get player Transform
            {
                float _dist1 = Vector3.Distance(path_transform.GetChild(0).position, playerTransform.position);
                float _dist2 = Vector3.Distance(path_transform.GetChild(3).position, playerTransform.position);
                if(_dist1 > 75f && _dist2 > 75f)
                {
                    isAlert = false;
                    LocalPatrol();
                }
                else
                {
                    TargetingPlayer();
                }
            }
        }
        else //NOT ALERT
        {
            LocalPatrol();
        }
    }

    void LocalPatrol()
    {
        agent.isStopped = false;
        float dist = Vector3.Distance(transform.position, target);

        destRefresh += Time.deltaTime;
        if (dist < 2 || destRefresh >= destRefreshTime)
        {
            if(direction)
            {
                if (patrolCount < 3)
                {
                    patrolCount++;
                }
                else if (patrolCount == 3)
                {
                    direction = false;
                    patrolCount--;
                }
            }
            else
            {
                if (patrolCount > 0)
                {
                    patrolCount--;
                }
                else if (patrolCount == 0)
                {
                    direction = true;
                    patrolCount++;
                }
            }
            destRefresh = 0;
        }

        target = path_transform.GetChild(patrolCount).position;
        agent.destination = target;
    }


    void TargetingPlayer()
    {
        //calculate distance
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= shootRange) //if in shoot range
        {
            //start shooting
            Shooting();

            if (dist > stopRange) //if havent reach stop range
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
        if (trigger)
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
