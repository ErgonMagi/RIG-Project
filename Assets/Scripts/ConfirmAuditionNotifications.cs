using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAuditionNotifications : MonoBehaviour {

    public AuditionResultNotifications auditionResultNotifications;

	public void Clicked()
    {
        int repGained = 0;
        List<Notification> auditionResultList = auditionResultNotifications.GetNotifications();
        foreach(Notification n in auditionResultList)
        {
            repGained += n.getMovie().getAuditionRepReward();
        }
        Player.Instance.AddReputation(repGained);
        NotificationManager.Instance.hideNotifications();
    }
}
