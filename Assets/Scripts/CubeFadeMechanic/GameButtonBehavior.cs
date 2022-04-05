using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonBehavior : MonoBehaviour, IButtonActivatable
{
    [SerializeField] FadeCubeAction[] cubes;
    [SerializeField] GameButtonBehavior[] buttons;
    List<IButtonActivatable> activeObjects = new List<IButtonActivatable>();
    [Header("Button state")]
    [SerializeField] bool readyTo = true;
    [SerializeField] GameObject buttonUp, buttonDown;

    private void Start()
    {
        foreach (IButtonActivatable a in cubes)
        {
            activeObjects.Add(a);
        }
        foreach (IButtonActivatable a in buttons)
        {
            activeObjects.Add(a);
            Debug.Log(a.ToString());
        }
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
        if (readyTo)
        {
            readyTo = false;
            MakeButtonAction();
            ButtonLower();
        }

    }
    public void ButtonAction()
    {
        readyTo = true;
        ButtonElevate();
    }

    void ButtonLower()
    {
        buttonDown.SetActive(true);
        buttonUp.SetActive(false);
        //transform.localScale = new Vector3(1, 0.125f, 1);
    }

    void ButtonElevate()
    {
        buttonDown.SetActive(false);
        buttonUp.SetActive(true);
        //transform.localScale = new Vector3(1, 0.25f, 1);
    }


}
