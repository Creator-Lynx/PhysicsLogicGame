using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsBehaviors : MonoBehaviour
{
    public void MuteButtonChanger()
    {

    }

    public int level;

    public void StartLevel()
    {
        SceneManager.LoadScene(level);
    }
}
