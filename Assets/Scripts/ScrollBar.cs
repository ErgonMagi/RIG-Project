using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBar : MonoBehaviour {

    public List<Transform> scrollObject;
    private Transform myTransform;
    public int currentFocusNum;
    public float Yoffset;
    private Vector3 mousePos;
    private Vector3 prevMousePos;
    private bool scrolling;
    public float yForce;
    public float scrollMomentumScale;
    public float scrollSpeedScale;
    public float scrollDrag;
    public float scrollCutoff;
    public float lockScrollToFocusForce;
    private float totalOffset;

    public float objectSpacing;

	// Use this for initialization
	void Start () {
        scrollObject = new List<Transform>();
        Yoffset = 0;
        totalOffset = 0;
        myTransform = this.transform;
        scrolling = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (scrollObject.Count >= 1)
        {
            //Drag
            yForce *= scrollDrag;
            if(Mathf.Abs(yForce) < scrollCutoff)
            {
                yForce = 0;
            }

            //Apply force
            Yoffset += yForce;

            //Adjust focus
            if (Yoffset < -objectSpacing / 2.0f)
            {
                if (currentFocusNum < scrollObject.Count -1)
                {
                    currentFocusNum++;
                    Yoffset += objectSpacing;
                    totalOffset -= objectSpacing;
                }
                else
                {
                    Debug.Log("Hit minimum");
                    Yoffset = -objectSpacing / 2.0f;
                }
            }
            else if (Yoffset > objectSpacing / 2.0f)
            {
                if (currentFocusNum > 0)
                {
                    currentFocusNum--;
                    Yoffset -= objectSpacing;
                    totalOffset += objectSpacing;
                }
                else
                {
                    Debug.Log("Hit maximum");
                    Yoffset = objectSpacing / 2.0f;
                }
            }

            //Move towards focus
            if (!scrolling)
            {
                Yoffset -= Yoffset * lockScrollToFocusForce;
                if (Mathf.Abs(Yoffset) < 0.05)
                {
                    Yoffset = 0;
                    yForce = 0;
                }
            }

        //Set position of all objects    
            for(int i = 0; i < scrollObject.Count; i++)
            {
                scrollObject[i].position = new Vector3(myTransform.position.x, myTransform.position.y + totalOffset + Yoffset - i*objectSpacing, myTransform.position.z);
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
        yForce = (mousePos.y - prevMousePos.y) * scrollMomentumScale;
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

        float yChange = (mousePos.y - prevMousePos.y) * scrollSpeedScale;
        Yoffset += yChange;
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
