using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaAnimation : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerAnimation()
    {
        animator.SetTrigger("Smol2Standard");
    }
}