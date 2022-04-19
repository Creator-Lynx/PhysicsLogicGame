using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testController : MonoBehaviour
{
    Rigidbody rig;
    float speed = 5f;
    RaycastCubesChecker checker;
    void Start()
    {
        checker = GetComponent<RaycastCubesChecker>();
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

        if (horInput > 0)
        {
            if (checker.CastRightRays()) horInput = 0;
        }
        else
        {
            if (checker.CastLeftRays()) horInput = 0;
        }
        if (verInput > 0)
        {
            if (checker.CastForwardRays()) verInput = 0;
        }
        else
        {
            if (checker.CastBackwardRays()) verInput = 0;
        }

        rig.velocity = new Vector3(horInput, 0f, verInput) * speed;
    }
}
