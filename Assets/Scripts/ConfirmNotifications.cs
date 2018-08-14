using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmNotifications : MonoBehaviour {

	public void Clicked()
    {
        NotificationManager.Instance.hideNotifications();
    }
}
