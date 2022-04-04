using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rigidVelocityChecker : MonoBehaviour
{
    Rigidbody rig;
    [SerializeField]
    float zeroMagnitude = 0.05f;
    [SerializeField]
    float timeToChange = 0.8f;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    int counterToChange = 0;
    bool counting = true;
    void FixedUpdate()
    {
        if (!counting) return;
        if (rig.velocity.magnitude > zeroMagnitude)
        {
            counterToChange++;
            if (counterToChange > timeToChange / Time.fixedDeltaTime)
                ChangeScale();
        }
    }

    void ChangeScale()
    {

        counting = false;
        if (GetComponent<Player>().isVert)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
            ScaleCalc(transform.localScale.z));
        }
        if (GetComponent<Player>().isHor)
        {
            transform.localScale = new Vector3(ScaleCalc(transform.localScale.x), transform.localScale.y,
            transform.localScale.z);
        }
    }
    int ScaleCalc(float x)
    {
        x = x / 2 + 0.5f;
        if (x > 1) return (int)x;
        else return 1;
    }
}
