using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCubesChecker : MonoBehaviour
{
    void Start()
    {
        VerticalChecker();
        string s = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                s += (cubeMatrix[i, j] ? 1 : 0) + " ";

            }
            s += '\n';
        }
        Debug.Log(s);
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
                    if (c.collider.gameObject == gameObject)
                    {
                        cubeMatrix[i, j] = true;
                    }
                    else
                    {
                        int find = name.IndexOf(' ');
                        int target = find == -1 ? name.Length : find;
                        if (c.collider.name.Substring(0, target) == name.Substring(0, target) && c.collider.transform.parent != null)
                        {
                            if (c.collider.transform.parent == transform)
                            {
                                cubeMatrix[i, j] = true;
                            }
                        }

                    }

                }
            }
        }
    }
}
