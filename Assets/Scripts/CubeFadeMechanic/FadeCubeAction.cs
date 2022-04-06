using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCubeAction : MonoBehaviour, IButtonActivatable
{
    [SerializeField]
    Material main, transparent;
    [SerializeField] bool state = true;

    Collider[] childsColl;
    MeshRenderer[] childsMesh;
    void Start()
    {
        //GetComponent<MeshRenderer>().material = state ? main : transparent;
        //GetComponent<Collider>().isTrigger = !state;
        childsMesh = GetComponentsInChildren<MeshRenderer>();
        childsColl = GetComponentsInChildren<Collider>();
        for (int i = 0; i < childsColl.Length; i++)
        {
            childsColl[i].isTrigger = !state;
            childsMesh[i].material = state ? main : transparent;
        }
    }
    public void ButtonAction()
    {
        state = !state;
        //GetComponent<MeshRenderer>().material = state ? main : transparent;
        //GetComponent<Collider>().isTrigger = !state;
        for (int i = 0; i < childsColl.Length; i++)
        {
            childsColl[i].isTrigger = !state;
            childsMesh[i].material = state ? main : transparent;
        }
    }

}
