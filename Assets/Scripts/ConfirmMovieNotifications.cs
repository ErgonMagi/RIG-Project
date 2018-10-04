using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmMovieNotifications : MonoBehaviour {

    public MovieResultNotifications movieResultNotifications;

    public void Clicked()
    {
        int currencyGained = 0;
        List<Notification> auditionResultList = movieResultNotifications.GetNotifications();
        foreach (Notification n in auditionResultList)
        {
            currencyGained += n.getMovie().getMovieCoinReward();
        }
        Player.Instance.addMoney(currencyGained);
        NotificationManager.Instance.hideNotifications();
    }
}
