using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    protected float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        var dir = (transform.forward * z + transform.right * x).normalized;
        agent.Move(dir * speed * Time.deltaTime);

        var yRot = Input.GetAxis("Mouse X") * 50f * Time.deltaTime;
        transform.Rotate(0, yRot, 0);
    }
}
