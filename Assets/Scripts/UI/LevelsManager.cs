using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelsManager : MonoBehaviour
{

    [SerializeField] Transform grid;
    [SerializeField] GameObject levelButton;

    bool showAllLevelsFlag = false;
    [SerializeField]
    int touchCounter = 0;
    [SerializeField] int firstLevelInLayout = 0;
    public void NextPage()
    {
        if (firstLevelInLayout != 0 && firstLevelInLayout + 31 < RGameManager.LevelsCount)
        {
            Debug.Log(PlayerPrefs.GetInt("Completed_Levels", 0));
            if (PlayerPrefs.GetInt("Completed_Levels", 0) > firstLevelInLayout + 31 || showAllLevelsFlag)
                CreateLevelButtons(firstLevelInLayout + 32);
            else
            {
                touchCounter++;
                if (touchCounter >= 10) showAllLevelsFlag = true;
            }
        }
    }

    public void PrevPage()
    {
        if (firstLevelInLayout != 0 && firstLevelInLayout - 32 > 0)
        {
            CreateLevelButtons(firstLevelInLayout - 32, firstLevelInLayout - 1);
        }
    }

    public void CreateLevelButtons(int firstLevel = 1, int lastLevel = 0)
    {
        int completedLevels = PlayerPrefs.GetInt("Completed_Levels", 0);
        for (int i = 0; i < grid.childCount; i++)
        {
            Destroy(grid.GetChild(i).gameObject);
        }
        if (lastLevel == 0) lastLevel = RGameManager.LevelsCount;
        firstLevelInLayout = firstLevel;
        if (lastLevel - firstLevel >= 32)
        {
            lastLevel = firstLevel + 31;
        }
        for (int i = firstLevel; i <= lastLevel; i++)
        {
            GameObject b = Instantiate(levelButton, Vector3.zero, Quaternion.identity, grid);
            b.transform.localEulerAngles = Vector3.zero;
            b.transform.localScale = Vector3.one * 2;
            b.GetComponent<ButtonsBehaviors>().level = i;
            b.GetComponent<Text>().text = i.ToString();
            if (i > completedLevels + 1) b.GetComponent<Button>().interactable = false;
        }
    }


}
