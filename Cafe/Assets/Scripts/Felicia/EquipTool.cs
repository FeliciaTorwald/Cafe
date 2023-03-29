using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipTool : MonoBehaviour
{
    public GameObject tool;
    public Transform toolParent;
    public bool equipped;
    public bool slotIsfull;
    void Start()
    {
        tool.GetComponent<Rigidbody>().isKinematic = true;

        
    }
    //använd mesh collider för att detta scriptet ska fungera,sätt på convex och sen en boxcollider som är trigger

    // Update is called once per frame
    void Update()
    {
        if (equipped)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Drop();
            }
        }
    }

    void Drop()
    {
        toolParent.DetachChildren();
        tool.transform.eulerAngles = new Vector3(tool.transform.position.x, tool.transform.position.z, tool.transform.position.y);
        tool.GetComponent<Rigidbody>().isKinematic = false;
        tool.GetComponent<MeshCollider>().enabled = true;

        equipped = false;
        slotIsfull= false;
    }

    void Equip()
    {
        tool.GetComponent<Rigidbody>().isKinematic = true;

        tool.transform.position = toolParent.transform.position;
        tool.transform.rotation = toolParent.transform.rotation;

        tool.GetComponent<MeshCollider>().enabled = false;

        tool.transform.SetParent(toolParent);

        equipped = true;
        slotIsfull = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(!equipped && !slotIsfull && other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Equip();
            }
        }

        
    }
}
