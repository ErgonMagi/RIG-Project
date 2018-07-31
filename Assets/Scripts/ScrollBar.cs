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
                if (currentFocusNum < scrollObject.Count -1)
                {
                    currentFocusNum++;
                    offset += objectSpacing;
                    totalOffset -= objectSpacing;
                }
                else
                {
                    Debug.Log("Hit minimum");
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
                    Debug.Log("Hit maximum");
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
                    scrollObject[i].position = new Vector3(myTransform.position.x + totalOffset + offset - i * objectSpacing, myTransform.position.y, myTransform.position.z);
                }
                else
                {
                    scrollObject[i].position = new Vector3(myTransform.position.x, myTransform.position.y + totalOffset + offset - i * objectSpacing, myTransform.position.z);
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
    }

    private void OnMouseDown()
    {
        mousePos = Input.mousePosition;
        prevMousePos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        scrolling = true;
        prevMousePos = mousePos;
        mousePos = Input.mousePosition;

        float change = 0;
        if (isHorizontal)
        {
            change = (mousePos.x - prevMousePos.x) * scrollSpeedScale;
        }
        else
        {
            change = (mousePos.y - prevMousePos.y) * scrollSpeedScale;
        }
        offset += change;
    }

    public void addObject(Transform addedObject)
    {
        if(scrollObject.Count >= 1)
        {
            scrollObject.Insert(currentFocusNum + 1, addedObject);
            currentFocusNum++;
        }
        else
        {
            scrollObject.Add(addedObject);
            currentFocusNum = 0;
        }
    }

    public GameObject removeFocusObject()
    {
        GameObject temp = scrollObject[currentFocusNum].gameObject;
        scrollObject.Remove(scrollObject[currentFocusNum]);
        return temp;
    }
}
