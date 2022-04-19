using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCubesChecker : MonoBehaviour
{

    void Start()
    {
        VerticalChecker();
        //ShowCubeMatrix();// testing
        SetVectorsList();
        //ShowVectors(); // testing

    }
    void Update() //testing
    {
        DrawVectors();
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
                Debug.DrawRay(rayStart, Vector3.down * 3, Color.blue, 4f);
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
                        if (c.collider.transform.parent != null)
                            if (c.collider.transform.parent == transform)
                                if (c.collider.name.Substring(0, target) == name.Substring(0, target))
                                {
                                    cubeMatrix[i, j] = true;
                                }

                    }

                }
            }
        }
    }
    void ShowCubeMatrix()
    {
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

    void ShowVectors()
    {
        foreach (var v in upVectors)
        {
            Debug.Log(v + "UpVector");
        }
        foreach (var v in downVectors)
        {
            Debug.Log(v + "DownVector");
        }
        foreach (var v in leftVectors)
        {
            Debug.Log(v + "LeftVector");
        }
        foreach (var v in rightVectors)
        {
            Debug.Log(v + "RightVector");
        }

    }
    void DrawVectors()
    {
        foreach (var v in upVectors)
        {
            Debug.DrawRay(transform.position + v, Vector3.forward * rayDistance, Color.red, Time.deltaTime);
        }
        foreach (var v in downVectors)
        {
            Debug.DrawRay(transform.position + v, Vector3.back * rayDistance, Color.red, Time.deltaTime);
        }
        foreach (var v in leftVectors)
        {
            Debug.DrawRay(transform.position + v, Vector3.left * rayDistance, Color.red, Time.deltaTime);
        }
        foreach (var v in rightVectors)
        {
            Debug.DrawRay(transform.position + v, Vector3.right * rayDistance, Color.red, Time.deltaTime);
        }
    }
    List<Vector3> upVectors = new List<Vector3>(),
    downVectors = new List<Vector3>(),
    leftVectors = new List<Vector3>(),
    rightVectors = new List<Vector3>();

    void SetVectorsList()
    {
        bool Check(int x, int z, int dx, int dz)
        {
            if (x + dx < cubeMatrix.Length && z + dz < cubeMatrix.Length && x + dx >= 0 && z + dz >= 0)
            {
                if (cubeMatrix[x + dx, z + dz])
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else return true;
        }
        for (int x = -4; x <= 4; x++)
            for (int z = -4; z <= 4; z++)
            {
                int i = x + 4;
                int j = z + 4;
                if (cubeMatrix[i, j])
                {
                    if (Check(i, j, 0, 1)) upVectors.Add(new Vector3(x, 0, z));
                    if (Check(i, j, 0, -1)) downVectors.Add(new Vector3(x, 0, z));
                    if (Check(i, j, -1, 0)) leftVectors.Add(new Vector3(x, 0, z));
                    if (Check(i, j, 1, 0)) rightVectors.Add(new Vector3(x, 0, z));
                }

            }
    }


    [SerializeField] float rayDistance = 0.5f;
    public bool CastForwardRays()
    {
        bool res = false;
        foreach (var v in upVectors)
        {
            if (Physics.Raycast(transform.position + v, Vector3.forward, out RaycastHit hitInfo, rayDistance))
                if (!hitInfo.collider.isTrigger)
                {
                    res = true;
                    break;
                }

        }
        return res;
    }
    public bool CastBackwardRays()
    {
        bool res = false;
        foreach (var v in downVectors)
        {
            if (Physics.Raycast(transform.position + v, Vector3.forward, out RaycastHit hitInfo, rayDistance))
                if (!hitInfo.collider.isTrigger)
                {
                    res = true;
                    break;
                }

        }
        return res;
    }
    public bool CastLeftRays()
    {
        bool res = false;
        foreach (var v in leftVectors)
        {
            if (Physics.Raycast(transform.position + v, Vector3.forward, out RaycastHit hitInfo, rayDistance))
                if (!hitInfo.collider.isTrigger)
                {
                    res = true;
                    break;
                }

        }
        return res;
    }
    public bool CastRightRays()
    {
        bool res = false;
        foreach (var v in rightVectors)
        {
            if (Physics.Raycast(transform.position + v, Vector3.forward, out RaycastHit hitInfo, rayDistance))
                if (!hitInfo.collider.isTrigger)
                {
                    res = true;
                    break;
                }

        }
        return res;
    }
}
