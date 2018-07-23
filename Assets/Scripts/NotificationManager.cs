using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    private List<Notification> notificationList;

    private NotificationBanner notificationBanner;

	// Use this for initialization
	void Start () {
        notificationList = new List<Notification>();
        notificationBanner = FindObjectOfType<NotificationBanner>();
	}
	
    //Adds a notification to the end of the list
	public void addNotification(string text)
    {
        notificationList.Add(new Notification(text));
        //notificationBanner.setText("You have " + notificationList.Count + " notifications");
        notificationBanner.setText(text);
        notificationBanner.showNotification();
    }

    //Returns the notifications given by notificationNum
    public Notification getNotification(int notificationNum)
    {
        return notificationList[notificationNum];
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
   
    public void showNotifications()
    {
        //Add code for moving camera to the notifications and opening the ui.
    }

}
