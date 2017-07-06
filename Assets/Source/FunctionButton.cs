using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FunctionButton : MonoBehaviour
{
    public int coastScore = 0;
    public Animator buttonAnimator;
    public UnityEvent executeEvent;



    void Awake()
    {
        if (executeEvent == null)
            executeEvent = new UnityEvent();
        InvokeRepeating("CheckEnable", 0f, 1f);  
    }

    public void Click()
    {
        if (!buttonAnimator.GetBool("isDisable"))
        {
            SEManager.Instance.PlaySE("ButtonPress_Ingame");
            ScoreMng.Instance.SpendScore(coastScore);
            executeEvent.Invoke();
        } 
    }

    void CheckEnable()
    {
        if (ScoreMng.Instance.Score < coastScore)
            buttonAnimator.SetBool("isDisable", true);
        else
            buttonAnimator.SetBool("isDisable", false);
    }
}
