using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorScrollBar : MonoBehaviour, UIUpdatable {

    //Parent Object
    public AuditionScreen auditionScreen;

    //Actors
    public List<ActorPicture> actorPictures;
    private Collider2D collider;
    
    //Input variables
    private Vector3 mousePos;
    private Vector3 prevMousePos;

    //Public Scroll variables
    public float offset;
    public bool isHorizontal;
    public float scrollMomentumScale;
    public float scrollSpeedScale;
    public int numPicturesOnScreen;
    public Transform assignBar;
    public float scrollDragScale;
    public float moveToFocusTargetScale;

    //Private scroll variables
    private bool scrolling;
    private float force;
    private float objectSpacing;
    public int currentFocusIndex;
    private float maxOffset;
    private Vector3 startPos;
    private Transform myTransform;
    private float focusTargetOffset;
    private int ActivePicturesInArray;


    // Use this for initialization
    void Start () {
        scrolling = false;
        objectSpacing = 7.79f; //actorPictures[0].GetComponent<RectTransform>().rect.height + GetComponent<VerticalLayoutGroup>().spacing;
        myTransform = this.transform;
        startPos = myTransform.position;
        collider = this.GetComponent<Collider2D>();
    }
	
	// Update is called once per frame
	void Update () {

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
        myTransform.position = new Vector3(myTransform.position.x, startPos.y + offset, myTransform.position.z);
        
	}

    public int getCurrentFocusnum()
    {
        return currentFocusIndex;
    }

    public void UpdateUI()
    {
        List<Actor> actorList = auditionScreen.getActorsList();
        for(int i = 0; i < actorPictures.Count; i++)
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
        UpdateMaxOffset();
        UpdateFocusedObject();
    }

    private void UpdateFocusedObject()
    {
        //find the first picture on screen
        int arrayIndexOfFirstVisible = Mathf.FloorToInt((offset-objectSpacing/2) / objectSpacing);
        int arrayIndexOfCentreObject = Mathf.Min(ActivePicturesInArray-1, arrayIndexOfFirstVisible + Mathf.FloorToInt(numPicturesOnScreen / 2));
        if(arrayIndexOfCentreObject < 0)
        {
            arrayIndexOfCentreObject = 0;
        }
        currentFocusIndex = arrayIndexOfCentreObject;
        focusTargetOffset = currentFocusIndex * objectSpacing;
    }

    private void UpdateMaxOffset()
    {
        //Calculate the max size
        ActivePicturesInArray = 0;
        int totalCounted = 0;
        for (int i = 0; i < actorPictures.Count; i++)
        {
            if (actorPictures[i].hasActor())
            {
                ActivePicturesInArray++;
            }
            totalCounted++;
        }

        maxOffset = ActivePicturesInArray * objectSpacing - (objectSpacing/2);
    } 

    private void OnMouseUp()
    {
        //Check if an actor has been swiped
        if (actorPictures[currentFocusIndex].transform.position.x < assignBar.position.x)
        {
            //Pass actor to audition screen
            auditionScreen.AssignActorToAudition(currentFocusIndex);
        }

        //Reset x position of focused actor
        Transform focusTransform = actorPictures[currentFocusIndex].getTransform();
        focusTransform.position = new Vector3(myTransform.position.x, focusTransform.position.y, focusTransform.position.z);

        //Update the focused target
        UpdateFocusedObject();

        //Calculate the slide effect
        scrolling = false;
        force = (mousePos.y - prevMousePos.y) * scrollMomentumScale;
        
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

        offset += yChange * scrollSpeedScale;

        if(offset < -objectSpacing/2)
        {
            offset = -objectSpacing/2;
        }
        else if (offset > maxOffset)
        {
            offset = maxOffset;
        }

        //Allow horizontal movement of focused object
        Transform focusTransform = actorPictures[currentFocusIndex].getTransform();
        focusTransform.position = new Vector3(CameraManager.Instance.getCam().ScreenToWorldPoint(mousePos).x, focusTransform.position.y, focusTransform.position.z);
       
    }

    public void SetCollision(bool collisionOn)
    {
        if(collisionOn)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }

}
