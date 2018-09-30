using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAuditionsButton : MonoBehaviour {

    public AuditionScreen auditionScreen;

    public void onClick()
    {
        if (Player.Instance.getMoney() > auditionScreen.getSubmissionPrice())
        {
            NotificationManager.Instance.QuickNotification("Actors sent on auditions");
            Player.Instance.spendMoney(auditionScreen.getSubmissionPrice());
            auditionScreen.submitActors();
        }
        else
        {
            NotificationManager.Instance.QuickNotification("You cannot afford to do that");
        }

    }
}
