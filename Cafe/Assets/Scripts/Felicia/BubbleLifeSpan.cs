using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLifeSpan : MonoBehaviour
{
    public float lifespan = 5;
    private void Start()
    {
        Invoke(nameof(Destroy), lifespan);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
