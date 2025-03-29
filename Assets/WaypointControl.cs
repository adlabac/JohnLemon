using UnityEngine;
using UnityEngine.AI; 

public class WaypointControl : MonoBehaviour
{
    public Transform[] waypoints;

    NavMeshAgent navMeshAgent;
    int currentWaypoint = 0;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
}
