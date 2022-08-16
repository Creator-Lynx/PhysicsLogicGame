using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeChanger : MonoBehaviour
{
    AudioSource _audioSource;
    AudioYB _audioYB;
    Animator _animator;
    [SerializeField] string[] triggerNames;
    [SerializeField] string[] music;
    [SerializeField] float[] musicLength;

    [SerializeField]
    bool useSideMusicPlayer = true;
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
        _audioYB = GetComponent<AudioYB>();
        changingThemes = PlayerPrefs.GetInt("ChangeTheme", 1) == 1;
        StartCoroutine(MusicEndCheck());
    }



    void SetTheme(bool wantToSetPause = false)
    {
        if (wantToSetPause)
        {
            prevState = state;
            state = State.legacy;
            StopMusic();
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
            PlayMusic(music[(int)state - 1]);

        }



    }

    IEnumerator MusicEndCheck()
    {
        if (changingThemes)
        {
            if (state == State.legacy)
            {

                yield return new WaitForSeconds(30f);
                SetTheme();
                yield return new WaitForSeconds(0.5f);
                yield return new WaitForSeconds(musicLength[(int)state - 1]);
            }
            else
            {
                SetTheme(true);
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
            StopAllCoroutines();
        }
        else
        {
            changingThemes = true;
            PlayerPrefs.SetInt("ChangeTheme", 1);
            StartCoroutine(MusicEndCheck());
        }
    }

    void PlayMusic(string clip)
    {
        if (useSideMusicPlayer)
        {
            _audioYB.Play(clip);
        }
        else
        {
            //_audioSource.clip = .;
            _audioSource.Play();
        }
    }
    void StopMusic()
    {
        if (useSideMusicPlayer)
        {
            _audioYB.Pause();
        }
        else
        {
            _audioSource.Stop();
        }
    }
    bool CheckMusicIsPlaying()
    {
        if (useSideMusicPlayer)
        {
            return false;
        }
        else
        {
            return _audioSource.isPlaying;
        }

    }
}
