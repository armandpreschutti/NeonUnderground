using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionHandler : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;

    public Vector3 moveDirection;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        rb.freezeRotation = true;
    }

    private void Update()
    {
        DetectInput();     
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void DetectInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //Calcutlate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        //rb.velocity = moveDirection.normalized * speed ;
    }
}
