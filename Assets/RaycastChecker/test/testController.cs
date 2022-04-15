using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour
{
    Rigidbody rig;
    float speed = 5f;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    // use some input
    float horInput, verInput;
    void Update()
    {
        horInput = Input.GetAxis("Horizontal");
        verInput = Input.GetAxis("Vertical");
    }

    // test moving by input with raycast checker condition
    void FixedUpdate()
    {
        Vector3 hor = new Vector3(horInput, 0f, 0f) * speed;
        Vector3 ver = new Vector3(0f, 0f, verInput) * speed;
        rig.velocity = hor + ver;
    }
}
