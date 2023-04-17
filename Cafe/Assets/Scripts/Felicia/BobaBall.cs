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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
