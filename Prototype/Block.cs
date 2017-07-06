using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    public Vector3 zeroPos;
    public Vector3 increaseSpace;
    public Text displayWord;


    List<Vector2> prevPos = new List<Vector2>();
    List<string> prevWord = new List<string>();
    List<Block> prevBlock = new List<Block>();

    float deltaTime = 2f;
    Vector3 originPosition;
    Vector3 endPosition;


    
    void OnEnable()
    {
        displayWord.text = WordDispatch.Instance.GetRandomWordPiece();
    }


    /// <summary>
    /// 블럭의 다음 위치를 설정해둡니다.
    /// </summary>
    public void SetPosition(int x, int y)
    {
        this.gameObject.name = "Block (" + x + "/" + y + ")";
        originPosition = this.transform.localPosition;
        endPosition = zeroPos + new Vector3(
            increaseSpace.x * x,
            -increaseSpace.y * y);
    }

    public void SetPositionImmediately(int x, int y)
    {
        this.gameObject.name = "Block (" + x + "/" + y + ")";
        this.transform.localPosition = 
            zeroPos + new Vector3(
            increaseSpace.x * x,
            -increaseSpace.y * y);
        originPosition = this.transform.localPosition;
        endPosition = this.transform.localPosition;
    }
    
    /// <summary>
    /// 블럭과 블럭을 합칩니다.
    /// </summary>
    /// <param name="target">합쳐지기 전의 블럭</param>
    /// <returns>합쳐진 문자열 (정답 판별용)</returns>
    public string Incorporation(Block target)
    {
        this.displayWord.text += target.displayWord.text;
        if(this.displayWord.text.Equals(WordDispatch.Instance.CurrentWordData[(int)WordDispatch.WordAccess.HURIGANA]))

        target.endPosition = originPosition;
        Destroy(target.gameObject);
        return this.displayWord.text;
    }

    /// <summary>   
    /// 블럭을 직접적으로 움직이기 시작합니다.
    /// </summary>
    public void Execute()
    {
        deltaTime = 0f;
    }

    public void SetWord(string word)
    {

    }

    public void PrevPosLogging(int x, int y)
    {
        prevWord.Add(displayWord.text);
        prevPos.Add(new Vector2(x, y));
    }

    public void Recover()
    {
        if (prevPos.Count == 0)
            Destroy(this.gameObject);
        this.SetPosition((int)prevPos[prevPos.Count - 1].x, (int)prevPos[prevPos.Count - 1].y);
        this.displayWord.text = prevWord[prevWord.Count - 1];
        prevPos.RemoveAt(prevPos.Count - 1);
        prevWord.RemoveAt(prevWord.Count - 1);
    }

    void Update()
    {
        if(deltaTime < 1f)
        {
            deltaTime += Time.deltaTime * 4f;
            this.transform.localPosition =
                Vector3.Lerp(originPosition, endPosition, deltaTime);
        }
    }
}