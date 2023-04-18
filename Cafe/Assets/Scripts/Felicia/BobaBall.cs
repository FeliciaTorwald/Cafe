using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaBall : Pickupable
{
    [SerializeField] private ToolType toolType;
    
    public override void Interact()
    {
        FindObjectOfType<BobaShooterController>().PickingUpBall(gameObject);
    }
    
    public ToolType IdentifyToolType()
    {
        return toolType;
    }
   
}
