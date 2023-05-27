using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;       // Referenca ka objektu igrača
    public GameEnding gameEnding;  // Referenca ka skripti GameEnding

    bool playerInRange = false;    // Da li je igrač u blizini?

    // Igrač je u blizini
    private void OnTriggerEnter(Collider other) {
        if (other.transform == player)
        {
            playerInRange = true;
        }
    }

    // Igrač više nije u blizini
    private void OnTriggerExit(Collider other) {
        if (other.transform == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        // Ako je igrač u blizini provjeriti da li je vidljiv, tj. fa li ga neki objekat zaklanja
        if (playerInRange)
        {
            // Vektor pogleda od objekta PointOfView do tačke 1 m iznad koordinate igrača (pivot je u nivou poda)
            Vector3 direction = player.position - transform.position + Vector3.up;

            // Zrak od tačke pogleda (PointOfView) u pravcu vektora pogleda
            Ray ray = new Ray(transform.position, direction);

            RaycastHit hit;
            // Ako postoji objekat u koji zrak udara
            if (Physics.Raycast(ray, out hit))
            {
                // Ako je zrak udario u igrača
                if (hit.collider.transform == player)
                {
                    // Pozovi metodu da signaliziraš da je igrač uhvaćen
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
