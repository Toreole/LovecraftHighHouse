using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

namespace HighHouse
{
    /// <summary>
    /// Interacting with stuff.
    /// </summary>
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField]
        protected Camera cam;
        [SerializeField]
        protected GameObject uiCenterDot;
        [SerializeField]
        protected TextMeshProUGUI msg;

        protected List<Item> inventory = new List<Item>();

        private void Update()
        {
            CheckInteraction();
        }

        void CheckInteraction()
        {
            var ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 2.5f))
            {
                var interactable = hitInfo.collider.GetComponent<IInteractable>();
                var interExists = interactable != null;
                uiCenterDot.SetActive(interExists);
                if (interExists)
                {
                    if (Input.GetMouseButtonDown(0))
                        interactable.Interact(this);
                }
            }
            else
                uiCenterDot.SetActive(false);
        }

        public void AddItem(Item item)
        {
            inventory.Add(item);
            msg.alpha = 1f;
            msg.text = string.Format("{0} wurde ins Inventar gelegt.", item.name);
            StartCoroutine(FadeText());
        }

        IEnumerator FadeText()
        {
            yield return new WaitForSeconds(1f);
            for (float t = 3f; t > 0f; t -= Time.deltaTime)
            {
                msg.alpha = t / 3f;
                yield return null;
            }
            msg.alpha = 0f;
            msg.text = "";
        }

        public bool HasItem(Item item)
        {
            if (item is null)
                return true;
            return inventory.Exists(x => x == item);
        }

        public void RemoveItem(Item item)
        {
            inventory.Remove(item);
            msg.alpha = 1f;
        }

        public void Notify(string message)
        {

        }
    }
}