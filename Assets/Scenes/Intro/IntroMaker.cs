using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroMaker : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        PlayerPrefs.SetInt("IsFirstStart", 0);
#endif
        if (PlayerPrefs.GetInt("IsFirstStart", 0) == 1)
        {
            LoadGameScene();
        }
    }
    public void LoadGameScene()
    {
        PlayerPrefs.SetInt("IsFirstStart", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
