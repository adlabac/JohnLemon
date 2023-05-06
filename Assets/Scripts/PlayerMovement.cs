using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;

    private Vector3 movement;
    private Rigidbody rb;
    private Animator animator;

    private Quaternion rotation = Quaternion.identity;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0f, vertical);
        movement.Normalize();
        
        bool isWalking = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f);
        animator.SetBool("IsWalking", isWalking);
    }

    void OnAnimatorMove() {
        rb.MovePosition(rb.position + movement * animator.deltaPosition.magnitude);
        Vector3 desiredForward = 
            Vector3.RotateTowards(
                transform.forward, 
                movement, 
                turnSpeed * Time.deltaTime,
                0f);

        rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
    }
}
