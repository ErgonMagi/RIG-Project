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
	
	public void addNotification(string text)
    {
        notificationList.Add(new Notification(text));
        //notificationBanner.setText("You have " + notificationList.Count + " notifications");
        notificationBanner.setText(text);
        notificationBanner.showNotification();
    }
}
