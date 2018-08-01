using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBar : MonoBehaviour {

    public List<Transform> scrollObject;
    private Transform myTransform;
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

    public bool isHorizontal;
    private float totalOffset;

    public float objectSpacing;

	// Use this for initialization
	void Start () {
        scrollObject = new List<Transform>();
        offset = 0;
        totalOffset = 0;
        myTransform = this.transform;
        scrolling = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (scrollObject.Count >= 1)
        {
            //Drag
            force *= scrollDrag;
            if(Mathf.Abs(force) < scrollCutoff)
            {
                force = 0;
            }

            //Apply force
            offset += force;

            //Adjust focus
            if (offset < -objectSpacing / 2.0f)
            {
                if (currentFocusNum < scrollObject.Count - 1)
                {
                    currentFocusNum++;
                    offset += objectSpacing;
                    totalOffset -= objectSpacing;
                }
                else
                {
                    offset = -objectSpacing / 2.0f;
                }
            }
            else if (offset > objectSpacing / 2.0f)
            {
                if (currentFocusNum > 0)
                {
                    currentFocusNum--;
                    offset -= objectSpacing;
                    totalOffset += objectSpacing;
                }
                else
                {
                    offset = objectSpacing / 2.0f;
                }
            }
            

            //Move towards focus
            if (!scrolling)
            {
                offset -= offset * lockScrollToFocusForce;
                if (Mathf.Abs(offset) < 0.05)
                {
                    offset = 0;
                    force = 0;
                }
            }

        //Set position of all objects    
            for(int i = 0; i < scrollObject.Count; i++)
            {
                if (isHorizontal)
                {
                    scrollObject[i].position = new Vector3(myTransform.position.x + totalOffset + offset + i * objectSpacing, myTransform.position.y, myTransform.position.z);
                }
                else
                {
                    scrollObject[i].position = new Vector3(scrollObject[i].transform.position.x, myTransform.position.y + totalOffset + offset + i * objectSpacing, myTransform.position.z);
                    if(!scrolling)
                    {
                        scrollObject[i].position = new Vector3(myTransform.position.x, myTransform.position.y + totalOffset + offset + i * objectSpacing, myTransform.position.z);
                    }
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
        return scrollObject[currentFocusNum].gameObject;
    }

    private void OnMouseUp()
    {
        scrolling = false;
        if (isHorizontal)
        {
            force = (mousePos.x - prevMousePos.x) * scrollMomentumScale;
        }
        else
        {
            force = (mousePos.y - prevMousePos.y) * scrollMomentumScale;
        }

        GetComponentInParent<MovieMenu>().resetActorAssignedThisDrag();
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
        }
        else
        {
            yChange = (mousePos.y - prevMousePos.y) * scrollSpeedScale;
            offset += yChange;
            if (scrollObject.Count > 0)
            {
                scrollObject[currentFocusNum].transform.position = new Vector3(CameraManager.Instance.getCam().ScreenToWorldPoint(mousePos).x, scrollObject[currentFocusNum].position.y, scrollObject[currentFocusNum].position.z);
            }
        }
        
    }

    public void addObject(Transform addedObject)
    {
        if(scrollObject.Count >= 1)
        {
            scrollObject.Insert(currentFocusNum + 1, addedObject);
        }
        else
        {
            scrollObject.Add(addedObject);
            currentFocusNum = 0;
        }
    }

    public int getNumObjects()
    {
        return scrollObject.Count;
    }

    public GameObject removeFocusObject()
    {
        GameObject temp = scrollObject[currentFocusNum].gameObject;
        scrollObject.Remove(scrollObject[currentFocusNum]);
        if(currentFocusNum == scrollObject.Count)
        {
            currentFocusNum--;
        }
        return temp;
    }
}
