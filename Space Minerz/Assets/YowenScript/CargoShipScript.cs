using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CargoShipScript : MonoBehaviour
{
    NavMeshAgent agent;

    [Header("Reference")]
    public Transform formation_transform;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
