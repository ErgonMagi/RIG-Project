using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification {

    private string text;
    private bool isUnread;
    private Actor actor;
    private Movie movie;

    //Constructor
    public Notification(string t, Actor a, Movie m)
    {
        text = t;
        actor = a;
        movie = m;
        isUnread = true;
    }

    //Returns the notifications text
    public string getText()
    {
        return text;
    }

    public Actor getActors()
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
}
