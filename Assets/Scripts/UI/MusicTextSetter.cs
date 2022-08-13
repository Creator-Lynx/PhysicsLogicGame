using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTextSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        if (Application.systemLanguage == SystemLanguage.Russian)
        {
            GetComponent<Text>().text =
            "Музыка:  mint dragon;\nup to the hell;\nboat of pleasure;\n от Der Steppenfuchs";
        }
        else
        {
            GetComponent<Text>().text =
            "Music:  mint dragon;\nup to the hell;\nboat of pleasure;\nby Der Steppenfuchs";
        }

    }


}
