using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody _rig;
    [SerializeField] bool isVert, isHor;
    [SerializeField] Vector3 speed;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {

        _rig.velocity += new Vector3(isHor ? -speed.x * Controll.Horizontal : 0f, _rig.velocity.y, isVert ? -speed.z * Controll.Vertical : 0f);
        //_rig.AddForce(new Vector3(isHor ? -speed.x * Controll.Horizontal: 0f, _rig.velocity.y, isVert ? -speed.z * Controll.Vertical : 0f));

    }
    private void OnTriggerEnter(Collider other)
    {
        //если проходим уровень до среднего, играть можено без покупки, далее можно играть только с подтвержденной покупкой
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            if (true)//SceneManager.GetActiveScene().buildIndex <= (PurchaseManager.LevelsCount / 2)){
                if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
                {
                    RGameManager.SetComleteLevel();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

                }
                else
                {
                    RGameManager.SetComleteLevel();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                }

        }
        /*else{
            if(PlayerPrefs.GetInt("IsBought") == 1 PurchaseManager.CheckBuyState("get_levels")){
                if(SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1){
                    PurchaseManager.SetComleteLevel();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }    
                else{
                    PurchaseManager.SetComleteLevel();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

            }
            else{
                GameObject.Find("Main Camera").GetComponent<PurchaseManager>().ShowBuyScreen();
            }
        }

    }*/
    }

}
