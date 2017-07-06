using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


#if UNITY_ANDROID

public class PlayIntro : MonoBehaviour {

	// Use this for initialization
	void OnEnable ()
    {
        Debug.Log("Intro play!");
        Handheld.PlayFullScreenMovie("Intro (1).wmv", Color.black, FullScreenMovieControlMode.CancelOnInput);
        Debug.Log("Done!");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            SceneManager.LoadScene(1);
    }
}

#endif