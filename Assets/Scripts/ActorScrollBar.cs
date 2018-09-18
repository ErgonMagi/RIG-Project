using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActorScrollBar : MonoBehaviour, UIUpdatable, IPointerDownHandler, IPointerUpHandler
{

    //Parent Object
    public AuditionScreen auditionScreen;

    public ScrollRect scrollRect;
    public VerticalLayoutGroup scrollbarLayoutGroup;

    public float currentPos;

    //Actors
    public List<ActorPicture> actorPictures;
   
    //Private scroll variables
    private bool scrolling;
    public float objectSpacing;
    public int currentFocusIndex;
    private Transform myTransform;
    private int ActivePicturesInArray;

    public float heightOfScrollbar;
    private int numActiveActors;

    public float snapToMiddleDist;
    public float centringForceStrength;

    // Use this for initialization
    void Start () {
        myTransform = this.transform;
        objectSpacing = actorPictures[0].GetComponent<RectTransform>().rect.height + scrollbarLayoutGroup.spacing;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!scrolling)
        {
            //Calculate centred position in normalized position
            float centrePos = currentFocusIndex * objectSpacing;
            float normalizedPos = centrePos / heightOfScrollbar;

            //Find distance from centre position
            float dist = normalizedPos - scrollRect.verticalNormalizedPosition;

            if (Mathf.Abs(dist * objectSpacing) > snapToMiddleDist)
            {

                //Multiply by scaling force
                float distMovingCloser = dist * centringForceStrength;

                scrollRect.verticalNormalizedPosition += distMovingCloser;
            }
            else
            {
                scrollRect.verticalNormalizedPosition = normalizedPos;
            }
        }




    }

    public int getCurrentFocusnum()
    {
        return currentFocusIndex;
    }

    public void UpdateUI()
    {
        List<Actor> actorList = auditionScreen.getActorsList();
        numActiveActors = actorList.Count;
        for (int i = 0; i < actorPictures.Count; i++)
        {
            if (i < actorList.Count)
            {
                actorPictures[i].gameObject.SetActive(true);
                actorPictures[i].setActor(actorList[i]);
            }  
            else
            {
                actorPictures[i].gameObject.SetActive(false);
                actorPictures[i].Empty();
            }
        }
        UpdateFocusedObject();
    }

    private void UpdateFocusedObject()
    {
        //Calculate percentage positions of all objects
        heightOfScrollbar = (numActiveActors - 1) * objectSpacing;
        currentPos = heightOfScrollbar * scrollRect.verticalNormalizedPosition + objectSpacing / 2.0f;

        int arrayNum = (int)(currentPos / objectSpacing);

        if (arrayNum >= numActiveActors)
        {
            arrayNum = numActiveActors - 1;
        }
        else if (arrayNum < 0)
        {
            arrayNum = 0;
        }

        currentFocusIndex = arrayNum;
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
