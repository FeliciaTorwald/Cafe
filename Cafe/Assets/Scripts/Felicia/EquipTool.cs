using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipTool : MonoBehaviour
{
    public GameObject tool;
    public GameObject playerRef;
    public Transform toolParent;
    public bool equipped;
    public static bool slotIsfull;
    bool inTriggerArea;
    void Start()
    {
        playerRef = GameObject.Find("Player Object");
        //playerRef.GetComponent<BobaShooterController>().enabled= false;
        //tool.GetComponent<Rigidbody>().isKinematic = true;
        toolParent = GameObject.Find("ToolParent").transform;//now transfom works with prefabs
    }
    //use meshcollider,turn on convex then add boxcollider as trigger

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

        if(inTriggerArea == true)
        {
            //playerRef.GetComponent<BobaShooterController>().enabled = true;
        }
         if(inTriggerArea == false)
        {
            //playerRef.GetComponent<BobaShooterController>().enabled = false;
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
        if(tool == null)
        {
            slotIsfull = false;
            equipped = false;
        }
        if (toolParent == null)
        {
            slotIsfull = false;
            equipped = false;
        }

        if(!equipped && !slotIsfull && other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Equip();  
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BobaPearls"))
        {
            inTriggerArea = true;
            Debug.Log("inTriggerArea = true");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BobaPearls"))
        {
            inTriggerArea = false;
            Debug.Log("inTriggerArea = false");
        }
    }
}
