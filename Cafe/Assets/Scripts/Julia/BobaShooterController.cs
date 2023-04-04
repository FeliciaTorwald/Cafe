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
    private float T = 0;

    //variables
    private bool InBallHands = true;
    private bool IsBallFlying = false;

    void Start()
    {
        
    }

    void Update()
    {
        //walkin
        Debug.Log("1");
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Debug.Log("2");
        transform.position += direction * MoveSpeed * Time.deltaTime;
        Debug.Log("3");
        transform.LookAt(transform.position + direction);
        
        //Ball in hands
        if (InBallHands)
        {
        
        //Hold over head
        if (Input.GetKey(KeyCode.Space))
        {
            Ball.position = PosOverHead.position;
            Arms.localEulerAngles = Vector3.right * 180;

            transform.LookAt(Target.position);
        }
        //dribbling
        else
        {
            Ball.position = posDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
            Arms.localEulerAngles = Vector3.right * 0;
        }
        }
        //throw ball
        if (Input.GetKeyUp(KeyCode.Space))
        {
            InBallHands = false;
            IsBallFlying = true;
            T = 0;
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
}
