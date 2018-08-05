using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationPanel : MonoBehaviour, UIUpdatable {

    public AuditionScreen auditionScreen;
    public List<AuditionSummary> auditionSummaries;

    private bool isVisible = false;
    private Vector3 hiddenPos = new Vector3(50, -10.3f, 0);
    private Vector3 visiblePos = new Vector3(50, 0, 0);
    private float t;
    public float lerpTime;
    private bool lerping = false;

    private void Update()
    {    
        if (lerping)
        {
            t += Time.deltaTime;
            if (t >= lerpTime)
            {
                t = lerpTime;
                lerping = false;
            }
            if (isVisible)
            {
                this.transform.position = new Vector3(50, Mathf.Lerp(hiddenPos.y, visiblePos.y,  t / lerpTime), 0);
            }
            else
            {
                this.transform.position = new Vector3(50, Mathf.Lerp(visiblePos.y, hiddenPos.y, t / lerpTime), 0);
            }
        }
    }

    public void ShowConfirmationScreen()
    {
        if(!isVisible)
        {
            isVisible = true;
            lerping = true;
            t = 0;
        }
    }

    public void HideConfirmationScreen()
    {
        if(isVisible)
        {
            isVisible = false;
            lerping = true;
            t = 0;
        }
    }

    public bool IsVisble()
    {
        return isVisible;
    }

    public void UpdateUI()
    {
        List<AuditionScreen.Audition> auditionList = auditionScreen.getAuditionsList();
        int arrayNum = 0;
        int auditionNum = 0;
        foreach(AuditionScreen.Audition a in auditionList)
        {
            if(a.actor != null)     //Auditions will always have a movie but only have an actor if one is being sent on the audition.
            {
                auditionSummaries[arrayNum].gameObject.SetActive(true);
                auditionSummaries[arrayNum].setAudition(a);
                auditionSummaries[arrayNum].setArrayNum(auditionNum);
                arrayNum++;
            }
            auditionNum++;
        }
        for(int i = arrayNum; i < auditionSummaries.Count; i++)
        {
            auditionSummaries[i].gameObject.SetActive(false);
        }
    }

    [ContextMenu("Show transform pos")]
    public void ShowTransformPos()
    {
        Debug.Log(this.transform.position);
    }
}
