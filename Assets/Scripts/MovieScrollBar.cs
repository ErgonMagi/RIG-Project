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

    private Collider2D collider;

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
    public float objectSpacing;
    public int currentFocusIndex;
    private float maxOffset;
    private Vector3 startPos;
    private Transform myTransform;
    private float focusTargetOffset;
    private int ActivePicturesInArray;


    // Use this for initialization
    void Start()
    {
        scrolling = false;
        myTransform = this.transform;
        startPos = myTransform.localPosition;
        collider = this.GetComponent<Collider2D>();
        auditions[0].gameObject.SetActive(true);
        objectSpacing = auditions[0].GetComponent<RectTransform>().rect.width + GetComponent<HorizontalLayoutGroup>().spacing;
        auditions[0].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (scrolling)
        {
            prevMousePos = mousePos;
            mousePos = Input.mousePosition;

            float xChange = 0;
            float yChange = 0;

            xChange = mousePos.x - prevMousePos.x;
            yChange = mousePos.y - prevMousePos.y;

            offset += xChange;

            if (offset < -maxOffset)
            {
                offset = -maxOffset;
            }
            else if (offset > objectSpacing / 2)
            {
                offset = objectSpacing / 2;
            }

        }


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
        myTransform.localPosition = new Vector3(startPos.x + offset, myTransform.localPosition.y, myTransform.localPosition.z);

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

    private Vector3 getMousePositionWorldSpace()
    {
        Vector3 mp = Input.mousePosition;
        
        Ray ray = CameraManager.Instance.getCam().ScreenPointToRay(mp);

        Plane plane = new Plane(new Vector3(0, 0, 1), new Vector3(0, 0, 0f));
        float dist;
        plane.Raycast(ray, out dist);

        Vector3 point = ray.origin + dist * ray.direction;

        return point;

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

    public void OnPointerUp(PointerEventData pointer)
    {
        //Update the focused target
        UpdateFocusedObject();

        //Calculate the slide effect
        scrolling = false;
        force = (mousePos.x - prevMousePos.x);
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        scrolling = true;
        mousePos = Input.mousePosition;
        prevMousePos = Input.mousePosition;
    }

    public void SetCollision(bool collisionOn)
    {
        if (collisionOn)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
        }
    }


    [ContextMenu("Print pos")]
    public void printpos()
    {
        Debug.Log(transform.position.ToString("F4"));
    }
}
