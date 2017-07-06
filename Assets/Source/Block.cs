using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Block : MonoBehaviour
{
    bool isHalfStatic = false;
    public Animator blockAnime;
    public Vector3 zeroPos;
    public Vector3 increaseSpace;
    public Text displayWord;
    public float animationSpeed = 1f;

    
    enum BlockMode { MERGE, MOVE, DESTROY, DESTROYIMMEDIATE }

    float deltaAccel = 4f;
    float deltaTime = 2f;
    Vector3 originPosition;
    Vector3 endPosition;
    BlockMode currentMode = BlockMode.MOVE;

    public void PlaySE(string name)
    {
        SEManager.Instance.PlaySE(name);
    }

    public bool isStatic
    {
        get
        {
            return
                blockAnime.GetBool("isStatic") ||
                isHalfStatic;
        }
    }

    public void ChangeMode()
    {
        if(deltaTime >= 0.9f)
        {
            isHalfStatic = !isHalfStatic;
            blockAnime.SetBool("isHalfStatic", isHalfStatic);
            Debug.Log("Half Static!");
        }
    }

    void OnEnable()
    {
        originPosition = Vector3.zero;
        endPosition = Vector3.zero;
        
        displayWord.text = WordDispatch.Instance.RandomCharacter;
        blockAnime.speed = animationSpeed;
        blockAnime.SetBool("isSpawned", true);
    }

    /// <summary>
    /// 블럭의 다음 위치를 설정해둡니다.
    /// </summary>
    public void SetPosition(int x, int y)
    {
        currentMode = BlockMode.MOVE;
        originPosition = this.transform.localPosition;
        endPosition = zeroPos + new Vector3(
            increaseSpace.x * x,
            -increaseSpace.y * y);
    }

    public void SetPositionImmediately(int x, int y)
    {
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
        ScoreMng.Instance.mergeCnt += 1;
        currentMode = BlockMode.MERGE;
        target.currentMode = BlockMode.DESTROYIMMEDIATE;

        string margeWord = this.displayWord.text + target.displayWord.text;
        if (margeWord.Length <= 18)
            this.displayWord.text = margeWord;
        else
            this.displayWord.text = target.displayWord.text;

        target.endPosition = originPosition;
        return this.displayWord.text;
    }

    /// <summary>   
    /// 블럭을 직접적으로 움직이기 시작합니다.
    /// </summary>
    public void Execute()
    {
        deltaTime = 0f;
    }
    
    public void StaticOn()
    {
        blockAnime.SetBool("isStatic", true);
    }

    void Update()
    {
        if(deltaTime < 1f)
        {
            deltaTime += Time.deltaTime * deltaAccel;
            this.transform.localPosition =
                Vector3.Lerp(originPosition, endPosition, deltaTime);
            if (deltaTime >= 0.99f)
            {
                switch (currentMode)
                {
                    case BlockMode.MERGE:
                        blockAnime.Play("Block_Merge");
                        break;
                    case BlockMode.DESTROY:
                        blockAnime.SetBool("isSpawned", false);
                        break;
                    case BlockMode.DESTROYIMMEDIATE:
                        this.gameObject.SetActive(false);
                        break;
                    case BlockMode.MOVE:
                        break;
                }
                originPosition = endPosition;
            }
        }
    }
}