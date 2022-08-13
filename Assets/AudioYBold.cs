// ������� �����  Asset/StreamingAssets. � ��� ������� ����� ����� � ������������ .mp3,.wav
// ������ ������ �� ������ ������, ����� ��� ����� �������������� ����� ���� ������
// AudioYB.Play("��� ����� ��� ����������") 
// AudioYB.Play()
// AudioYB.PlayOneShot("��� ����� ��� ����������")
// AudioYB.Volume(0-1 float)
// AudioYB.IsPlaying(get; bool)
// AudioYB.AudioClip Clip(set;get; AudioClip) 
// AudioYB.Stop() 
// AudioYB.Pause()
// AudioYB.Enabled(get;set; bool) 
// �� � �� ��������)) ���� ���� ������� ����������  yoomoney 410016006657394 ��� 6769 0700 0745 0835
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
namespace OldAudioYB
{
    [RequireComponent(typeof(AudioSource))]
    [ExecuteInEditMode]
    public class AudioYBold : MonoBehaviour
    {
        [Tooltip("����������� ����� ����� ��� ������")]
        public bool Cash;
        [Tooltip("������������ ����������� ����� ��� ������������")]
        public bool DinamicCash;
        public List<Clip> infoList = new List<Clip>();
        AudioSource audioSource;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
#if UNITY_EDITOR
            Listen();
#endif
#if !UNITY_EDITOR
    foreach (var item in infoList) item.ClearCash();    
#endif
            if (Cash) LoadCach();
        }
        Action play;
        bool load;
        void PlayAfter()
        {
            audioSource.Play();
        }
        public void Play(string nameClip = "")
        {
            if (nameClip == "")
            {
                if (load) PlayAfter();
                else play = PlayAfter;
            }
            else
                StartCoroutine(Find(nameClip).GetFile(PlayCar));
        }
        public void PlayOneShot(string nameClip)
        {
            StartCoroutine(Find(nameClip).GetFile(PlayOneShotCar));
        }
        void SetClip(string nameClip)
        {
            load = false;
            StartCoroutine(Find(nameClip).GetFile(SetClipCar));
        }

        private void PlayCar(Clip clip)
        {
            audioSource.clip = clip.audioClip;
            audioSource.Play();
            if (!Cash && !DinamicCash) clip.ClearCash();
        }
        private void PlayOneShotCar(Clip clip)
        {
            audioSource.PlayOneShot(clip.audioClip);
            if (!Cash && !DinamicCash) clip.ClearCash();
        }
        private void SetClipCar(Clip clip)
        {
            audioSource.clip = clip.audioClip;
            load = true;
            play?.Invoke();
            play = null;
            if (!Cash && !DinamicCash) clip.ClearCash();
        }

        #region


        public void Stop()
        {
            audioSource.Stop();
        }
        public void Pause()
        {
            audioSource.Pause();
        }
        public void UnPause()
        {
            audioSource.UnPause();
        }
        public bool isPlaying { get => audioSource.isPlaying; }
        public float volume { get => audioSource.volume; set => audioSource.volume = value; }
        public string clip { set => SetClip(value); }
        public bool Enabled { get => audioSource.enabled; set => audioSource.enabled = value; }
        public float pitch { get => audioSource.pitch; set => audioSource.pitch = value; }
        #endregion
        Clip Find(string name)
        {
            if (name == null)
            {
                audioSource.clip = null;
                return null;
            }
            foreach (var item in infoList)
                if (item.Name == name) return item;
            Debug.LogError("�� ���������� ��� " + name);
            return null;
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
                infoList.Add(new Clip(item.Name, ext, type));
        }
        void LoadCach()
        {
            foreach (var item in infoList)
            {
                StartCoroutine(item.GetFile());
            }
        }
    }
    [Serializable]
    public class Clip
    {
        public string Name;
        public string Ext;
        public AudioType Type;
        public AudioClip audioClip;
        public Clip(string name, string ext, AudioType type)
        {
            Ext = "." + ext;
            Name = name.Substring(0, name.Length - Ext.Length);
            Type = type;
        }
        public IEnumerator GetFile(Action<Clip> action = null)
        {
            if (audioClip == null)
            {
                string Url = Path.Combine(Application.streamingAssetsPath, Name + Ext);
                UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(Url, Type);
                yield return request.SendWebRequest();
                audioClip = DownloadHandlerAudioClip.GetContent(request);
                audioClip.name = Name;
            }
            action?.Invoke(this);
        }
        public void ClearCash()
        {
            audioClip = null;
        }
    }
}
