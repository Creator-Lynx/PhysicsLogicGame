using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RGameManager : MonoBehaviour
{
    [SerializeField] GameObject LevelScreen, SettingsScreen;
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



    [Header("Settings elements")]
    [SerializeField]
    GameObject[] settingsElements;
    //Show Authors screen or levels
    public void ShowAuthors()
    {
        AuthorsUI.SetActive(!AuthorsUI.activeSelf);
        foreach (GameObject el in settingsElements) el.SetActive(!el.activeSelf);
    }

    //показывает экран с уровнями
    public void ShowLevelsScreen()
    {
        SettingsScreen.SetActive(false);
        LevelScreen.SetActive(!LevelScreen.activeSelf);
        AuthorsUI.SetActive(false);
        LevelsUI.SetActive(true);
        LevelScreen.GetComponentInChildren<LevelsManager>().CreateLevelButtons();
        ExitButton.SetActive(!ExitButton.activeSelf);
    }

    public void ShowHideSettings()
    {
        SettingsScreen.SetActive(!SettingsScreen.activeSelf);
        AuthorsUI.SetActive(false);
        LevelScreen.SetActive(false);
        foreach (GameObject el in settingsElements) if (!el.activeSelf) el.SetActive(true);
        ExitButton.SetActive(!ExitButton.activeSelf);
    }

    public void HideAllPanels()
    {
        SettingsScreen.SetActive(false);
        LevelScreen.SetActive(false);
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
