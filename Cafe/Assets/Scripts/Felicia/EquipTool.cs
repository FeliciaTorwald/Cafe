using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipTool : MonoBehaviour
{
    public GameObject tool;
    public Transform toolParent;
    public bool equipped;
    public static bool slotIsfull;
    public bool inCollision;
    Get_water_In_Teapot gWIT;

    void Start()
    {
        tool.GetComponent<Rigidbody>().isKinematic = true;
        toolParent = GameObject.Find("ToolParent").transform;//now transfom works with prefabs
        gWIT  = FindFirstObjectByType<Get_water_In_Teapot>();
        
    }
    //use meshcollider,turn on convex then add boxcollider as trigger

    // Update is called once per frame
    void Update()
    {
        if (!equipped && !slotIsfull && inCollision)
        {
           if (Input.GetKeyDown(KeyCode.E))
            {
                Equip();
            }
        }
       

        else if (equipped && slotIsfull)
        {

            if(gWIT.inTriggerArea == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    gWIT.PickingUpWater();
                }
            }
            
            else if (Input.GetKeyDown(KeyCode.E))
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

    private void OnTriggerStay(Collider other) // should deal with ghostobjects
    {
        if(tool == null)
        {
            slotIsfull = false;
            equipped = false;
        }
        if (toolParent == null)
        {
            toolParent.DetachChildren();
            slotIsfull = false;
            equipped = false;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (!equipped && !slotIsfull && other.gameObject.tag == "Player")
        {
             inCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
        {
        if (!equipped && !slotIsfull && other.gameObject.tag == "Player")
         {
            inCollision = false;
         }

    }

}
