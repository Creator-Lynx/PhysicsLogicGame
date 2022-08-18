using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoCache
{
    // Start is called before the first frame update
    void Start()
    {

    }


    public override void OnTick()
    {
        Debug.DrawRay(Vector3.up * 2, new Vector3(4, 0, 10), Color.blue, 5f, false);
    }
}
