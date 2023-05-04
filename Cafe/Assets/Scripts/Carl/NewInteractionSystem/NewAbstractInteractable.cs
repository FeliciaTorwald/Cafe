using UnityEngine;

namespace Carl.NewInteractionSystem
{
    public enum NewItemType
    {
        emptyBucket,
        fullBucket,
        boba,
        finishedTea,
        dirtyTea,
        cleanTea
    }

    public abstract class NewAbstractInteractable : MonoBehaviour
    {
        [SerializeField] public NewItemType newItemType;
        public bool isHeld;
        
        public NewInteract playerInteractRef;
        public Transform toolParent;
        
        public abstract void Interact(NewInteract newInteract);
        
        public void Hold(NewInteract newInteract)
        {
            //Connect holdable to player model
            toolParent = GameObject.Find("ToolParent").transform;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            
            gameObject.transform.position = toolParent.transform.position;
            gameObject.transform.rotation = toolParent.transform.rotation;
            
            
            gameObject.GetComponent<Collider>().enabled = false;

            gameObject.transform.SetParent(toolParent);

            isHeld = true;

            newInteract.HoldingSomething(this);
            newInteract.interactables.Remove(this);
        }

        public void Drop(NewInteract newInteract)
        {
            //Detach holdable from player model
            toolParent.DetachChildren();
            gameObject.transform.eulerAngles = new Vector3(gameObject.transform.position.x, gameObject.transform.position.z, gameObject.transform.position.y);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<MeshCollider>().enabled = true;

            isHeld = false;
        
            playerInteractRef.NoLongerHoldingSomething();
        }

        public abstract void Throw(NewInteract newInteract);
        //If held item is throwable, throw it

        public abstract void TeaOperations(NewInteract newInteract);
        //Serve customer if possible

        public abstract void WaterOperations(NewInteract newInteract);
        //Water operations
    }
}