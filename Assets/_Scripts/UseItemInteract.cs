using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HighHouse
{
    public class UseItemInteract : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected Item requiredItem;
        [SerializeField]
        protected bool removeItemAfterUse = true;
        [SerializeField]
        protected UnityEvent onInteractSuccess;

        public void Interact(PlayerInteract player)
        {
            if (player.HasItem(requiredItem))
            {
                onInteractSuccess?.Invoke();
                //todo: do stuffs
                if (removeItemAfterUse)
                    player.RemoveItem(requiredItem);
            }
            else
            {
                player.Notify("I think i need something here...");
            }
        }
    }
}