using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaBall : Pickupable
{
    public override void Interact()
    {
        FindObjectOfType<BobaShooterController>().PickingUpBall(gameObject);
    }
}
