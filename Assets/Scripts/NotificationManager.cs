using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager> {

    private List<Notification> notificationList;

    public NotificationBanner notificationBanner;
    public NotificationMenu notificationMenu;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        notificationList = new List<Notification>();
	}
	
    //Adds a notification to the end of the list
	public void addNotification(string text, Actor actor, Movie movie)
    {
        notificationList.Add(new Notification(text, actor, movie));
        //notificationBanner.setText("You have " + notificationList.Count + " notifications");
        notificationBanner.setText(text);
        notificationBanner.showNotification();
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

    public void showNotifications()
    {
        //Add code for moving camera to the notifications and opening the ui.
        notificationMenu.ShowNotificationMenu();
    }

}
