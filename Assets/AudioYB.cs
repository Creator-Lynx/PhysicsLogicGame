using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioYB : MonoBehaviour
{
    AudioSource source;
    bool load;
    bool play;

    private void Awake() => source = GetComponent<AudioSource>();

    void PlayAfter()
    {
        play = false;
        source.Play();
    }

    void LoadAfter(AudioClip clip)
    {
        source.clip = clip;
        load = true;
        if (play) PlayAfter();
    }

    void LoadShotAfter(AudioClip clip) => source.PlayOneShot(clip);

    public void Play()
    {
        if (load) source.Play();
        else play = true;
    }

    public void Play(string name)
    {
        Clip clip = AudioStreamCash.Find(name);
        if (clip == null) return;
        load = false;
        clip.GetFile(LoadAfter);
        play = true;
    }

    public void PlayOnShot(string name)
    {
        Clip clip = AudioStreamCash.Find(name);
        if (clip == null) return;
        clip.GetFile(LoadShotAfter);
    }

    public void Pause() => source.Pause();
    public void UnPause() => source.UnPause();
    public string clip { get => source.clip.name; set => source.clip = AudioStreamCash.Find(value).Cash; }

}

