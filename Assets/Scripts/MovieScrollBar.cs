using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovieScrollBar : MonoBehaviour, UIUpdatable, IPointerDownHandler, IPointerUpHandler
{

    //Parent Object
    public AuditionScreen auditionScreen;

    //Auditions
    public List<AuditionSlot> auditions;

    public HorizontalLayoutGroup scrollbarLayoutGroup;
    public ScrollRect scrollRect;
    public RectTransform contentRect;

    public float currentPos;
    public float widthOfScrollbar;

    public float snapToMiddleDist;
    public float centringForceStrength;

    //Private scroll variables
    private bool scrolling;
    private float objectSpacing;
    private float spacing;
    public int currentFocusIndex;
    private int numActiveAuditions;
    private Transform myTransform;


    // Use this for initialization
    void Start()
    {
        scrolling = false;
        myTransform = this.transform;
        spacing = scrollbarLayoutGroup.spacing;
        objectSpacing = auditions[0].GetComponent<RectTransform>().rect.width + spacing;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(scrollRect.horizontalNormalizedPosition);
        if(!scrolling)
        {
            //Calculate centred position in normalized position
            float centrePos = currentFocusIndex * objectSpacing;
            float normalizedPos = centrePos / widthOfScrollbar;

            //Find distance from centre position
            float dist = normalizedPos - scrollRect.horizontalNormalizedPosition;

            if (Mathf.Abs(dist * objectSpacing) > snapToMiddleDist)
            {

                //Multiply by scaling force
                float distMovingCloser = dist * centringForceStrength;

                scrollRect.horizontalNormalizedPosition += distMovingCloser;
            }
            else
            {
                scrollRect.horizontalNormalizedPosition = normalizedPos;
            }
        }
    }

    public void UpdateUI()
    {
        List<AuditionScreen.Audition> auditionList = auditionScreen.getAuditionsList();
        numActiveAuditions = auditionList.Count;
        for (int i = 0; i < auditions.Count; i++)
        {
            if (i < auditionList.Count)
            {
                auditions[i].gameObject.SetActive(true);
                auditions[i].setAudition(auditionList[i]);
                auditions[i].setArrayNum(i);
            }
            else
            {
                auditions[i].gameObject.SetActive(false);
                auditions[i].Empty();
            }
        }
        UpdateFocusedObject();
    }

    private void UpdateFocusedObject()
    {
        //Calculate percentage positions of all objects
        widthOfScrollbar = (numActiveAuditions - 1) * objectSpacing;
        currentPos = widthOfScrollbar * scrollRect.horizontalNormalizedPosition + objectSpacing/2.0f;

        int arrayNum = (int)(currentPos / objectSpacing);

        if(arrayNum >= numActiveAuditions)
        {
            arrayNum = numActiveAuditions - 1;
        }
        else if(arrayNum < 0)
        {
            arrayNum = 0;
        }

        currentFocusIndex = arrayNum;
    }

    public int getfocusNum()
    {
        return currentFocusIndex;
    }

    public void OnPointerUp(PointerEventData pointer)
    {
        //Update the focused target
        UpdateFocusedObject();

        //Calculate the slide effect
        scrolling = false;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        scrolling = true;
    }
}
