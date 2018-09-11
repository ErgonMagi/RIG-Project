using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ConfirmationPanel : MonoBehaviour, UIUpdatable, IPointerDownHandler, IPointerUpHandler {

    public AuditionScreen auditionScreen;
    public List<AuditionSummary> auditionSummaries;

    private bool isVisible = false;
    private Vector3 hiddenPos = new Vector3(50, -10.3f, 0);
    private Vector3 visiblePos = new Vector3(50, 0, 0);
    private float t;
    public float lerpTime;
    private bool lerping = false;
    float yPos = -10.3f;

    private Tweener tween;

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
            startPos = lerpObject.localPosition;
        }

        if(collider == null)
        {
            collider = this.GetComponent<Collider2D>();
            collider.enabled = false;
        }

        //Scroll code
        if (!scrolling)
        {
            //Calculate force
            force *= scrollDragScale;
            offset += force;
        }


        //Update position
        myTransform.localPosition = new Vector3(myTransform.localPosition.x, offset, myTransform.localPosition.z);
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
        maxOffset *= myTransform.localScale.y;
    }

    public void OnPointerUp(PointerEventData pointer)
    {
        //Calculate the slide effect
        scrolling = false;
        force = (mousePos.y - prevMousePos.y) * scrollMomentumScale;

    }

    public void OnPointerDown(PointerEventData pointer)
    {
        scrolling = true;
        mousePos = Input.mousePosition;
        prevMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (scrolling)
        {
            prevMousePos = mousePos;
            mousePos = Input.mousePosition;

            float xChange = 0;
            float yChange = 0;

            xChange = mousePos.x - prevMousePos.x;
            yChange = mousePos.y - prevMousePos.y;

            offset += yChange;


            if (offset < -objectSpacing / 2)
            {
                offset = -objectSpacing / 2;
            }
            else if (offset > maxOffset)
            {
                offset = maxOffset;
            }
        }
    }

    public void ShowConfirmationScreen()
    {
        tween = lerpObject.GetComponent<RectTransform>().DOLocalMoveY(0, 1f).SetEase(Ease.OutBack);
        isVisible = true;
        SetCollision(true);
    }

    public void HideConfirmationScreen()
    {
        tween = lerpObject.GetComponent<RectTransform>().DOLocalMoveY(startPos.y, 1f).SetEase(Ease.OutBack);
        isVisible = false;
        SetCollision(false);
    }

    public bool IsVisible()
    {
        return isVisible;
    }

    public bool isTweening()
    {
        if(tween == null)
        {
            return false;
        }
        else
        {
            return tween.IsPlaying();
        }
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
                objectSpacing = (auditionSummaries[0].GetComponent<RectTransform>().rect.height + GetComponent<VerticalLayoutGroup>().spacing) * myTransform.localScale.y;
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
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }
}
