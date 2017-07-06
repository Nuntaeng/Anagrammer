using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Preloader : MonoBehaviour {
    

    void Awake()
    {
        WordDispatch.Instance.LoadWordData();
    }
}
