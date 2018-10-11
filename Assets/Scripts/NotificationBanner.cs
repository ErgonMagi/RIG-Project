using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class NotificationBanner : MonoBehaviour {

    public TextMeshProUGUI bannerText;
    public Image notiImage;

	// Use this for initialization
	void Start () {
        notiImage.enabled = false;
	}

    public void Update()
    {

    }

    public void showNotification()
    {
        notiImage.enabled = true;
    }

    public void hideNotification()
    {
        notiImage.enabled = false;
        bannerText.text = "";
    }

    public void setText(string text)
    {
        bannerText.text = text;
    }

    public void Clicked()
    {
        hideNotification();

        //Add show the notifications menu.
        NotificationManager.Instance.BannerClicked();
    }

}
