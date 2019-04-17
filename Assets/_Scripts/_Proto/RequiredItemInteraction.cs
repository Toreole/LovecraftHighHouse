using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace HighHouse
{
    public class RequiredItemInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected Item requiredItem;
        [SerializeField]
        protected UnityEvent onInteractSuccess;

        public void Interact(PlayerController player)
        {
            if (player.HasItem(requiredItem))
            {
                player.RemoveItem(requiredItem);
                onInteractSuccess?.Invoke();
                Destroy(this);
            }
            else
                player.MissingItem();
        }
    }
}