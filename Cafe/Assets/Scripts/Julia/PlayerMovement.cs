using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed = 5;
    public float groundDrag;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    GameObject newpos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //set how much the player movement can slide on the ground
        rb.drag = 7;
    }

    void Update()
    {
        MyInput();
        speedControl(); 

        if (Input.GetKeyDown(KeyCode.W))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.D))
        {

            
        }      
    }
    private void FixedUpdate() 
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void speedControl()
    {
        Vector2 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized *  moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
