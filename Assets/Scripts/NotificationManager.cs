using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class NotificationManager : Singleton<NotificationManager> {

    private List<Notification> notificationList;

    public NotificationBanner notificationBanner;
    public NotificationMenu notificationMenu;

    public ReturnButton returnButton;

    public Image quickNotification;
    public TextMeshProUGUI quickNotiText;
    private Color onColor, offColor, onTextColor, offTextColor;


    public AuditionResultNotifications auditionResults;
    public MovieResultNotifications movieResults;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        notificationList = new List<Notification>();
        onColor = quickNotification.color;
        offColor = new Color(onColor.r, onColor.g, onColor.b, 0);
        quickNotification.color = offColor;
        onTextColor = quickNotiText.color;
        offTextColor = new Color(onTextColor.r, onTextColor.g, onTextColor.b, 0);
        quickNotiText.color = offTextColor;
        quickNotification.transform.DOLocalMoveY(-1000, 0).SetDelay(2);
    }
	
    public void QuickNotification(string notiText)
    {
        quickNotification.transform.localPosition = Vector3.zero;
        quickNotiText.text = notiText;
        quickNotification.DOColor(onColor, 1);
        quickNotification.DOColor(offColor, 1).SetDelay(1.5f);
        quickNotiText.DOColor(onTextColor, 1);
        quickNotiText.DOColor(offTextColor, 1).SetDelay(1.5f);
        quickNotification.transform.DOLocalMoveY(-1000, 0).SetDelay(2.5f);
    }

    //Adds a notification to the end of the list
	public void addNotification(string text, Actor actor, Movie movie, bool passed, Notification.NotificationType notiType)
    {
        notificationList.Add(new Notification(text, actor, movie, passed, notiType));
        notificationBanner.setText(notificationList.Count.ToString());
        notificationBanner.showNotification();
    }

    public void UpdateNotificationsUI()
    {
        auditionResults.UpdateUI();
        movieResults.UpdateUI();
    }

    //Returns the notifications given by notificationNum
    public Notification getNotification(int notificationNum)
    {
        return notificationList[notificationNum];
    }

    public List<Notification> getNotificationsList()
    {
        return notificationList;
    }

    public List<Notification> getAuditionResultNotifications()
    {
        List<Notification> auditionNots = new List<Notification>();

        for (int i = 0; i < notificationList.Count; i++)
        {
            if(notificationList[i].getNotiType() == Notification.NotificationType.Audition)
            {
                auditionNots.Add(notificationList[i]);
            }
        }

        for (int i = 0; i < auditionNots.Count; i++)
        {
            notificationList.Remove(auditionNots[i]);
        }

        return auditionNots;
    }

    public List<Notification> getMovieResultNotifications()
    {
        List<Notification> auditionNots = new List<Notification>();

        for (int i = 0; i < notificationList.Count; i++)
        {
            if (notificationList[i].getNotiType() == Notification.NotificationType.Movie)
            {
                auditionNots.Add(notificationList[i]);
            }
        }

        for (int i = 0; i < auditionNots.Count; i++)
        {
            notificationList.Remove(auditionNots[i]);
        }

        return auditionNots;
    }

    //Returns the number of unread notifications 
    public int getNumrUnreadNotifications()
    {
        int count = 0;
        for(int i = 0; i < notificationList.Count; i++)
        {
            if(notificationList[i].isMessageUnread())
            {
                count++;
            }
        }
        return count;
    }
   
    public void BannerClicked()
    {
        showNotifications();
    }

    public void EnvelopeClicked()
    {
        showNotifications();
    }

    public void showNotifications()
    {
        UpdateNotificationsUI();
        if(auditionResults.GetNotifications().Count == 0)
        {
            notificationMenu.showJobResults();
        }
        notificationMenu.ShowNotificationMenu();
        returnButton.SetVisbility(false);
        GameController.Instance.lockCamera();
        GameController.Instance.lockClicking();
    }

    public void hideNotifications()
    {
        notificationMenu.HideNotificationMenu();
        returnButton.SetVisbility(true);
        GameController.Instance.unlockCamera();
        GameController.Instance.unlockClicking();
    }

}
