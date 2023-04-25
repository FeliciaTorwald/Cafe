using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using UnityEngine;

//use meshcollider,turn on convex then add boxcollider as trigger

public class EquipTool : Pickupable
{
    public GameObject tool;
    public Transform toolParent;
    public bool equipped;
    public static bool slotIsFull;
    public bool inCollision;
    public bool canYouEquipBoba;
    public float force = 1000;
    List<BobaTeaHandler> bTHs;
    WaterPickup wP;
    bool guestInRange;
    private PickupManager pickupManager;

    public override void Interact()
    {
        //picks up tools
        if (!equipped && !slotIsFull && inCollision)
        {
            Equip();
        }

        else if (equipped && slotIsFull)
        {
            // if (guestInRange)
            // {
            //     foreach (var bobaTea in bTHs)
            //     {
            //         if (bobaTea.inTriggerArea)
            //         {
            //             bobaTea.ServedSequence();
            //             return;
            //         }
            //     }
            // }

            //drops tool
            if (IdentifyToolType() != ToolType.EmptyBucket)
            {
                Drop();
            }
            else if (wP.PTriggerArea == false)
            {
                if (wP.BPTriggerArea == false || !GetComponent<WaterPickup>().hasWater)
                {
                    Drop();
                }
            }

            if (IdentifyToolType() == ToolType.EmptyBucket)
            {
                //Picks up water

                if (wP.PTriggerArea)
                {
                    wP.AddWaterToBucket();
                }

                //Pours in kettle
                else if (wP.BPTriggerArea)
                {
                    wP.AddWaterToKettle();
                }
            }
        }
    }

    void Start()
    {
        tool.GetComponent<Rigidbody>().isKinematic = true;
        toolParent = GameObject.Find("ToolParent").transform; //now transfom works with prefabs
        bTHs = FindObjectsOfType<BobaTeaHandler>().ToList();
        wP = FindObjectOfType<WaterPickup>();
        pickupManager = FindObjectOfType<PickupManager>();
    }
    private void Update()
    {
        if (equipped)
        {
            if (IdentifyToolType() == ToolType.EmptyTea)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Shoot();
                }

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
        slotIsFull = false;

        pickupManager.NoLongerHoldingSomething();
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

        pickupManager.HoldingSomething(this);
        pickupManager.pickupables.Remove(this);
    }

    void Shoot()
    {
        toolParent.DetachChildren();
        tool.GetComponent<Rigidbody>().isKinematic = false;
        tool.GetComponent<MeshCollider>().enabled = true;
        tool.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0.5f, 2) * 10, ForceMode.Impulse);

        equipped = false;
        slotIsFull = false;

        pickupManager.NoLongerHoldingSomething();
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
            // toolParent.DetachChildren();
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

        if (other.gameObject.tag == "Guest")
        {
            guestInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!equipped && !slotIsFull && other.gameObject.tag == "Player")
        {
            inCollision = false;
        }

        if (other.gameObject.tag == "Guest")
        {
            guestInRange = false;
        }
    }
}
