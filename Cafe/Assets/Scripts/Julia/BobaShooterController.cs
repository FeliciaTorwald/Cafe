using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaShooterController : MonoBehaviour
{
    //references
    public float MoveSpeed = 30;
    public Transform Ball;
    public Transform Arms;
    public Transform PosOverHead;
    public Transform posDribble;
    public Transform Target;

    //variables
    public bool IsBallInHands = false;
    private bool IsBallFlying = false;
    private float T = 0;

    void Start()
    {
        
    }

    void Update()
    {
        //walkin
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + direction);
        
        //Ball in hands
        if (IsBallInHands == true)
        {
            //Hold over head
            if (Input.GetKey(KeyCode.Space))
            {
               Ball.position = PosOverHead.position;
               //Arms.localEulerAngles = Vector3.right * 180;

               transform.LookAt(Target.position);
            }
            //dribbling
            else
            {
                Ball.position = posDribble.position;
                //Arms.localEulerAngles = Vector3.right * 1;
            } 
            //throw ball
            if (Input.GetKeyUp(KeyCode.Space))
            {
                IsBallInHands = false;
                IsBallFlying = true;
                T = 0;
            }
        }


        //ball in the air
         if (IsBallFlying)
         {
            T += Time.deltaTime;
            float duration = 1.5f;
            float t01 = T / duration;

            // move to target
            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);
            Ball.position = pos + arc;

            if (t01 >= 1)
            {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }
         }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (Input.GetKey(KeyCode.E))
            {
            if(other.gameObject.tag == "BobaPearls" && !IsBallInHands && !IsBallFlying)
                {
                    IsBallInHands = true;
                    Ball.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
    }
}
