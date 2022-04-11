using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsAnimationController : MonoBehaviour
{
    public void OnLevelsScreenOpenAnimationEnd()
    {
        RGameManager.This.OnLevelsAnimationEnd(true);
    }
    public void OnLevelsScreenCloseAnimationEnd()
    {
        RGameManager.This.OnLevelsAnimationEnd(false);
    }
    public void OnSettingsScreenOpenAnimationEnd()
    {
        RGameManager.This.OnSettingsAnimationEnd(true);
    }
    public void OnSettingsScreenCloseAnimationEnd()
    {
        RGameManager.This.OnSettingsAnimationEnd(false);
    }
}
