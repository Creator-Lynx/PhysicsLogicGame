using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChanger : MonoBehaviour
{
    AudioSource _audioSource;
    Animator _animator;
    [SerializeField] string[] triggerNames;
    [SerializeField] AudioClip[] music;

    enum State
    {
        legacy,
        pleasure,
        mint,
        hell
    }


    [SerializeField] State state = State.legacy;
    State prevState = State.legacy;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        changingThemes = PlayerPrefs.GetInt("ChangeTheme", 1) == 1;
        StartCoroutine(MusicEndCheck());
    }



    void SetTheme(bool wantToSetPause = false)
    {
        if (wantToSetPause)
        {
            prevState = state;
            state = State.legacy;
            _audioSource.Stop();
        }
        else
        if (prevState != State.legacy)
        {
            int r;
            do
            {
                r = UnityEngine.Random.Range(1, 4);
            }
            while (prevState == (State)r);
            state = (State)r;

        }
        else
        {
            state = (State)UnityEngine.Random.Range(1, 4);
        }

        _animator.SetTrigger(triggerNames[(int)state]);
        if (state != State.legacy)
        {
            _audioSource.clip = music[(int)state - 1];
            _audioSource.Play();
        }



    }

    IEnumerator MusicEndCheck()
    {
        if (changingThemes)
        {
            if (!_audioSource.isPlaying)
            {
                if (state == State.legacy)
                {
                    yield return new WaitForSeconds(30f);
                    SetTheme();
                }
                else
                {
                    yield return new WaitForSeconds(10f);
                    SetTheme(true);
                }

            }
            else
            {
                yield return new WaitForSeconds(5f);
            }
            StartCoroutine(MusicEndCheck());
        }

    }

    bool changingThemes;

    public void ToggleChangingThemes()
    {
        if (changingThemes)
        {
            changingThemes = false;
            PlayerPrefs.SetInt("ChangeTheme", 0);
            SetTheme(true);
        }
        else
        {
            changingThemes = true;
            PlayerPrefs.SetInt("ChangeTheme", 1);
            StartCoroutine(MusicEndCheck());
        }
    }
}
