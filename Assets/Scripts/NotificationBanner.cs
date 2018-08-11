using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NotificationBanner : MonoBehaviour {

    private Vector3 downPos;
    private Vector3 upPos;
    private Transform myTransform;
    private Tweener tween;

    public float moveTime;
    public float showTime;

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        downPos = myTransform.position;
        upPos = myTransform.position + new Vector3(0, 200, 0);

        myTransform.position = upPos;
	}

    public void showNotification()
    {
        tween = myTransform.DOMoveY(downPos.y, moveTime);
        tween = myTransform.DOMoveY(upPos.y, moveTime).SetDelay(moveTime + showTime);
    }

    public void hideNotification()
    {
        myTransform.position = upPos;
    }

    public void setText(string text)
    {
        this.GetComponentInChildren<Text>().text = text;
    }

    public void Clicked()
    {
        tween.Complete();
        hideNotification();

        //Add show the notifications menu.
        NotificationManager.Instance.BannerClicked();
    }

}
