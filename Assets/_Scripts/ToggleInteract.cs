using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HighHouse {
    public class ToggleInteract : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected Animator anim;

        public void Interact(PlayerController player)
        {
            anim.SetTrigger("Toggle");
        }
    }
}