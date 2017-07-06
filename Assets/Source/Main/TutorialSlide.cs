using UnityEngine;
using System.Collections;

public class TutorialSlide : MonoBehaviour
{
    public GameBoard pauseList;
    float tick = 1f;
    public Transform img;
    int currentPage = 0;
    float stratch = 0f;
    float prevPos = 0f;




    void Awake()
    {
        this.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        currentPage = 0;
        img.transform.localPosition = new Vector3(1280f, 0f);
        tick = 0f;
        if(pauseList)
            pauseList.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (stratch > 0f)
                currentPage += 1;
            if (stratch < 0f)
                if(currentPage > 0)
                    currentPage -= 1;   
                
            tick = 0f;
        }

        if(Input.GetMouseButtonDown(0))
        {
            prevPos = Input.mousePosition.x;
        }

        if(Input.GetMouseButton(0))
        {
            stratch = prevPos - Input.mousePosition.x;
            img.transform.localPosition -= new Vector3(stratch, 0f);
            prevPos = Input.mousePosition.x; 
        }

        else
        {
            if(tick < 1f)
            {
                tick += Time.deltaTime * 2f;
                img.localPosition = Vector3.Lerp(img.localPosition, 
                    new Vector3(-(1280f * currentPage), 0f), tick);
            }
            else
            {
                if (currentPage >= 5)
                {
                    if (pauseList)
                        pauseList.enabled = true;
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
