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
        [SerializeField] public bool isThrowAble;
        [SerializeField] public bool isHoldAble;
        [SerializeField] public NewItemType newItemType;
        public bool isHeld;

        public NewInteract playerInteractRef;
        
        //Positioning bools
        public bool closeToWater;
        public bool closeToPot;
        public bool closeToServableCustomer;
        public bool closeToNastyCustomer;

        public void Interact(NewInteract newInteract)
        {
            playerInteractRef = newInteract;
        
            if (isHeld)
            {
                if (newItemType is NewItemType.boba or NewItemType.dirtyTea)
                {
                    Throw(playerInteractRef);
                }
                else if (newItemType is NewItemType.finishedTea)
                {
                    Serve(playerInteractRef);
                }
                else if (newItemType is NewItemType.emptyBucket or NewItemType.fullBucket)
                {
                    WaterOperations(playerInteractRef);
                }
            }
            else if (isHoldAble)
                Hold(playerInteractRef);
            else if (isThrowAble) 
                Throw(playerInteractRef); 
            else
            {
                //Do interaction things
            }
        }
        //Default interact function that is called on all interactables

        public abstract void Hold(NewInteract newInteract);
        //Connect holdable to player model

        public abstract void Drop(NewInteract newInteract);
        //Detach holdable from player model

        public abstract void Throw(NewInteract newInteract);
        //If held item is throwable, throw it

        public abstract void Serve(NewInteract newInteract);
        //Serve customer if possible

        public abstract void WaterOperations(NewInteract newInteract);
        //Water operations
    }
}