using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionResultNotifications : MonoBehaviour, UIUpdatable {

    private List<Notification> notifications;

    public List<NotificationSlot> notificationSlots;
	
	public void UpdateUI()
    {
        notifications = NotificationManager.Instance.getAuditionResultNotifications();
        int slotNum = 0;


        for (int i = 0; i < notifications.Count; i++)
        {
            if(slotNum >= notificationSlots.Count)
            {
                break;
            }
            else
            {
                notificationSlots[slotNum].gameObject.SetActive(true);
                notificationSlots[slotNum].SetNotification(notifications[i]);
                slotNum++;
            }
        }

        for(int i = slotNum; i < notificationSlots.Count; i++)
        {
            notificationSlots[i].gameObject.SetActive(false);
        }
    }

    public List<Notification> GetNotifications()
    {
        return notifications;
    }
}
