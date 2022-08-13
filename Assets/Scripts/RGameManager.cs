using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RGameManager : MonoBehaviour
{
    [SerializeField] GameObject LevelScreen, SettingsScreen, AnalyticsMessage;
    Animation LevelScreenAnim, SettingsScreenAnim;
    [SerializeField] GameObject LevelsUI, AuthorsUI, ExitButton;
    [SerializeField] AudioSource LevelCompleteAudio;
    public static int LevelsCount
    {
        get { return This.levelsCount; }
        set { This.levelsCount = value; }
    }
    public static RGameManager This;
    [Header("Levels")]
    [SerializeField] int levelsCount;
    static bool isFirstStart = true;


    //for custom ad
    static int currentFinishedLevels = 0;
    static int currentRestartedLevels = 0;
    static float lastAdCallTime = 0;

    [SerializeField]
    int restartsToAd = 12;
    public static int levelsToAd = 7;
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
            CheckLocalization();
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
        {
            OnLevelEnd();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //ad
            currentRestartedLevels++;
            if (currentRestartedLevels > restartsToAd)
            {
                if (Time.time > lastAdCallTime + (5 * 60))
                {
                    currentRestartedLevels = 0;
                    YandexSDK.instance.ShowInterstitial();
                    lastAdCallTime = Time.time;
                }

            }
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            OnLevelEnd();
            //something what reset object positions in the generated scene
        }


    }

    [SerializeField]
    Image muteButton;
    [SerializeField]
    Sprite soundOn, soundOff;
    bool isMuted = false;
    public void MuteAudio()
    {
        isMuted = !isMuted;
        GetComponent<AudioListener>().enabled = !GetComponent<AudioListener>().enabled;
        muteButton.sprite = muteButton.sprite == soundOff ? soundOn : soundOff;
    }

    //отметка пройденных уровней
    public static void SetComleteLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("Completed_Levels", 0))
        {
            PlayerPrefs.SetInt("Completed_Levels", PlayerPrefs.GetInt("Completed_Levels", 0) + 1);

            //analitics
            OnLevelComplete();
        }

        currentFinishedLevels++;
        if (currentFinishedLevels > levelsToAd)
        {
            if (Time.time > lastAdCallTime + (5 * 60))
            {
                currentFinishedLevels = 0;
                YandexSDK.instance.ShowInterstitial();
                lastAdCallTime = Time.time;
            }

        }
    }


    public static void OnLevelComplete()
    {
#if UNITY_EDITOR
#else
        AnalyticsResult res = Analytics.CustomEvent(
            "LvlComplete", new Dictionary<string, object>
            {
                {"Number", PlayerPrefs.GetInt("Completed_Levels", 0)},
                {"Name", SceneManager.GetActiveScene().name}
            }
        );
        if (res == AnalyticsResult.AnalyticsDisabled || res == AnalyticsResult.NotInitialized || res == AnalyticsResult.UnsupportedPlatform)
            This.ShowMessageOnFailedSendAnalytics();    
#endif
    }
    public static void OnLevelEnd()
    {
#if UNITY_EDITOR
        Debug.Log(Time.timeSinceLevelLoad);
#else
        AnalyticsResult res = Analytics.CustomEvent(
        "LvlEnd", new Dictionary<string, object>
            {
                {"Number", SceneManager.GetActiveScene().buildIndex},
                {"Name", SceneManager.GetActiveScene().name},
                {"Time", Time.timeSinceLevelLoad },
                {"Try", 1 }
            }
        );
        if (res == AnalyticsResult.AnalyticsDisabled || res == AnalyticsResult.NotInitialized || res == AnalyticsResult.UnsupportedPlatform)
            This.ShowMessageOnFailedSendAnalytics();
#endif
    }

    public static void PlayCompleteAudio()
    {

        This.LevelCompleteAudio.GetComponent<AudioYB>().Play("big thunder drum");
    }
    public void ShowMessageOnFailedSendAnalytics()
    {
        if (PlayerPrefs.GetInt("NeverAsk", 0) == 0)
        {
            AnalyticsMessage.SetActive(true);
            AnalyticsMessage.GetComponent<Animation>().Play("AnalyticOpen");
        }
        else return;
    }
    public void HideMessagesOnFailedAnalitics(bool NeverShow)
    {
        if (NeverShow)
            PlayerPrefs.GetInt("NeverAsk", 1);
        AnalyticsMessage.GetComponent<Animation>().Play("AnalyticClose");
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
            needToOffExitButton = false;
            ShowHideSettings(false);
            lvlScreenAnimating = true;
            LevelScreenAnim.Play("LevelsOpen");
            LevelScreen.GetComponentInChildren<LevelsManager>().CreateLevelButtons();

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
            needToOffExitButton = false;
            ShowHideLevelsScreen(false);

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


    void CheckLocalization()
    {
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            SetRuLocalization();
        }
        else
        {
            SetEnLocalization();
        }
    }
    [SerializeField]
    Text[] texts;
    [SerializeField]
    string[] ru =
    {
    };
    [SerializeField]
    string[] en =
    {
    };
    public void SetRuLocalization()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = ru[i];
        }
    }
    public void SetEnLocalization()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = en[i];
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!isMuted)
            GetComponent<AudioListener>().enabled = hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        GetComponent<AudioListener>().enabled = pauseStatus;
    }

    public void SetQuality(int lvl)
    {
        QualitySettings.SetQualityLevel(lvl);
    }

}
