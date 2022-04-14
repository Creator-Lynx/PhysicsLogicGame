using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsBehaviors : MonoBehaviour
{
    public void MuteButtonChanger()
    {

    }

    public int level;

    [SerializeField]
    bool themeButton = false;
    public void StartLevel()
    {
        RGameManager.OnLevelEnd();
        SceneManager.LoadScene(level);
    }


    [SerializeField]
    Sprite buttonSpriteOn;
    public bool buttonState;
    public void ThemeButtonChanger()
    {
        GetComponent<Image>().overrideSprite = buttonState ? null : buttonSpriteOn;
        buttonState = !buttonState;
    }
    private void Awake()
    {
        if (themeButton)
        {
            buttonState = PlayerPrefs.GetInt("ChangeTheme", 1) == 1;
            ThemeButtonChanger();
            ThemeButtonChanger();
        }
    }
}
