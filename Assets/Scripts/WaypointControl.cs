using UnityEngine;
using UnityEngine.AI;

public class WaypointControl : MonoBehaviour
{
    public Transform[] waypoints;  // Niz tačaka (waypointa) koje neprijatelj treba da prati

    NavMeshAgent navMeshAgent;  // Referenca ka NavMeshAgent komponenti
    int currentWaypoint = 0;  // Tačka prema kojoj se neprijatelj/nav mesh agent trenutno kreće

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();  // Preuzimanje reference ka NavMeshAgent komponenti
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);  // Kreni ka prvom waypointu
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)  // Da li je agent stigao dovoljno blizu krajnjoj tački?
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;  // Odredi indeks sljedeće tačke
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);  // Kreni ka sljedećoj tački
        }
    }
}
