using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderEnemyAI : MonoBehaviour
{
    [Header("Reference")]
    NavMeshAgent agent;
    public Transform gunPoint_transform;
    public GameObject laserBeam_obj;

    [Header("Data Value")]
    public float shootRange = 20f;
    public float stopRange = 10f;
    public float gunRefreshTime = 0.5f;

    bool isMove = false;
    bool isAlert = false;
    Transform playerTransform = null;
    float gunRefresh = 0;

    Vector3 StartPosition;
    Vector3 targetPos;
    Vector3 target;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
        targetPos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
        target = transform.position + targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        GunRefresh();

        if (isAlert) //ALERT
        {
            if (playerTransform != null) //check if get player Transform
            {
                TargetingPlayer();
            }
        }
        else //NOT ALERT
        {
            randMove();
        }
    }

    void randMove()
    {
        float dist = Vector3.Distance(transform.position, target);
        if (isMove)
        {
            if(dist < .2)
            {
                isMove = false;
            }
        }
        else
        {
            targetPos = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            target = StartPosition + targetPos;
            agent.destination = target;
            isMove = true;
        }

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
