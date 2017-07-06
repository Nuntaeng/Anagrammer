using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PassIntro : MonoBehaviour {

    public Animator anime;
	void Update () {
        if (Input.GetMouseButtonDown(0))
            GotoMain();
	}

    public void GotoMain()
    {
        anime.Play("Intro_GotoMenu");
    }

    public void GotoMainImmediate()
    {
        SceneManager.LoadScene(1);
    }
}
