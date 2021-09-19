using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CargoShipScript : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Reference")]
    public Transform formation_transform;
    public Vector3 targetPos = Vector3.zero;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.destination = targetPos;
    }
}
