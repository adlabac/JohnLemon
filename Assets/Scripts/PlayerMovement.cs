using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;  // Brzina okretanja pri promjeni pravca. Polje turnSpeed je public, pa se može vidjeti i podešavati u Inspectoru.

    private Vector3 movement;      // Vektor pravca kretanja
    private Rigidbody rb;          // Rigidbody komponenta
    private Animator animator;     // Animator komponenta
    private AudioSource audioSource;

    private Quaternion rotation = Quaternion.identity;  // Rotacija glavnog lika


    // Kod koji se izrvršava na početku
    void Start()
    {
        //  Inicijalizacija polja rb i animator
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    // Kod koji se izvršava za svaki frejm igre (game loop)
    void FixedUpdate()
    {
        // Očitavanje ulaza kontrolera
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Postavljanje vrijednosti vektora smjera i njegova normalizacija
        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();

        // Da li lik hoda, ili stoji?
        bool isWalking = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f);

        // Postavljanje parametra animacije u zavisnosti od toga da li lik hoda, ili ne
        animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        // Izračunavanje ugla u koji treba okrenuti lika imajući u obzir: trenutni pravac, želejeni pravac i maksimalno dozvoljeni ugao zaokretanja
        Vector3 desiredForward =
            Vector3.RotateTowards(
                transform.forward,
                movement,
                turnSpeed * Time.deltaTime,
                0f);

        // Postavljanja kvaterniona rotacije tako da "gleda" prema željenom uglu
        rotation = Quaternion.LookRotation(desiredForward);
    }


    // Ovaj kod se takođe izvršava za svaki frejm, ali nakon što se animacije proračunaju.
    // Tako možemo obezbijediti kretanje u skladu sa root motionom, tj. brzinom definisanom unutar same animacije lika.
    void OnAnimatorMove() {
        // Pomjeranje lika u smjeru vektora movement za dužinu definisanu root motionom animatora, o odnosu na tekuću poziciju
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);

        // Okretanje lika na izračunati ugao
        rb.MoveRotation(rotation);
    }
}
