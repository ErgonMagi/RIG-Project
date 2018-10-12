using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ConfirmJobNotifications : MonoBehaviour {

    public AuditionResultNotifications auditionResultNotifications;
    public NotificationMenu notificationMenu;
    public MovieResultNotifications movieResultNotifications;
    private bool closing;

    public void Clicked()
    {
        if (!closing)
        {
            closing = true;
            int currencyEarned = 0;

            List<MoviesCompletedNotification> notiSlots = movieResultNotifications.getNotificationSlots();
            foreach (MoviesCompletedNotification notificationSlot in notiSlots)
            {
                Actor a = notificationSlot.getActor();
                Movie m = notificationSlot.getMovie();

                if (a != null)
                {
                    a.addExperience((float)m.getMovieXPReward());

                    notificationSlot.getXPFillImage().DOFillAmount(a.getExperience() / a.getMaxExperience(), 1.0f);

                    currencyEarned += notificationSlot.getMovie().getAuditionRepReward();
                }
            }
            Player.Instance.addMoney(currencyEarned);
            StartCoroutine(closeMenuAfterWait(1.5f));
        }
    }

    public IEnumerator closeMenuAfterWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        closing = false;
        movieResultNotifications.emptyNotifications();
        if (auditionResultNotifications.hasNotifications())
        {
            notificationMenu.ChangeMenu(false);
        }
        else
        {
            NotificationManager.Instance.hideNotifications();
        }
    }
}
