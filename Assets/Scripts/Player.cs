using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody _rig;

    public bool isVert, isHor;
    [SerializeField] bool invertMovement = false;
    int invertK = 1;
    [SerializeField] Vector3 speed;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        invertK = invertMovement ? -1 : 1;
        Vector3 horDirection =
            new Vector3(isHor ? -speed.x * Controll.Horizontal * invertK : 0f, 0f, 0f);
        Vector3 vertDirection =
            new Vector3(0f, 0f, isVert ? -speed.z * Controll.Vertical * invertK : 0f);

        bool enanbleHor = !Physics.Raycast(transform.position, horDirection, transform.localScale.x / 2);
        bool enanbleVert = !Physics.Raycast(transform.position, vertDirection, transform.localScale.z / 2);

        //for disable
        enanbleHor = true;
        enanbleVert = true;
        _rig.velocity += enanbleHor ? horDirection : Vector3.zero;
        _rig.velocity += enanbleVert ? vertDirection : Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
            if (SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
            {
                RGameManager.SetComleteLevel();
                RGameManager.OnLevelEnd();
                RGameManager.PlayCompleteAudio();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                RGameManager.SetComleteLevel();
                RGameManager.OnLevelEnd();
                RGameManager.PlayCompleteAudio();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}
