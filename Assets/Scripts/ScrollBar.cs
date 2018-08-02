using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBar : MonoBehaviour {

    public List<Transform> scrollObject;
    private Transform myTransform;
    private Vector3 startPos;
    public int currentFocusNum;
    public float offset;
    private Vector3 mousePos;
    private Vector3 prevMousePos;
    private bool scrolling;
    public float force;
    public float scrollMomentumScale;
    public float scrollSpeedScale;
    public float scrollDrag;
    public float scrollCutoff;
    public float lockScrollToFocusForce;
    public Vector3 centrePosition;
    public float currentPos;
    public float focusPos;

    private bool addingToEnd;

    public bool isHorizontal;
    public float totalOffset;

    public float objectSpacing;


	// Use this for initialization
	void Start () {
        scrollObject = new List<Transform>();
        offset = 0;
        totalOffset = 0;
        myTransform = this.transform;
        scrolling = false;
        startPos = myTransform.position;
        centrePosition = this.transform.position;
        addingToEnd = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (scrollObject.Count >= 1)
        {
          
            for(int i = 0; i < scrollObject.Count; i++)
            {
                scrollObject[i].SetSiblingIndex(i);
            }

            //Drag
            force *= scrollDrag;
            if(Mathf.Abs(force) < scrollCutoff)
            {
                force = 0;
            }

            //Apply force
            offset += force;

            //Update focus if not scrolling
            if (!scrolling)
            {
                if (!isHorizontal)
                {
                    while (offset > objectSpacing / 2)
                    {
                        if (currentFocusNum < scrollObject.Count - 1)
                        {
                            totalOffset += objectSpacing;
                            offset -= objectSpacing;
                            currentFocusNum++;
                        }
                    }
                    while (offset < -objectSpacing / 2)
                    {
                        if (currentFocusNum > 0)
                        {
                            totalOffset -= objectSpacing;
                            offset += objectSpacing;
                            currentFocusNum--;
                        }
                    }
                }
                else
                {
                    while (offset < -objectSpacing / 2)
                    {
                        if (currentFocusNum < scrollObject.Count - 1)
                        {
                            totalOffset -= objectSpacing;
                            offset += objectSpacing;
                            currentFocusNum++;
                        }
                    }
                    while (offset > objectSpacing / 2)
                    {
                        if (currentFocusNum > 0)
                        {
                            totalOffset += objectSpacing;
                            offset -= objectSpacing;
                            currentFocusNum--;
                        }
                    }
                }
            }

            //Move towards focus
            if (!scrolling)
            {
                offset -= offset * lockScrollToFocusForce;
                //move to focus
                if (!isHorizontal)
                {
                    if ((startPos.y - myTransform.position.y) - objectSpacing * currentFocusNum > objectSpacing)
                    {
                        Debug.Log("moving +half");
                        offset += objectSpacing/2;
                    }
                    else if ((myTransform.position.y - startPos.y) - objectSpacing * currentFocusNum < -objectSpacing)
                    {
                        Debug.Log("moving -half");
                        offset -= objectSpacing/2;
                    }
                }
                else
                {
                    if ((startPos.x - myTransform.position.x) - objectSpacing * currentFocusNum > objectSpacing)
                    {
                        offset -= objectSpacing / 2;
                    }
                    else if ((startPos.x - myTransform.position.x) - objectSpacing * currentFocusNum < -objectSpacing)
                    {
                        offset += objectSpacing / 2;
                    }
                }
                
            }

            //Set position of all objects 
            if (isHorizontal)
            {
                this.transform.position = new Vector3(startPos.x + offset + totalOffset, myTransform.position.y, myTransform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(myTransform.position.x, startPos.y + offset + totalOffset, myTransform.position.z);
            }

            //Keep inside bounds
            if (isHorizontal)
            {
                if (offset + totalOffset < -(scrollObject.Count - 1) * objectSpacing - objectSpacing / 2)
                {
                    offset = -(objectSpacing / 2) + 0.05f;
                    totalOffset = -(scrollObject.Count - 1) * objectSpacing;
                }
                else if (offset + totalOffset > objectSpacing / 2)
                {
                    offset = objectSpacing / 2 - 0.05f;
                    totalOffset = 0;
                }
            }
            else
            {
                if (offset + totalOffset <  -objectSpacing / 2)
                { 
                    offset = -objectSpacing / 2 + 0.05f;
                    totalOffset = 0;            
                }
                else if (offset + totalOffset > (scrollObject.Count - 1) * objectSpacing + objectSpacing / 2)
                {
                    offset = objectSpacing / 2 - 0.05f;
                    totalOffset = (scrollObject.Count - 1) * objectSpacing;
                }
            }
        }
        
	}

    public bool isInList(GameObject checkedObject)
    {
        for(int i = 0; i < scrollObject.Count; i++)
        {
            if(checkedObject == scrollObject[i].gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public GameObject getCurrentFocus()
    {
        if(currentFocusNum >= scrollObject.Count)
        {
            return null;
        }
        return scrollObject[currentFocusNum].gameObject;
    }

    private void OnMouseUp()
    {
        GetComponentInParent<MovieMenu>().AssignActor();
        scrolling = false;
        if (isHorizontal)
        {
            force = (mousePos.x - prevMousePos.x) * scrollMomentumScale;
        }
        else
        {
            force = (mousePos.y - prevMousePos.y) * scrollMomentumScale;
        }

        if (!isHorizontal)
        {
            for (int i = 0; i < scrollObject.Count; i++)
            {
                scrollObject[i].localPosition = new Vector3(0, scrollObject[i].localPosition.y, 0);
            }
        }
    }

    public void resetYToSwipedObject(Transform swipedObject)
    {
        for(int i = 0; i < scrollObject.Count; i++)
        {
            if(swipedObject == scrollObject[i])
            {
                totalOffset = objectSpacing * i;
                offset = 0;
            }
        }
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
        if (isHorizontal)
        {
            xChange = (mousePos.x - prevMousePos.x) * scrollSpeedScale;
            offset += xChange;
            if (offset + totalOffset < -(scrollObject.Count - 1) * objectSpacing - objectSpacing / 2)
            {
                offset = -objectSpacing / 2 + 0.05f;
                totalOffset = -(scrollObject.Count - 1) * objectSpacing;
                currentFocusNum = scrollObject.Count - 1;
            }
            else if (offset + totalOffset > objectSpacing / 2)
            {
                offset = objectSpacing / 2 - 0.05f;
                totalOffset = 0;
                currentFocusNum = 0;
            }
        }
        else
        {
            yChange = (mousePos.y - prevMousePos.y) * scrollSpeedScale;
            offset += yChange;
            if (scrollObject.Count > 0)
            {
                for (int i = 0; i < scrollObject.Count; i++)
                {
                    scrollObject[i].localPosition = new Vector3(0, scrollObject[i].localPosition.y, 0);
                }
                scrollObject[currentFocusNum].transform.position = new Vector3(CameraManager.Instance.getCam().ScreenToWorldPoint(mousePos).x, scrollObject[currentFocusNum].position.y, scrollObject[currentFocusNum].position.z);
            }

            if (offset + totalOffset < -objectSpacing / 2)
            {
                offset = -objectSpacing / 2 + 0.05f;
                totalOffset = 0;
                currentFocusNum = 0;
            }
            else if (offset + totalOffset > (scrollObject.Count - 1) * objectSpacing + objectSpacing / 2)
            {
                offset = objectSpacing / 2 - 0.05f;
                currentFocusNum = scrollObject.Count - 1;
                totalOffset = (scrollObject.Count - 1) * objectSpacing;
            }
        }
        
    }

    public void addObject(Transform addedObject)
    {
        addedObject.SetParent(myTransform);
        int index = currentFocusNum;
        if(scrollObject.Count == 0)
        {
            scrollObject.Add(addedObject);
            currentFocusNum = 0;
        }
        else
        {
            if (index > scrollObject.Count)
            {
                index = scrollObject.Count;
            }
            scrollObject.Insert(index, addedObject);
            if (index == scrollObject.Count -1)
            {
                currentFocusNum++;
            }
        }
        
       
        if (!isHorizontal)
        {
            objectSpacing =  GetComponentInChildren<VerticalLayoutGroup>().spacing + scrollObject[currentFocusNum].GetComponent<RectTransform>().rect.height;
        }
        else
        {
            objectSpacing = GetComponentInChildren<HorizontalLayoutGroup>().spacing + scrollObject[currentFocusNum].GetComponent<RectTransform>().rect.width;
        }
    }

    public int getNumObjects()
    {
        int numAssigned = 0;

        for(int i = 0; i< scrollObject.Count; i++)
        {
            if(scrollObject[i].GetComponent<ActorPicture>() != null)
            {
                if(scrollObject[i].GetComponent<ActorPicture>().isSetTomovie())
                {
                    numAssigned++;
                }
            }
            else if (scrollObject[i].GetComponent<AuditionSlot>() != null)
            {
                if (scrollObject[i].GetComponent<AuditionSlot>().getActor() != null)
                {
                    numAssigned++;
                }
            }
        }



        return scrollObject.Count - numAssigned;
    }

    public GameObject removeFocusObject()
    {
        GameObject temp;       
        temp = scrollObject[currentFocusNum].gameObject;
        scrollObject.Remove(scrollObject[currentFocusNum]);
        if (currentFocusNum == scrollObject.Count)
        {
            currentFocusNum--;
        }


        return temp;
    }
}
