using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;




public class ScoreMng
{
    #region This class is based on Singletone
    ScoreMng() { }
    static ScoreMng _instance = null;
    public static ScoreMng Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ScoreMng();
            return _instance;
        }
    }
    #endregion


    /// <summary>
    /// score of current game
    /// </summary>
    public int Score { get { return currentScore; } }
    int currentScore = 0;
    Text scoreBoard = null;
    public int mergeCnt = 0;

    
    // ===================================================

    public void Initialize(Text board)
    {
        scoreBoard = board;
        scoreBoard.text = currentScore.ToString();
    }

    public void AddScore(int score)
    {
        currentScore += score;
        scoreBoard.text = currentScore.ToString();
    }

    public void SpendScore(int score)
    {
        currentScore -= score;
        scoreBoard.text = currentScore.ToString();
    }
}

public class ScoreBoard : MonoBehaviour
{
    public Text scoreBoard;

    void Start()
    {
        ScoreMng.Instance.Initialize(scoreBoard);
    }

    void OnDisable()
    {
        SEManager.Instance.ChangeBGM("MainBGM");
        SEManager.Instance.PlaySE("SceneChange");
        Social.ReportScore(ScoreMng.Instance.Score, " CgkI4K6PvNgBEAIQAQ", (bool success) => {
            // handle success or failure
        });
    }
}