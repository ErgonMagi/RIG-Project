using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification {

    private string text;
    private bool isUnread;
    private Actor actor;
    private Movie movie;
    private bool passed;

    public enum NotificationType{
        Audition,
        Movie,
        Training
        };

    private NotificationType notificationType;

    //Constructor
    public Notification(string t, Actor a, Movie m, bool passed, NotificationType nt)
    {
        text = t;
        actor = a;
        movie = m;
        notificationType = nt;
        isUnread = true;
        this.passed = passed;
    }

    //Returns the notifications text
    public string getText()
    {
        return text;
    }

    public NotificationType getNotiType()
    {
        return notificationType;
    }

    public Actor getActor()
    {
        return actor;
    }

    public Movie getMovie()
    {
        return movie;
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

    public bool isPassed()
    {
        return passed;
    }
}
