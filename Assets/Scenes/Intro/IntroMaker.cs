using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMaker : MonoBehaviour
{
    [SerializeField]
    AudioYB introAudio, textAudio;
    AsyncOperation sceneLoading;
    private void Start()
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt("IsFirstStart", 1);
#endif

        if (PlayerPrefs.GetInt("IsFirstStart", 1) == 1)
        {
            GetComponent<Animator>().SetBool("splash", false);
        }
        sceneLoading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        sceneLoading.allowSceneActivation = false;
        DontDestroyOnLoad(textAudio.gameObject);
        Destroy(textAudio, 10f);
    }
    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("IsFirstStart", 0);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        sceneLoading.allowSceneActivation = true;
    }

    public void PlayTextSound()
    {
        textAudio.PlayOnShot("big thunder drum");
    }
    public void PlayIntroSound()
    {
        introAudio.PlayOnShot("Intro");
    }
}
