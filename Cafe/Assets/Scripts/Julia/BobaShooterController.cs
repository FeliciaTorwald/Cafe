using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BobaShooterController : MonoBehaviour
{
    //references
    public float MoveSpeed = 30;
    public GameObject Ball;
    public List<GameObject> Balls;
    public Transform Arms;
    public Transform PosOverHead;
    public Transform posDribble;
    public Transform Target;
    public AudioSource source;
    public Transform PlayerRef;
    private Vector3 DistanceRef;
    float MinDist = 2;
    public float Distance;
    bool equipped = false;
    int maxBobaInHand = 1;

    public AudioClip sound_of_boba;

    //variables
    public bool IsBallInHands = false;
    private bool IsBallFlying = false;
    private float T = 0;
    public bool inTriggerArea;

    EquipTool eQ;
   
    void Start()
    {
        eQ = FindFirstObjectByType<EquipTool>();
        //GameObject.FindGameObjectWithTag("BobaPearls")
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
            //else if (Ball != null)
            //{
            //    Ball.transform.position = posDribble.position;// + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
            //    //Arms.localEulerAngles = Vector3.right * 1;
            //}
            //if (Balls.Count >= maxBobaInHand && IsBallInHands)
            //{
            //    return;
            //}



            //if (EquipTool.slotIsfull == false && eQ.equipped == false)
            else 
            {
                for (int i = 0; i < Balls.Count; i++)
                {
                    //EquipTool.slotIsfull = false;
                    float Distance = Vector3.Distance(Balls[i].transform.position, PlayerRef.position);
                    if (Distance < MinDist && inTriggerArea)
                    {

                        Balls[i].transform.position = posDribble.position;
                        Ball = Balls[i];

                        //if(Balls[i] >= maxBobaInHand)
                        //{
                        //    posDribble.DetachChildren();
                        //}

                        //EquipTool.slotIsfull = true;
                        //equipped = true;
                        //eQ.canYouEquipBoba = false;
                        //eQ.equipped = true;

                    }

                }
            }

            //throw ball
            if (Input.GetKeyUp(KeyCode.Space))
            {
                IsBallInHands = false;
                IsBallFlying = true;
                T = 0;
                source.PlayOneShot(sound_of_boba);
                eQ.canYouEquipBoba = true;
                equipped = false;
                Debug.Log(equipped);
                eQ.equipped = false;
                EquipTool.slotIsfull = false;
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
            Ball.transform.position = pos + arc;


            if (t01 >= 1.5)
            {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            PickingUpBall();
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

    public void PickingUpBall()
    {

        if (inTriggerArea && IsBallInHands == false && IsBallFlying == false)
        {
            IsBallInHands = true;
            Ball.GetComponent<Rigidbody>().isKinematic = true;
            //equipped = true;
            //eQ.equipped = true;
        }

    }

    public void OnTriggerEnter(Collider other)
    {

        if (!IsBallInHands && !IsBallFlying)
        {
            inTriggerArea = true;
            //IsBallInHands = true;
            //EquipTool.slotIsfull = true;
            //Ball.GetComponent<Rigidbody>().isKinematic = true;
            //eQ.equipped = false;
            //EquipTool.slotIsfull = true;
        }

        //if (other.gameObject.tag == "BobaPearls" && IsBallInHands && !IsBallFlying)
        //{

        //    EquipTool.slotIsfull = true;

        //}
    }

    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "BobaPearls" && !IsBallInHands && !IsBallFlying)
    //    {
    //        inTriggerArea = false;
    //        EquipTool.slotIsfull = false;

    //    }
    //    if (other.gameObject.tag == "BobaPearls" && IsBallInHands)
    //    {
    //        inTriggerArea = false;
    //        //EquipTool.slotIsfull = false;
    //    }
    //    if (other.gameObject.tag == "BobaPearls" && IsBallFlying)
    //    {
    //        inTriggerArea = false;
    //        EquipTool.slotIsfull = false;
    //    }
    //}
}

