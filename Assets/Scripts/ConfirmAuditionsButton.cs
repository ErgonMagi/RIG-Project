using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmAuditionsButton : MonoBehaviour {

    public AuditionScreen auditionScreen;

    public void onClick()
    {
        NotificationManager.Instance.QuickNotification("Actors sent on auditions");
        auditionScreen.submitActors();
    }
}
