using UnityEngine;
using System.Collections;

public class MapScrolling : MonoBehaviour
{
    public RectTransform myTrans;
    public float moveSpeed = 1f;
    float boarder = 0f;


    void Awake()
    {
        boarder = 765f + myTrans.rect.width / 2;
    }

    void Update()
    {
        if (myTrans.localPosition.x < boarder)
            myTrans.localPosition += Vector3.right * moveSpeed * Time.smoothDeltaTime;
        else
            myTrans.localPosition = (Vector3.left * boarder) + (Vector3.up * myTrans.transform.localPosition.y);
    }
}
