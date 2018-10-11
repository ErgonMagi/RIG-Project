using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConfirmJobNotifications : MonoBehaviour {

    public MovieResultNotifications movieResultNotifications;

    public void Clicked()
    {
        int currencyEarned = 0;

        List<MoviesCompletedNotification> notiSlots = movieResultNotifications.getNotificationSlots();
        foreach (MoviesCompletedNotification notificationSlot in notiSlots)
        {
            Actor a = notificationSlot.getActor();
            Movie m = notificationSlot.getMovie();

            a.addExperience((float)m.getMovieXPReward());

            notificationSlot.getXPFillImage().DOFillAmount(a.getExperience() / a.getMaxExperience(), 1.0f);

            currencyEarned += notificationSlot.getMovie().getAuditionRepReward();
        }
        Player.Instance.addMoney(currencyEarned);
        NotificationManager.Instance.hideNotifications();
    }
}
