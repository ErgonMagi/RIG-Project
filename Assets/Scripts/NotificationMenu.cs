using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NotificationMenu : MonoBehaviour {

    private Transform myTransform;
    private float height;

    public GameObject leftArrow;
    public GameObject rightArrow;

    public ScrollRect auditionScroll;
    public ScrollRect movieScroll;

    public float[] menuXPositions;
    private int currentMenu;


    //public vars
    public float transitionTime;
   

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        height = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height;
        myTransform.position = new Vector3(myTransform.position.x, -height, myTransform.position.z);
        currentMenu = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeMenu(bool moveRight)
    {
        if(moveRight)
        {
            if(currentMenu < menuXPositions.Length-1)
            {
                currentMenu++;
            }
        }
        else
        {
            if (currentMenu > 0)
            {
                currentMenu--;
            }         
        }

        myTransform.DOMoveX(menuXPositions[currentMenu], transitionTime);

        if(currentMenu > 0)
        {
            leftArrow.SetActive(true);
        }
        else
        {
            leftArrow.SetActive(false);
        }

        if(currentMenu < menuXPositions.Length -1)
        {
            rightArrow.SetActive(true);
        }
        else
        {
            rightArrow.SetActive(false);
        }
    }

    public void ShowNotificationMenu()
    {
        rightArrow.SetActive(true);
        movieScroll.verticalNormalizedPosition = 1;
        auditionScroll.verticalNormalizedPosition = 1;
        myTransform.DOLocalMoveY(0, transitionTime);
    }

    public void HideNotificationMenu()
    {
        rightArrow.SetActive(false);
        leftArrow.SetActive(false);
        myTransform.DOLocalMoveY(-1.5f*height, transitionTime);
        myTransform.transform.position = new Vector2(menuXPositions[0], myTransform.position.y);
    }

    [ContextMenu("show pos")]
    public void showpos()
    {
        Debug.Log(transform.position);
    }

    [ContextMenu("Set scrolls to 0")]
    public void scrollToTop()
    {
        movieScroll.verticalNormalizedPosition = 1;
        auditionScroll.verticalNormalizedPosition = 1;
    }

}
