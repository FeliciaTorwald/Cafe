using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip_Kettle : MonoBehaviour
{
    public GameObject Kettle;
    public Transform KettleParent;
    void Start()
    {
        Kettle.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Drop();
        }
    }

    void Drop()
    {
        KettleParent.DetachChildren();
        Kettle.transform.eulerAngles = new Vector3(Kettle.transform.position.x, Kettle.transform.position.z, Kettle.transform.position.y);
        Kettle.GetComponent<Rigidbody>().isKinematic = false;
        Kettle.GetComponent<SphereCollider>().enabled = true;
    }

    void Equip()
    {
        Kettle.GetComponent<Rigidbody>().isKinematic = true;

        Kettle.transform.position = KettleParent.transform.position;
        Kettle.transform.rotation = KettleParent.transform.rotation;

        Kettle.GetComponent<SphereCollider>().enabled = false;

        Kettle.transform.SetParent(KettleParent);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Equip();
            }
        }
    }
}



