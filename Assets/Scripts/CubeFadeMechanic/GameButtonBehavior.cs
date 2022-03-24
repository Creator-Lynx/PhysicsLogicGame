using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonBehavior : MonoBehaviour, IButtonActivatable
{
    [SerializeField]
    FadeCubeAction[] activeObjects;
    [SerializeField] bool readyTo = true;
    private void Start()
    {
        if (readyTo) ButtonElevate();
        else ButtonLower();
    }
    void MakeButtonAction()
    {
        foreach (IButtonActivatable obj in activeObjects)
        {
            obj.ButtonAction();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!readyTo) return;
        readyTo = false;
        MakeButtonAction();
        ButtonLower();
    }
    public void ButtonAction()
    {
        readyTo = true;
        ButtonElevate();
    }

    void ButtonLower()
    {
        transform.localScale = new Vector3(1, 0.125f, 1);
    }

    void ButtonElevate()
    {
        transform.localScale = new Vector3(1, 0.25f, 1);
    }


}
