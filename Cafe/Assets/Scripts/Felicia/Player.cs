using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool chair;
    public bool canServe;
    EquipTool eT;
    List<BobaTeaHandler> bTHs;
    public List<Interactable> interactables;

    private void Start()
    {
        eT = FindObjectOfType<EquipTool>();
        bTHs = FindObjectsOfType<BobaTeaHandler>().ToList();
        interactables = new List<Interactable>();
    }
    private void Update()
    {

        if (HoldingTea())
        {
            //foreach (var guestRef in bTHs)
            //{
            //    if (guestRef.guestRef != null)
            //    {
            //        canServe = true;
            //        if(canServe)
            //        {
            //            if (Input.GetKeyDown(KeyCode.E))
            //            {
            //             UpdateClosest();
            //             guestRef.Interact();
            //             Debug.Log(interactables);
            //            }
            //        }
                    
            //        return;
            //    }
            //}
            if(interactables.Count > 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                Interactable temp = UpdateClosest();
                if (temp.GetComponent<BobaTeaHandler>().guestRef != null)
                    temp.GetComponent<BobaTeaHandler>().Interact();

                }
                //UpdateClosest().GetComponent<BobaTeaHandler>().Interact();
            }
        }

    }

    private bool HoldingTea()
    {
        EquipTool[] tools = FindObjectsOfType<EquipTool>();
        foreach (EquipTool tool in tools)
        {
            if (tool.IdentifyToolType() == ToolType.Tea && tool.equipped)
            {
                return true;
            }
        }
        return false;
    }

    private Interactable UpdateClosest()
    {
        CleanList();
        if (interactables.Count == 0)
        {
            return null;
        }
        Interactable closest = interactables.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
        return closest;
    }

    private void CleanList()
    {
        for (int i = interactables.Count - 1; i >= 0; i--)
        {
            if (interactables[i] == null)
            {
                interactables.RemoveAt(i);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //chair = true;
        //if(other.tag == "Boba")
        //{
        //    BobaTeaHandler temp = other.GetComponent<BobaTeaHandler>();
        //    //BobaTeaHandler closest = bTHs.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
        //    bTHs.Add(temp);
        //}

        if (other.GetComponent<Interactable>() != null)
        {
            interactables.Add(other.GetComponent<Interactable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //chair = false;
        //if (other.tag == "Boba")
        //{
        //    BobaTeaHandler temp = other.GetComponent<BobaTeaHandler>();
        //    //BobaTeaHandler closest = bTHs.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).First();
        //    bTHs.Remove(temp);
        //}

        if (other.GetComponent<Interactable>() != null)
        {
            interactables.Remove(other.GetComponent<Interactable>());
        }
    }
}
