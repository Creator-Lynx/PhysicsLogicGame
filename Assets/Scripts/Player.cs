using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        _rig.velocity += new Vector3(isHor ? -speed.x * Controll.Horizontal * invertK : 0f, _rig.velocity.y,
         isVert ? -speed.z * Controll.Vertical * invertK : 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.CompareTag("Player") && other.CompareTag("Finish"))
        {
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
    }

}
