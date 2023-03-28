using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipNet : MonoBehaviour
{
    public GameObject net;
    public Transform netParent;
    void Start()
    {
        net.GetComponent<Rigidbody>().isKinematic = true;
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
        netParent.DetachChildren();
        net.transform.eulerAngles = new Vector3(net.transform.position.x, net.transform.position.z, net.transform.position.y);
        net.GetComponent<Rigidbody>().isKinematic = false;
        net.GetComponent<CapsuleCollider>().enabled = true;
    }

    void Equip()
    {
        net.GetComponent<Rigidbody>().isKinematic = true;

        net.transform.position = netParent.transform.position;
        net.transform.rotation = netParent.transform.rotation;

        net.GetComponent<CapsuleCollider>().enabled = false;

        net.transform.SetParent(netParent);
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
