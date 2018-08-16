using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager> {

    private List<Notification> notificationList;

    public NotificationBanner notificationBanner;
    public NotificationMenu notificationMenu;

    public AuditionResultNotifications auditionResults;
    //public UIUpdatable movieResults;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        notificationList = new List<Notification>();
	}
	
    //Adds a notification to the end of the list
	public void addNotification(string text, Actor actor, Movie movie, Notification.NotificationType notiType)
    {
        notificationList.Add(new Notification(text, actor, movie, notiType));
        //notificationBanner.setText("You have " + notificationList.Count + " notifications");
        notificationBanner.setText(text);
        notificationBanner.showNotification();
    }

    public void UpdateNotificationsUI()
    {
        auditionResults.UpdateUI();
        //movieResults.UpdateUI();
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

        return auditionNots;
    }

    public List<Notification> geMovieResultNotifications()
    {
        List<Notification> auditionNots = new List<Notification>();

        for (int i = 0; i < notificationList.Count; i++)
        {
            if (notificationList[i].getNotiType() == Notification.NotificationType.Movie)
            {
                auditionNots.Add(notificationList[i]);
            }
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
        //Add code for moving camera to the notifications and opening the ui.
        UpdateNotificationsUI();
        notificationMenu.ShowNotificationMenu();
        GameController.Instance.lockCamera();
        GameController.Instance.lockClicking();
    }

    public void hideNotifications()
    {
        notificationMenu.HideNotificationMenu();
        GameController.Instance.unlockCamera();
        GameController.Instance.unlockClicking();
    }

}
