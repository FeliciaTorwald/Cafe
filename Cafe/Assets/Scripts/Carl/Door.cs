using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Guest> queue;
    
    public Transform GetGameObject()
    {
        return transform;
    }
}
