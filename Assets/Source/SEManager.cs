using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class SEManager : MonoBehaviour
{
    #region Singletone Definition
    static SEManager _instance;
    public static SEManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(SEManager)) as SEManager;
                if(_instance == null)
                {
                    return null;
                }
            }
            return _instance;
        }
    }
    #endregion

    public AudioSource BGM;
    public AudioSource audioSource;
    public AudioClip[] clips;
    public AudioClip[] bgms;

    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySE(string name)
    {
        for(int i = 0; i < clips.Length; i++)
        { 
            if (clips[i].name == name)
                audioSource.PlayOneShot(clips[i]);
        }
    }

    public void ChangeBGM(string name)
    {
        for (int i = 0; i < bgms.Length; i++)
        {
            if (bgms[i].name == name)
            {
                BGM.clip = bgms[i];
                BGM.Play();
            }
        }
    }
}
