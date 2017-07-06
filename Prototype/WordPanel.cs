using UnityEngine;
using UnityEngine.UI;
using System.Collections;




public class WordDispatch
{
    #region Singletone Def
    static WordDispatch _instance;
    protected WordDispatch() { }
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

    public enum WordAccess { JPWORD = 0, HURIGANA, MEANING }
    public string[] CurrentWordData { get { return _currentWord; } }
    string[] _currentWord = new string[] { "揺れる", "ゆれる", "흔들리다" };

    int _correctCnt = 0;
    int _currentLevel = 0;

   
    /// <summary>
    /// 새로운 문제를 출제할 수 있도록 요구합니다.
    /// </summary>
    public void NewQuestion()
    {
        // 난이도 조정
        _correctCnt += 1;
        if (_correctCnt >= 5)
        {
            if (_currentLevel > 2)
                _currentLevel = 0;
            else
                _currentLevel += 1;
            _correctCnt = 0;
        }

        // PHP로 새로운 코드 로드하는 소스 후첨할 것
    }

    public string GetRandomWordPiece()
    {
        string ret = "";
        ret += _currentWord[(int)WordAccess.HURIGANA][Random.Range(0, _currentWord[(int)WordAccess.HURIGANA].Length)];
        return ret;
    }
}

public class WordPanel : MonoBehaviour
{
    public Text currentWord;
    public Text meaning;
}