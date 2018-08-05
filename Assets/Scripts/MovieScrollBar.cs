using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovieScrollBar : MonoBehaviour, UIUpdatable
{

    //Parent Object
    public AuditionScreen auditionScreen;

    //Auditions
    public List<AuditionSlot> auditions;

    //Input variables
    private Vector3 mousePos;
    private Vector3 prevMousePos;

    //Public Scroll variables
    public float offset;
    public float scrollMomentumScale;
    public float scrollSpeedScale;
    public int numPicturesOnScreen;
    public float scrollDragScale;
    public float moveToFocusTargetScale;

    //Private scroll variables
    private bool scrolling;
    private float force;
    private float objectSpacing;
    private int currentFocusIndex;
    private float maxOffset;
    private Vector3 startPos;
    private Transform myTransform;
    private float focusTargetOffset;
    private int ActivePicturesInArray;


    // Use this for initialization
    void Start()
    {
        scrolling = false;
        objectSpacing = 5.77f; //actorPictures[0].GetComponent<RectTransform>().rect.height + GetComponent<VerticalLayoutGroup>().spacing;
        myTransform = this.transform;
        startPos = myTransform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (!scrolling)
        {
            //Calculate force
            force *= scrollDragScale;
            offset += force;

            //Slide towards focus target

            float focusForce = (focusTargetOffset - offset) * moveToFocusTargetScale;

            offset += focusForce;
        }


        //Update position
        myTransform.position = new Vector3(startPos.x + offset, myTransform.position.y, myTransform.position.z);

    }

    public void UpdateUI()
    {
        List<AuditionScreen.Audition> auditionList = auditionScreen.getAuditionsList();
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
        UpdateMaxOffset();
        UpdateFocusedObject();
    }

    private void UpdateFocusedObject()
    {
        //find the first picture on screen
        int arrayIndexOfFirstVisible = Mathf.FloorToInt((-offset - objectSpacing / 2) / objectSpacing);
        int arrayIndexOfCentreObject = Mathf.Min(ActivePicturesInArray - 1, arrayIndexOfFirstVisible + Mathf.FloorToInt(numPicturesOnScreen / 2));
        if (arrayIndexOfCentreObject < 0)
        {
            arrayIndexOfCentreObject = 0;
        }
        currentFocusIndex = arrayIndexOfCentreObject;
        focusTargetOffset = -currentFocusIndex * objectSpacing;
    }

    private void UpdateMaxOffset()
    {
        //Calculate the max size
        ActivePicturesInArray = 0;
        int totalCounted = 0;
        for (int i = 0; i < auditions.Count; i++)
        {
            if (auditions[i].hasMovie())
            {
                ActivePicturesInArray++;
            }
            totalCounted++;
        }

        maxOffset = ActivePicturesInArray * objectSpacing - (objectSpacing / 2);
    }

    public int getfocusNum()
    {
        return currentFocusIndex;
    }

    private void OnMouseUp()
    {
        //Update the focused target
        UpdateFocusedObject();

        //Calculate the slide effect
        scrolling = false;
        force = (mousePos.x - prevMousePos.x) * scrollMomentumScale;
    }

    private void OnMouseDown()
    {
        scrolling = true;
        mousePos = Input.mousePosition;
        prevMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        prevMousePos = mousePos;
        mousePos = Input.mousePosition;

        float xChange = 0;
        float yChange = 0;

        xChange = mousePos.x - prevMousePos.x;
        yChange = mousePos.y - prevMousePos.y;

        offset += xChange * scrollSpeedScale;

        if (offset < -maxOffset)
        {
            offset = -maxOffset;
        }
        else if (offset > objectSpacing/2)
        {
            offset = objectSpacing/2;
        }


    }

}
