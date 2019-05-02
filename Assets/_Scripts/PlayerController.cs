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

        protected float currentXRot = 0f;
        protected Transform camTransform;
        protected Camera cam;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            camTransform = transform.GetChild(0);
            cam = camTransform.GetComponent<Camera>();
        }

        void Update()
        {
            Move();
            Rotate();
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
    }
}