using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class MenuControl : MonoBehaviour
{
    public GameObject htp;

    public void PressStart()
    {
        SEManager.Instance.ChangeBGM("GameBGM");
        SEManager.Instance.PlaySE("ButtonPress_Main");
        SceneManager.LoadScene(2);
    }

    public void HTP()
    {
        SEManager.Instance.PlaySE("ButtonPress_Main");
        htp.SetActive(true);
    }

    public void Leaderboard()
    {
        SEManager.Instance.PlaySE("ButtonPress_Main");
        PlayGamesPlatform.Instance.ShowLeaderboardUI("Cfji293fjsie_QA");
    }

    public void Exit()
    {
        SEManager.Instance.PlaySE("ButtonPress_Main");
        Application.Quit();
    }

    public void ScoreTest()
    {
        ScoreMng.Instance.AddScore(1000);
    }

    public void GoMain()
    {
        SEManager.Instance.PlaySE("ButtonPress_Main");
        SceneManager.LoadScene(1);
    }
}
