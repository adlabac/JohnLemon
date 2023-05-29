using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointControl : MonoBehaviour
{
    public Transform[] waypoints;  // Niz tačaka duž kojih duh patrolira

    NavMeshAgent navMeshAgent;     // Referenca ka NavMeshAgent komponenti
    int currentWaypoint = 0;       // Indeks tačke ka kojoj se duh kreće

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();  //  Inicijalizacija polja navMeshAgent
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);  //  Zadati kretanje ka prvoj tački
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)  //  Da li je duh došao do zadate tačke?
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;  //  Pređi na sljedeću tačku, tj. na prvu ukoliko je na posljednjoj
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);  //  Zadati kretanje ka novoj tački
        }
    }
}
