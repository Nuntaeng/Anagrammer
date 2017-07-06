using UnityEngine;
using System.Collections;

public class SubAnimeDispatch : MonoBehaviour
{
    public Animator[] animations;
    public float speed = 1f;
    public float RandomDuration = 5f;


    void Awake()
    {
        foreach (var anime in animations)
            anime.speed = this.speed;
        InvokeRepeating("Animationing", 0f, Random.Range(0f, RandomDuration));
    }

    void Animationing()
    {
        int random = Random.Range(0, 2);
        foreach (var anime in animations)
            anime.SetInteger("idleNum", random);
    }
    
    public void Correct()
    {
        CancelInvoke("Animationing");
        foreach(var anime in animations)
            anime.Play(anime.gameObject.name + "_Right");

        InvokeRepeating("Animationing", 3f, Random.Range(0f, RandomDuration));
    }
}
