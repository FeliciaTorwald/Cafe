using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickupable : MonoBehaviour
{
    public abstract void Interact();
    public ToolType toolType;

    public ToolType IdentifyToolType()
    {
        return toolType;
    }
}
