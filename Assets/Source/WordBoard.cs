using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


public class WordDispatch
{ 
    #region This Class is Based on Singletone
    WordDispatch() { }
    static WordDispatch _instance = null;
    public static WordDispatch Instance
    {
        get
        {
            if (_instance == null)
                _instance = new WordDispatch();
            return _instance;
        }
    }
    #endregion


    class Dics
    {
        public class DicLevel
        {
            public class Word
            {
                public string word;
                public string hurigana;
                public string meaning;
            }
            public List<Word> questions;
        }
        public List<DicLevel> levels;
    }

    public Text question;
    public Text answer;
    public Text rightHuri;
    public Text rightAnswer;

    Dics dic;
    Dics.DicLevel.Word currentQuestion;
    public string Hurigana { get { return currentQuestion.hurigana; } }
    SubAnimeDispatch scoreEffect;
    GameBoard boardCopy;

    int questionNum = 0;
    int difficulty = 0;
    int passQuesCnt = 0;
    int pickLimit = 0;
    


    public void Initialize(Text q, Text a, Text ra, Text rs, SubAnimeDispatch s, GameBoard b)
    {
        question = q;
        answer = a;
        boardCopy = b;
        scoreEffect = s;
        rightAnswer = rs;
        rightHuri = ra;
        //difficulty = PlayerPrefs.GetInt("Difficulty");
        //passQuesCnt = PlayerPrefs.GetInt("PassQuestionCount");
        //pickLimit = PlayerPrefs.GetInt("PickLimit");
    }

    public void NewWord()
    {
        questionNum = Random.Range(0, 29);
        //while (PlayerPrefs.GetInt("QUESTION_" + difficulty + questionNum) > pickLimit)
        //    questionNum = Random.Range(0, 10);

        currentQuestion = dic.levels[difficulty].questions[questionNum];

        question.text = currentQuestion.word;
        answer.text = currentQuestion.meaning;
    }

    public void Correct()
    {
        rightHuri.text = currentQuestion.hurigana;
        rightAnswer.text = currentQuestion.meaning;
        scoreEffect.Correct();
        ScoreMng.Instance.AddScore(1000);
        NewWord();
        //// 난이도 조정
        //PlayerPrefs.SetInt("QUESTION_" + difficulty + questionNum,
        //    PlayerPrefs.GetInt("QUESTION_" + difficulty + questionNum) + 1);
        //if (passQuesCnt % 10 == 0)
        //    pickLimit += 1;
        //if (passQuesCnt % 50 == 0)
        //{
        //    pickLimit = 0;
        //    if (difficulty < 3)
        //        difficulty += 1;
        //    else
        //    {
        //        difficulty = 0;
        //        pickLimit = 5;
        //    }
        //}
        

        boardCopy.Correct();
    }

    public void LoadWordData()
    {
        StaticCoroutine.DoCoroutine(Respond());
    }

    IEnumerator Respond()
    {
        WWW www = new WWW("https://nuntaeng.github.io/wordlist.txt");
        yield return www;
        dic = JsonFx.Json.JsonReader.Deserialize<Dics>(www.text);

        yield return null;
    }

    /// <summary>
    /// 문제의 후리가나를 랜덤하게 한개 뽑아온다.
    /// </summary>
    public string RandomCharacter
    {
        get
        {
            try
            {
                string ret = "";
                ret += currentQuestion.hurigana[Random.Range(0, currentQuestion.hurigana.Length)];
                return ret;
            }
            catch
            {
                return "";
            }
        }
    }

    /// <summary>
    /// PlayerPrefs에 백업되야 할 정보들을 저장한다.
    /// </summary>
    public void Save()
    {
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetInt("PassQuestionCount", passQuesCnt);
        PlayerPrefs.SetInt("PickLimit", pickLimit);
    }
}


public class WordBoard : MonoBehaviour
{
    public Text question;
    public Text answer;
    public Text rightHuri;
    public Text rightAnswer;

    public SubAnimeDispatch scoreEffect;
    public GameBoard boardCopy;

    void Awake()
    {
        WordDispatch.Instance.Initialize(question, answer, rightHuri, rightAnswer, scoreEffect, boardCopy);
    }

    void OnEnable()
    {
        WordDispatch.Instance.NewWord();
    }
    
}