using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISeat
{
    void AddSelf();
    
    Chair GetChairRef();

    public bool HasDirtyDish();
}
