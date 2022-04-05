using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCubeAction : MonoBehaviour, IButtonActivatable
{
    [SerializeField]
    Material main, transparent;
    [SerializeField] bool state = true;
    void Start()
    {
        GetComponent<MeshRenderer>().material = state ? main : transparent;
        GetComponent<Collider>().isTrigger = !state;
    }
    public void ButtonAction()
    {
        state = !state;
        GetComponent<MeshRenderer>().material = state ? main : transparent;
        GetComponent<Collider>().isTrigger = !state;
    }
    public enum CubeFadeActionMode
    {
        switchMat,
        defaultMat,
        transparentMat
    }
}
