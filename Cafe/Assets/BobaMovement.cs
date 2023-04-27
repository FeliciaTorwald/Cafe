using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaMovement : MonoBehaviour
{

    public float speed = 2f; // Enemy movement speed
    public float minTime = 1f; // Minimum time before changing direction
    public float maxTime = 5f; // Maximum time before changing direction
    private Vector3 direction; // Current movement direction
    private float timer; // Timer for changing direction
    private void Start()
    {
        // Initialize direction and timer
        direction = Random.insideUnitSphere;
        timer = Random.Range(minTime, maxTime);
    }
    private void Update()
    {
        MoveDirection();
    }

    private void MoveDirection()
    {
        // Move the enemy in the current direction
        transform.Translate(direction * speed * Time.deltaTime);
        // Decrement the timer
        timer -= Time.deltaTime;
        // If the timer reaches zero, change direction
        if (timer <= 0f)
        {
            // Generate a new random direction
            direction = Random.insideUnitSphere;
            // Reset the timer
            timer = Random.Range(minTime, maxTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //MoveDirection();
    }

    //////////////////////
    /*public float movSpeed;
    public float rotSpeed = 25f;

    private bool isWandering = false;
    private bool isRotL = false;
    private bool isRotR = false;
    private bool isWalking = false;

    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotR == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotL == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (isWalking == true)
        {
            rb.transform.position += transform.forward * movSpeed;
        }
    }
    IEnumerator Wander()
    {
        int rottime = Random.Range(1, 4);
        int rotwait = Random.Range(1, 4);
        int rotatelorR = Random.Range(1, 2);
        int walkwait = Random.Range(1, 2);
        int walktime = Random.Range(1, 4);


        isWandering = true;

        yield return new WaitForSeconds(walkwait);
        isWalking = true;
        yield return new WaitForSeconds(walktime);
        isWalking = false;
        yield return new WaitForSeconds(rotwait);
        if (rotatelorR == 1)
        {
            isRotR = true;
            yield return new WaitForSeconds(rottime);
            isRotR = false;
        }
        if (rotatelorR == 2)
        {
            isRotL = true;
            yield return new WaitForSeconds(rottime);
            isRotL = false;
        }
        isWandering = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(Wander());
    }*/
    //////////////////////////////////////////////////7
    /*public float movementSpeed = 10f;
    public float rotationSpeed = 50f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isWandering == false)
        {
            StartCoroutine(Wander());
        }
        
        if (isRotatingRight== true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
        }

        if (isWalking == true)
        {
            rb.AddForce(transform.forward * movementSpeed);
        }
    }

    IEnumerator Wander()
    {
        int rotationTime = Random.Range(1, 3);
        int rotationWait = Random.Range(1, 3);
        int rotateDirection = Random.Range(1,2);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 3);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);

        isWalking = true;

        yield return new WaitForSeconds(walkTime);

        isWalking = false;

        yield return new WaitForSeconds(rotationWait);

        if ( rotateDirection == 1)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingLeft = false;
        }

        if (rotateDirection == 2)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotationTime);
            isRotatingRight = false;
        }

        isWandering = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.Rotate(0, 0, 180);
    }*/
    //////////////////////////////////////////////////////////7
}
