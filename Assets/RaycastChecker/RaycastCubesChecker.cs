using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCubesChecker : MonoBehaviour
{
    void Start()
    {
        VerticalChecker();
    }
    bool[,] cubeMatrix = new bool[9, 9];
    //throw vertical rays to check, where cube is
    void VerticalChecker()
    {
        Vector3 startCheckPos = transform.position + new Vector3(-4, 0, -4);

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                //cast the ray
                Vector3 rayStart = startCheckPos + new Vector3(i, 3f, j);
                Debug.DrawRay(rayStart, Vector3.down * 3, Color.blue, 10f);
                RaycastHit[] coll = Physics.RaycastAll(rayStart, Vector3.down, 10);
                foreach (var c in coll)
                {

                }
            }
        }
    }
}
