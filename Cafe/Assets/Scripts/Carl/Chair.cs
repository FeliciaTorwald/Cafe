using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, ISeat
{
    void Start()
    {
        Invoke(nameof(AddSelf), .1f);
    }
    
    void Update()
    {
        
    }

    public void AddSelf()
    {
        GameManager.Instance.seatsInScene.Add(this);
        GameManager.Instance.freeSeats++;
    }
    
    public Chair GetGameObject()
    {
        return this;
    }
}
