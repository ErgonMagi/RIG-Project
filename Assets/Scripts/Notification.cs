using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification {

    private string text;
    private bool isUnread;

    //Constructor
    public Notification(string t)
    {
        text = t;
        isUnread = true;
    }

    //Returns the notifications text
    public string getText()
    {
        return text;
    }

    //Returns whether the notification has been read
    public bool isMessageUnread()
    {
        return isUnread;
    }
    
    //Sets the notifications to has been read
    public void setRead()
    {
        isUnread = false;
    }
}
