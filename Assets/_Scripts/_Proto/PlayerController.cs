using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

namespace HighHouse
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        protected NavMeshAgent agent;
        [SerializeField]
        protected float speed = 2f;
        [SerializeField]
        protected float xRotLimit = 50f;
        [SerializeField]
        protected GameObject uiCenterDot;
        [SerializeField]
        protected TextMeshProUGUI itemPickupText;

        protected List<Item> inventory = new List<Item>();

        protected float currentXRot = 0f;
        protected Transform camTransform;
        protected Camera cam;

        private void Start()
        {
            camTransform = transform.GetChild(0);
            cam = camTransform.GetComponent<Camera>();
        }

        void Update()
        {
            Move();
            Rotate();
            CheckInteraction();
        }

        private void Move()
        {
            //Movement
            var x = Input.GetAxis("Horizontal");
            var z = Input.GetAxis("Vertical");
            var dir = (transform.forward * z + transform.right * x).normalized;
            agent.Move(dir * speed * Time.deltaTime);
        }

        void Rotate()
        {
            //Look left / right
            var yRot = Input.GetAxis("Mouse X") * 50f * Time.deltaTime;
            if (!Mathf.Approximately(yRot, 0f))
                transform.Rotate(0f, yRot, 0f);

            //Look up/down (clamped)
            var desiredXDelta = -Input.GetAxis("Mouse Y") * 45f * Time.deltaTime;
            var xDelta = Mathf.Clamp(desiredXDelta, -xRotLimit - currentXRot, xRotLimit - currentXRot);
            if (!Mathf.Approximately(xDelta, 0f))
            {
                camTransform.Rotate(xDelta, 0f, 0f);
                currentXRot += xDelta;
            }
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
            itemPickupText.alpha = 1f;
            itemPickupText.text = string.Format("{0} wurde ins Inventar gelegt.", item.name);
            StartCoroutine(FadeText());
        }

        IEnumerator FadeText()
        {
            yield return new WaitForSeconds(1f);
            for (float t = 3f; t > 0f; t -= Time.deltaTime)
            {
                itemPickupText.alpha = t / 3f;
                yield return null;
            }
            itemPickupText.alpha = 0f;
            itemPickupText.text = "";
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
            itemPickupText.alpha = 1f;
            itemPickupText.text = string.Format("{0} wurde vom Inventar entfernt.", item.name);
            StartCoroutine(FadeText());
        }

        public void MissingItem()
        {
            itemPickupText.alpha = 1f;
            itemPickupText.text = "Hier fehlt etwas.";
            StartCoroutine(FadeText());
        }
    }
}