using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RGameManager : MonoBehaviour
{
    [SerializeField] GameObject LevelScreen, SettingsScreen;
    Animation LevelScreenAnim, SettingsScreenAnim;
    [SerializeField] GameObject LevelsUI, AuthorsUI, ExitButton;
    public static int LevelsCount
    {
        get { return This.levelsCount; }
        set { This.levelsCount = value; }
    }
    public static RGameManager This;
    [Header("Levels")]
    [SerializeField] int levelsCount;
    static bool isFirstStart = true;

    private void Start()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");


        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            This = GetComponent<RGameManager>();
            DontDestroyOnLoad(this.gameObject);
        }

        if (isFirstStart)
        {
            int sceneNum = PlayerPrefs.GetInt("Completed_Levels", 0) + 1;
            if (SceneManager.GetActiveScene().buildIndex <= sceneNum)
            {
                if (sceneNum < LevelsCount)
                {
                    SceneManager.LoadScene(sceneNum);
                }
                else
                {
                    SceneManager.LoadScene(LevelsCount);
                }
            }
            isFirstStart = false;
        }

        SettingsScreen.SetActive(false);
        LevelScreen.SetActive(false);
        LevelScreenAnim = LevelScreen.GetComponent<Animation>();
        SettingsScreenAnim = SettingsScreen.GetComponent<Animation>();
    }

    public void RestartScene()
    {
        if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //something what reset object positions in the generated scene
        }


    }

    public void MuteAudio()
    {
        GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
    }

    //отметка пройденных уровней
    public static void SetComleteLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("Completed_Levels", 0))
        {
            PlayerPrefs.SetInt("Completed_Levels", PlayerPrefs.GetInt("Completed_Levels", 0) + 1);
            //analitics
#if UNITY_EDITOR
            AnalyticsResult res = Analytics.CustomEvent(
                "LevelComplete", new Dictionary<string, object>
                {
                    {"Number", PlayerPrefs.GetInt("Completed_Levels", 0)},
                    {"Name", SceneManager.GetActiveScene().name},
                    {"Develop", true}
                }
            );
            Debug.Log("analytics send result" + res);
#else
            Analytics.CustomEvent(
                "LevelComplete", new Dictionary<string, object>
                {
                    {"Number", PlayerPrefs.GetInt("Completed_Levels", 0)},
                    {"Name", SceneManager.GetActiveScene().name},
                    {"Develop", false}
                }
            );
#endif
        }

    }



    [Header("Settings elements")]
    [SerializeField]
    GameObject[] settingsElements;
    //Show Authors screen or levels
    public void ShowAuthors()
    {
        AuthorsUI.SetActive(!AuthorsUI.activeSelf);
        foreach (GameObject el in settingsElements) el.SetActive(!el.activeSelf);
    }

    //bool lvlScreenActiving = false, SettScreenActiving = false;
    bool lvlScreenAnimating = false, SettScreenAnimating = false;
    bool needToOffExitButton = true;
    //показывает экран с уровнями
    public void ShowHideLevelsScreen(bool needToOpen = true)
    {
        if (lvlScreenAnimating) return;
        if (!LevelScreen.activeSelf && needToOpen)
        {
            LevelScreen.SetActive(true);
            AuthorsUI.SetActive(false);
            ExitButton.SetActive(true);
            ShowHideSettings(false);
            lvlScreenAnimating = true;
            LevelScreenAnim.Play("LevelsOpen");
            LevelScreen.GetComponentInChildren<LevelsManager>().CreateLevelButtons();
            needToOffExitButton = false;
        }
        else if (LevelScreen.activeSelf)
        {
            LevelScreenAnim.Play("LevelsClose");
            lvlScreenAnimating = true;
            if (needToOffExitButton) ExitButton.SetActive(false);
        }

    }

    public void OnLevelsAnimationEnd(bool isOpenAnim)
    {
        lvlScreenAnimating = false;
        if (isOpenAnim)
        {
            needToOffExitButton = true;
        }
        else
        {
            LevelScreen.SetActive(false);
        }
    }

    public void ShowHideSettings(bool needToOpen = true)
    {
        if (SettScreenAnimating) return;
        if (!SettingsScreen.activeSelf && needToOpen)
        {
            SettScreenAnimating = true;
            SettingsScreen.SetActive(true);
            AuthorsUI.SetActive(false);
            ShowHideLevelsScreen(false);
            needToOffExitButton = false;
            ExitButton.SetActive(true);
            SettingsScreenAnim.Play("SettingsOpen");
        }
        else if (SettingsScreen.activeSelf)
        {
            SettingsScreenAnim.Play("SettingsClose");
            SettScreenAnimating = true;
            AuthorsUI.SetActive(false);
            if (needToOffExitButton) ExitButton.SetActive(false);

        }



    }

    public void OnSettingsAnimationEnd(bool isOpenAnim)
    {
        SettScreenAnimating = false;
        if (isOpenAnim)
        {
            needToOffExitButton = true;

        }
        else
        {
            SettingsScreen.SetActive(false);
        }
    }

    public void HideAllPanels()
    {
        ShowHideLevelsScreen(false);
        ShowHideSettings(false);
        ExitButton.SetActive(false);
    }


    public void OpenAuthorsLink(int a)
    {
        if (a == 0)
        {
            Application.OpenURL("https://t.me/train_game");
        }
        if (a == 1)
        {
            Application.OpenURL("https://t.me/umqrahound");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) PlayerPrefs.SetInt("Completed_Levels", 0);
    }





}
