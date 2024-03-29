﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class Player : MonoCache
{
    Rigidbody _rig;
    RaycastCubesChecker checker;

    public bool isVert, isHor;
    [SerializeField] bool invertMovement = false;
    int invertK = 1;
    [SerializeField] Vector3 speed;
    [SerializeField]
    float forceUpFactor = 20f, speedUpFactor = 20f;
    float fixedFactor;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        checker = GetComponent<RaycastCubesChecker>();
        AddFixedUpdate();
        fixedFactor = Time.fixedDeltaTime / 0.02f;
    }
    void OnDestroy()
    {
        RemoveFixedUpdate();
    }


    public override void OnFixedTick()
    {
#if UNITY_EDITOR
        fixedFactor = Time.fixedDeltaTime / 0.02f;
#endif
        invertK = invertMovement ? -1 : 1;
        float hor = isHor ? -speed.x * Controll.Horizontal * invertK : 0f;
        float ver = isVert ? -speed.z * Controll.Vertical * invertK : 0f;
        /*if (hor > 0)
        {
            if (checker.CastRightRays()) hor = 0;
        }
        else
        {
            if (checker.CastLeftRays()) hor = 0;
        }
        if (ver > 0)
        {
            if (checker.CastForwardRays()) ver = 0;
        }
        else
        {
            if (checker.CastBackwardRays()) ver = 0;
        }*/

        _rig.velocity += new Vector3(hor, 0f, ver) * fixedFactor;
        //_rig.velocity = new Vector3(hor, 0f, ver) * fixedFactor * speedUpFactor;
        //_rig.AddForce(new Vector3(hor, 0f, ver) * fixedFactor * forceUpFactor, ForceMode.Force);
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
