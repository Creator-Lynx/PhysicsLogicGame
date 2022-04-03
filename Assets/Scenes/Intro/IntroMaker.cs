using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMaker : MonoBehaviour
{
    [SerializeField]
    AudioSource introAudio, textAudio;

    private void Start()
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt("IsFirstStart", 1);
#endif
        if (PlayerPrefs.GetInt("IsFirstStart", 1) == 1)
        {
            GetComponent<Animator>().SetBool("splash", false);
        }



    }
    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("IsFirstStart", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayTextSound()
    {
        //textAudio.Play();
    }
    public void PlayIntroSound()
    {
        introAudio.Play();
    }
}
