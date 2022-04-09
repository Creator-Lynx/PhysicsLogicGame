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
    static RGameManager This;
    [Header("Levels")]
    [SerializeField] int levelsCount;
    static bool isFirstStart = true;

    private void Start()
    {
        This = GetComponent<RGameManager>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");


        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
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

    bool lvlScreenActiving = false, SettScreenActiving = false;
    bool lvlScreenAnimating = false, SettScreenAnimating = false;
    //показывает экран с уровнями
    public void ShowLevelsScreen()
    {
        if (lvlScreenAnimating) return;
        if (!lvlScreenActiving)
        {
            SettingsScreen.SetActive(false);
            LevelScreen.SetActive(true);
            AuthorsUI.SetActive(false);
            LevelsUI.SetActive(true);
            LevelScreen.GetComponentInChildren<LevelsManager>().CreateLevelButtons();
            ExitButton.SetActive(!ExitButton.activeSelf);
            lvlScreenAnimating = true;
            LevelScreenAnim.Play("LevelsOpen");
            lvlScreenActiving = true;
        }
        else
        {
            ExitButton.SetActive(!ExitButton.activeSelf);
            LevelScreenAnim.Play("LevelsClose");
            lvlScreenAnimating = true;
            lvlScreenActiving = false;
        }

    }

    public void OnLevelsAnimationEnd(bool isOpenAnim)
    {
        lvlScreenAnimating = false;
        if (!isOpenAnim) LevelScreen.SetActive(false);
    }

    public void ShowHideSettings()
    {
        SettingsScreen.SetActive(!SettingsScreen.activeSelf);
        AuthorsUI.SetActive(false);
        LevelScreen.SetActive(false);
        foreach (GameObject el in settingsElements) if (!el.activeSelf) el.SetActive(true);
        ExitButton.SetActive(!ExitButton.activeSelf);
    }

    public void OnSettingsAnimationEnd(bool isOpenAnim)
    {
        lvlScreenAnimating = false;
        if (!isOpenAnim) LevelScreen.SetActive(false);
    }

    public void HideAllPanels()
    {
        ShowLevelsScreen();
        SettingsScreen.SetActive(false);
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
