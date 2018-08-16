using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class NotificationBanner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public float minSwipeDist;

    private Vector3 downPos;
    private Vector3 upPos;
    private Transform myTransform;
    private Tweener downTween;
    private Tweener upTween;

    private Vector2 clickedPos;

    private Vector2 mouseDownPos;
    private Vector2 mouseUpPos;
    private float halfheight;

    public float moveTime;
    public float showTime;

    private bool clicked;

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        downPos = myTransform.position;
        upPos = myTransform.position + new Vector3(0, 200, 0);

        myTransform.position = upPos;
        clicked = false;

        halfheight = GetComponent<RectTransform>().rect.height / 2;
	}

    public void Update()
    {
        if(clicked)
        {
            //Mark mouse up position
            Vector2 mouseDragPos = Input.mousePosition;

            //Move banner up with mouse
            if (clickedPos.y + (mouseDragPos.y - mouseDownPos.y) > downPos.y)
            {
                myTransform.position = new Vector2(myTransform.position.x, clickedPos.y + (mouseDragPos.y - mouseDownPos.y));
            }
            else
            {
                myTransform.position = new Vector2(myTransform.position.x, downPos.y);   //Add half extents
            }
        }
    }

    public void showNotification()
    {
        downTween = myTransform.DOMoveY(downPos.y, moveTime);
        upTween = myTransform.DOMoveY(upPos.y, moveTime).SetDelay(moveTime + showTime);
    }

    public void hideNotification()
    {
        myTransform.position = upPos;
    }

    public void setText(string text)
    {
        this.GetComponentInChildren<Text>().text = text;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        //Mark the position of the click
        mouseDownPos = Input.mousePosition;

        clickedPos = myTransform.position;

        upTween.Pause();
        downTween.Pause();
        clicked = true;
    }

    public void OnPointerUp(PointerEventData pointer)
    {
        clicked = false;

        //Mark mouse up position
        mouseUpPos = Input.mousePosition;
        Debug.Log(mouseUpPos);

        //Check distance between points
        if(Vector2.Distance(mouseDownPos, mouseUpPos) < minSwipeDist)
        {
            Clicked();
        }
        else
        {
            if(mouseUpPos.y > mouseDownPos.y)
            {
                downTween.Complete();
                upTween.Complete();

                upTween = myTransform.DOMoveY(upPos.y, 0.25f);
            }
        }

    }

    public void Clicked()
    {
        downTween.Complete();
        upTween.Complete();
        hideNotification();

        //Add show the notifications menu.
        NotificationManager.Instance.BannerClicked();
    }

}
