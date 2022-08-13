using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode]
public class AudioStreamCash : MonoBehaviour
{
    public bool Cash;
    public bool DinamicCash;

    public List<Clip> infoList = new List<Clip>();
    public AudioClip this[int index] => infoList[index].Cash;

    static AudioStreamCash instance;

    private void Awake()
    {
        if (instance == null) instance = this; else Destroy(this);
#if UNITY_EDITOR
        Listen();
#endif
#if !UNITY_EDITOR
    foreach (var item in infoList) item.ClearCash();    
#endif
        if (Cash) LoadCash();
    }

    private void Listen()
    {
        infoList.Clear();
        LoadExt("mp3", AudioType.MPEG);
        LoadExt("wav", AudioType.WAV);
    }

    void LoadExt(string ext, AudioType type)
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] info = dir.GetFiles("*." + ext);
        foreach (var item in info)
            infoList.Add(new Clip(Application.streamingAssetsPath, item.Name, ext, type, Cash || DinamicCash));
    }

    void LoadCash()
    {
        foreach (var item in infoList)
           item.GetFile();
    }

    public static Clip Find(string name)
    {
        foreach (var item in instance.infoList)
            if (item.Name == name) return item;
        return null;
    }
}

[Serializable]
public class Clip
{
    public string Name;
    public string Path;
    public string Ext;
    public AudioType Type;
    public bool Cashing;
    public AudioClip Cash;

    public bool IsCashing => Cash != null;

    public Clip(string path, string name, string ext, AudioType type, bool cash)
    {
        Ext =  ext;
        Name = name.Substring(0, name.Length - Ext.Length - 1);
        Path = path;
        Type = type;
        Cashing = cash;
    }

    public async Task GetFile(Action<AudioClip> action = null)
    {
        if (IsCashing) action?.Invoke(Cash);
        else
        {
            string Url = Application.streamingAssetsPath + "/" + Name + "." + Ext;
            UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(Url, Type);
            Debug.Log("do");
            request.SendWebRequest();
            Debug.Log("posle");
            while (!request.isDone || Input.GetKeyDown(KeyCode.M))
            {
                await Task.Yield();
            }
            Debug.Log("done");
            AudioClip cash = DownloadHandlerAudioClip.GetContent(request);
            if (Cashing)
            {
                Cash = cash;
                Cash.name = Name;
            }
            action?.Invoke(cash);
        }
    }

   
    public void ClearCash() => Cash = null;
}