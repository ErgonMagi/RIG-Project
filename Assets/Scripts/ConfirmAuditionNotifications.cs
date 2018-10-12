using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAuditionNotifications : MonoBehaviour {

    public AuditionResultNotifications auditionResultNotifications;
    public MovieResultNotifications movieResultNotifications;
    public NotificationMenu notificationMenu;

	public void Clicked()
    {
        int repGained = 0;
        List<Notification> auditionResultList = auditionResultNotifications.GetNotifications();
        foreach(Notification n in auditionResultList)
        {
            if (n.isPassed())
            {
                repGained += n.getMovie().getAuditionRepReward();
            }
        }
        Player.Instance.AddReputation(repGained);
        auditionResultNotifications.emptyNotifications();
        if (movieResultNotifications.hasNotifications())
        {
            notificationMenu.ChangeMenu(true);
        }
        else
        {
            NotificationManager.Instance.hideNotifications();
        }
    }
}
