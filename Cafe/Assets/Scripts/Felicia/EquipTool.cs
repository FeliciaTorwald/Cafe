using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EquipTool : Pickupable
{
    public GameObject tool;
    public Transform toolParent;
    public bool equipped;
    public static bool slotIsFull;
    public bool inCollision;
    Get_water_In_Teapot gWIT;
    BobaShooterController bSC;
    public bool canYouEquipBoba;
    public float force = 200;
    BobaTeaHandler bTH;
    List<BobaTeaHandler> bTHs;
    WaterPickup wP;

    [SerializeField] private ToolType toolType;
    
    public override void Interact()
    {
        //picks up tools
        if (!equipped && !slotIsFull && inCollision)
        {
            Equip();
        }

        else if (equipped && slotIsFull)
        {
            foreach (var bobaTea in bTHs)
            {
                if (bobaTea.inTriggerArea)
                {
                    Debug.Log("tea in area");
                    bobaTea.ServedSequence();
                    return;
                }
            }
            //if (bTH.inTriggerArea)
            //{
            //    bTH.ServedSequence();
            //}
            //Picks up water

            if (wP.PTriggerArea)
            {
                wP.AddWaterToBucket();
                if (wP.BPTriggerArea)
                {
                    wP.AddWaterToKettle();
                }
            }

            //Pours in kettle
            else if (wP.BPTriggerArea)
            {
                wP.AddWaterToKettle();
            }

            //drops tool
            else if (wP.PTriggerArea == false && wP.BPTriggerArea == false)
            {
                Drop();
            }

            //Shoot();

        }

    }
    void Start()
    {
        tool.GetComponent<Rigidbody>().isKinematic = true;
        toolParent = GameObject.Find("ToolParent").transform;//now transfom works with prefabs
        gWIT = FindFirstObjectByType<Get_water_In_Teapot>();
        bSC = FindFirstObjectByType<BobaShooterController>();
        bTH = FindObjectOfType<BobaTeaHandler>();
        bTHs = FindObjectsOfType<BobaTeaHandler>().ToList<BobaTeaHandler>();

        wP = FindObjectOfType<WaterPickup>();
    }
    //use meshcollider,turn on convex then add boxcollider as trigger

    // Update is called once per frame
    void Update()
    {
        //pick up tools
        //if (!equipped && !slotIsfull && inCollision)
        //{
        //   if (Input.GetKeyDown(KeyCode.E))
        //   {
        //        Equip();
        //        //canYouEquipBoba = false;
        //   }

        //}
        //picks up boba
        //else if (!equipped && !slotIsfull && bSC.inTriggerArea == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        bSC.PickingUpBall();
        //        equipped= true;
        //        Debug.Log(equipped);
        //    }
        //}

        //else if (canYouEquipBoba)
        //{
        //    if (bSC.inTriggerArea == true)
        //    {
        //        if (Input.GetKeyDown(KeyCode.E))
        //        {
        //            bSC.PickingUpBall();
        //        }
        //    }
        //}

        //else if (equipped && slotIsfull)
        //{
        //    //picks up water
        //    if (gWIT.inTriggerArea == true)
        //    {
        //        if (Input.GetKeyDown(KeyCode.E))
        //        {
        //            gWIT.PickingUpWater();
        //        }
        //    }

        //    //drops tool
        //    else if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        Drop();
        //        canYouEquipBoba = true;
        //    }
        //}
    }

    public ToolType IdentifyToolType()
    {
        return toolType;
    }
    
    void Drop()
    {
        toolParent.DetachChildren();
        tool.transform.eulerAngles = new Vector3(tool.transform.position.x, tool.transform.position.z, tool.transform.position.y);
        tool.GetComponent<Rigidbody>().isKinematic = false;
        tool.GetComponent<MeshCollider>().enabled = true;

        equipped = false;
        slotIsFull = false;
    }


    void Equip()
    {
        tool.GetComponent<Rigidbody>().isKinematic = true;

        tool.transform.position = toolParent.transform.position;
        tool.transform.rotation = toolParent.transform.rotation;

        tool.GetComponent<MeshCollider>().enabled = false;

        tool.transform.SetParent(toolParent);

        equipped = true;
        slotIsFull = true;
    }


    private void OnTriggerStay(Collider other) // should deal with some ghostobjects, if item is destroyed in your hand(parentTool) you need to set the equipped bool to false in the method that destroys it
    {
        if (tool == null)
        {
            slotIsFull = false;
            equipped = false;
        }

        if (toolParent == null)
        {
            toolParent.DetachChildren();
            slotIsFull = false;
            equipped = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!equipped && !slotIsFull && other.gameObject.tag == "Player")
        {
            inCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!equipped && !slotIsFull && other.gameObject.tag == "Player")
        {
            inCollision = false;
        }
    }
}
