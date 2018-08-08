using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConfirmationPanel : MonoBehaviour, UIUpdatable {

    public AuditionScreen auditionScreen;
    public List<AuditionSummary> auditionSummaries;

    private bool isVisible = false;
    private Vector3 hiddenPos = new Vector3(50, -10.3f, 0);
    private Vector3 visiblePos = new Vector3(50, 0, 0);
    private float t;
    public float lerpTime;
    private bool lerping = false;
    float yPos = -10.3f;

    public Transform lerpObject;

    private Collider2D collider;

    //Input variables
    private Vector3 mousePos;
    private Vector3 prevMousePos;

    //Public Scroll variables
    public float offset;
    public bool isHorizontal;
    public float scrollMomentumScale;
    public float scrollSpeedScale;
    public float scrollDragScale;

    //Private scroll variables
    private bool scrolling;
    private float force;
    private float objectSpacing;
    public float maxOffset;
    private Vector3 startPos;
    private Transform myTransform;
    private int ActivePicturesInArray;

    private void Update()
    {    
        if(myTransform == null)
        {
            myTransform = this.transform;
            startPos = myTransform.localPosition;
        }

        if(collider = null)
        {
            collider = this.GetComponent<Collider2D>();
        }

        //Visibility code
        /*if (lerping)
        {
            t += Time.deltaTime;
            if (t >= lerpTime)
            {
                t = lerpTime;
                lerping = false;
            }
            if (isVisible)
            {
                lerpObject.position = new Vector3(50, Mathf.Lerp(hiddenPos.y, visiblePos.y,  t / lerpTime), 0);
            }
            else
            {
                lerpObject.position = new Vector3(50, Mathf.Lerp(visiblePos.y, hiddenPos.y, t / lerpTime), 0);
            }
        }*/
        if(isVisible)
        {
            //DOTween.To(() => yPos, x => yPos = x, 0f, 0.5);
            lerpObject.GetComponent<RectTransform>().DOLocalMoveY(0, 0.5f);
        }

        //Scroll code
        if (!scrolling)
        {
            //Calculate force
            force *= scrollDragScale;
            offset += force;
        }


        //Update position
        myTransform.localPosition = new Vector3(myTransform.localPosition.x, startPos.y + offset/myTransform.lossyScale.y, myTransform.localPosition.z);
    }

    private void UpdateMaxOffset()
    {
        //Calculate the max size
        ActivePicturesInArray = 0;
        int totalCounted = 0;
        for (int i = 0; i < auditionSummaries.Count; i++)
        {
            if (!auditionSummaries[i].IsEmpty())
            {
                ActivePicturesInArray++;
            }
            totalCounted++;
        }

        maxOffset = ActivePicturesInArray * objectSpacing - (objectSpacing / 2);
        maxOffset /= myTransform.lossyScale.y;
    }

    private void OnMouseUp()
    {
        //Calculate the slide effect
        scrolling = false;
        force = (mousePos.y - prevMousePos.y) * scrollMomentumScale;

    }

    private void OnMouseDown()
    {
        scrolling = true;
        mousePos = Input.mousePosition;
        prevMousePos = Input.mousePosition;
        Debug.Log("conf screen clicked");
    }

    private void OnMouseDrag()
    {
        prevMousePos = mousePos;
        mousePos = Input.mousePosition;

        float xChange = 0;
        float yChange = 0;

        xChange = mousePos.x - prevMousePos.x;
        yChange = mousePos.y - prevMousePos.y;

        offset += yChange * scrollSpeedScale;

        if (offset < -objectSpacing / 2)
        {
            offset = -objectSpacing / 2;
        }
        else if (offset > maxOffset)
        {
            offset = maxOffset;
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
        UpdateMaxOffset();
        if (arrayNum > 1)
        {
            if (objectSpacing == 0)
            {
                objectSpacing = auditionSummaries[0].transform.position.y - auditionSummaries[1].transform.position.y;
            }
        }
    }

    [ContextMenu("Show transform pos")]
    public void ShowTransformPos()
    {
        Debug.Log(lerpObject.transform.position);
    }

    public void SetCollision(bool collisionOn)
    {
        if(collider == null)
        {
            collider = this.GetComponent<Collider2D>();
        }
        if (collisionOn)
        {
            Debug.Log("Collider enabled");
            collider.enabled = true;
        }
        else
        {
            Debug.Log("Collider disabled");
            collider.enabled = false;
        }
    }
}
