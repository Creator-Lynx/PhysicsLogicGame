using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RGameManager : MonoBehaviour
{
    [SerializeField] GameObject LevelScreen, SettingsScreen;
    [SerializeField] GameObject LevelsUI, AuthorsUI;
    public static int LevelsCount
    {
        get { return This.levelsCount; }
        set { This.levelsCount = value; }
    }
    static RGameManager This;
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
        }

    }

    //показывает экран с уровнями
    public void ShowLevelsScreen()
    {
        LevelScreen.SetActive(!LevelScreen.activeSelf);
        AuthorsUI.SetActive(false);
        LevelsUI.SetActive(true);
        LevelScreen.GetComponentInChildren<LevelsManager>().CreateLevelButtons();
    }

    //Show Authors screen or levels
    public void ShowAuthors()
    {
        AuthorsUI.SetActive(!AuthorsUI.activeSelf);
        LevelsUI.SetActive(!LevelsUI.activeSelf);
    }

    //
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

    public void ShowHideSettings()
    {
        SettingsScreen.SetActive(!SettingsScreen.activeSelf);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) PlayerPrefs.SetInt("Completed_Levels", 0);
    }




}
