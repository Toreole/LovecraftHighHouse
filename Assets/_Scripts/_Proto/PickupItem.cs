﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighHouse
{
    public class PickupItem : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected Item item;

        public void Interact(PlayerInteract player)
        {
            player.AddItem(item);
            Destroy(gameObject);
        }
    }
}