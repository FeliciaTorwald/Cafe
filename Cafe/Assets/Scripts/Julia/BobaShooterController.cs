using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
    public Transform PlayerRef;
    public float Distance;
    Vector3 startThrowPos;
    bool equipped = false;

    public AudioClip sound_of_boba;

    //variables
    public bool IsBallInHands = false;
    public bool IsBallFlying = false;
    private float T = 0;
    public bool inTriggerArea;

    EquipTool eQ;

    void Start()
    {
        eQ = FindFirstObjectByType<EquipTool>();
    }

    void Update()
    {

        //Ball in hands
        if (IsBallInHands == true)
        {
            //Hold over head
            if (Input.GetKeyDown(KeyCode.Space))
            {

                Ball.transform.position = PosOverHead.position;
                // Arms.localEulerAngles = Vector3.right * 180;
                transform.LookAt(Target.position);

            }
            //dribbling
            else if (Ball != null)
            {
                Ball.transform.position = posDribble.position;
                //Arms.localEulerAngles = Vector3.right * 1;
            }

            //throw ball
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Ball.GetComponent<Rigidbody>().isKinematic = true;
                //Ball.GetComponent<Rigidbody>().AddForce(new Vector3(1,1,1)*10, ForceMode.Impulse);// use this to shoot customers later
                IsBallInHands = false;
                IsBallFlying = true;
                T = 0;
                source.PlayOneShot(sound_of_boba);
                eQ.equipped = false;
                EquipTool.slotIsFull = false;
                startThrowPos = PosOverHead.position;
            }

        }


        //ball in the air
        if (IsBallFlying)
        {
            T += Time.deltaTime;
            float duration = 1.5f;
            float t01 = T / duration;

            // move to target
            Vector3 A = startThrowPos;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);
            Ball.transform.position = pos + arc;


            if (t01 >= 2)
            {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }

    //private void OnTriggerStay(Collider other) 
    //{
    //    if (Input.GetKey(KeyCode.E))
    //        {
    //        if(other.gameObject.tag == "BobaPearls" && !IsBallInHands && !IsBallFlying)
    //            {
    //                IsBallInHands = true;
    //                Ball.GetComponent<Rigidbody>().isKinematic = true;
    //            }
    //        }
    //}

    public void PickingUpBall(GameObject bobaObject)
    {
        if (IsBallInHands == false && IsBallFlying == false)
        {
            Ball = bobaObject;
            IsBallInHands = true;
            Ball.GetComponent<Rigidbody>().isKinematic = false;
           EquipTool.slotIsFull = true;
        }

    }
}
