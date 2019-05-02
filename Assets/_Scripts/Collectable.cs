using UnityEngine;
using System.Collections;

namespace HighHouse
{
    [RequireComponent(typeof(Collider))]
    public class Collectable : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected Item item;

        public void Interact(PlayerInteract player)
        {
            //todo: add item to players inventory, probably mark it as collected to avoid respawns
            Destroy(gameObject);
        }
    }
}