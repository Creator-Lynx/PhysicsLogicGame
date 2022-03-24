using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCubeAction : MonoBehaviour, IButtonActivatable
{
    [SerializeField]
    Material main, transparent;
    bool state = true;
    public void ButtonAction()
    {
        /*switch ((int)mode) 
        {
            case 0:
                state = !state;
                break;
            case 1:
                state = true;
                break;
            case 2:
                state = false;
                break;
        }*/
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
