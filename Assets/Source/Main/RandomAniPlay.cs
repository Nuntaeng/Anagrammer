using UnityEngine;
using System.Collections;

public class RandomAniPlay : MonoBehaviour
{
    public float randomDuration = 60f;
    public string stateName = "";
    public Animator anime;

    
    void Start()
    {
        Invoke("RandomPlay", Random.Range(0f, randomDuration));
    }

    void RandomPlay()
    {
        anime.Play(stateName);
        Invoke("RandomPlay", Random.Range(0f, randomDuration));
    }

    void PlayEffect(string name)
    {
        SEManager.Instance.PlaySE(name);
    }
}
