using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaShooterController : MonoBehaviour
{
    //references
    public float MoveSpeed = 30;
    public GameObject Ball;
    public Transform Arms;
    public Transform PosOverHead;
    public Transform posDribble;
    public Transform Target;
    public AudioSource source;

    public AudioClip sound_of_boba;

    //variables
    public bool IsBallInHands = false;
    private bool IsBallFlying = false;
    private float T = 0;

    void Start()
    {
        
    }

    void Update()
    {

        //Ball in hands
        if (IsBallInHands == true)
        {
            //Hold over head
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject.FindGameObjectWithTag("BobaPearls").transform.position = PosOverHead.position;
               //Arms.localEulerAngles = Vector3.right * 180;

               transform.LookAt(Target.position);
            }
            //dribbling
            else
            {
                GameObject.FindGameObjectWithTag("BobaPearls").transform.position = posDribble.position;
                //Arms.localEulerAngles = Vector3.right * 1;
            } 
            //throw ball
            if (Input.GetKeyUp(KeyCode.Space))
            {
                IsBallInHands = false;
                IsBallFlying = true;
                T = 0;
                source.PlayOneShot(sound_of_boba);
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
            GameObject.FindGameObjectWithTag("BobaPearls").transform.position = pos + arc;
            

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
