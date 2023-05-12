using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;
    private Vector3 relataiveVerticalMovement;
    private Vector3 relativeHorizontalMovement;
    private Vector3 relativeForward;
    private Vector3 relativeHorizontal;

    private Rigidbody rgbd;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 600f;
    [SerializeField] private float acceleraion = 10f;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Vector3 brakeStrength = new Vector3(1, 0, 1);


    public ParticleSystem dust;

    public Animator animator;

    private void Start()
    {
        SetRelativeVector();
        rgbd = GetComponent<Rigidbody>();
        dust.Pause();
    }

    private void Update()
    {
        GetAndAlignInput();
        UpdateMovement();
        Braking();
    }

    private void Braking()
    {
        // if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
        // {
        //     rgbd.velocity = Vector3.zero;
        //
        // }
    }

    private void SetRelativeVector()
    {
        relativeForward = Camera.main.transform.forward;
        relativeHorizontal = Camera.main.transform.right;
        relativeForward.y = 0f;
        relativeHorizontal.y = 0f;
        relativeForward = relativeForward.normalized;
        relativeHorizontal = relativeHorizontal.normalized;
    }
    
    private void GetAndAlignInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        relataiveVerticalMovement = verticalInput * relativeForward;
        relativeHorizontalMovement = horizontalInput * relativeHorizontal;
    }

    private void UpdateMovement()
    {
        Vector3 movement = relataiveVerticalMovement + relativeHorizontalMovement;
        movement *= (acceleraion * speed * Time.deltaTime);
        movement = Vector3.ClampMagnitude(movement, maxSpeed);
        
        // transform.Translate(movement * (speed * Time. deltaTime), Space.World);
        rgbd.AddForce(movement, ForceMode.VelocityChange);
        // rgbd.velocity = Vector3.ClampMagnitude(rgbd.velocity, maxSpeed);
        
        if(movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,rotationSpeed * Time.deltaTime);
            dust.Play();
            //animator.SetTrigger("Walking");
        }
        if(movement== Vector3.zero)
        {
            //animator.SetTrigger("Idle");
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
           // animator.SetTrigger("Walking");
        }
        else
        {
            //animator.SetTrigger("Idle");
        }

        if(Mathf.Abs(verticalInput ) > 0.01f || Mathf.Abs(horizontalInput) > 0.01f)
        {
            animator.SetBool("WalkingBool", true);
        }
        else
        {
            animator.SetBool("WalkingBool", false);
        }
    }
}
