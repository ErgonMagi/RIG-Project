using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class sued for checking success or failure of movies/auditions

public class ScoreManager : MonoBehaviour {

    //Checks whether an actor passes an audition for a movie and adds a notification
    public void checkAudition(ref Actor actor, Movie movie)
    {
        bool pass = true;
        if (actor.getAction() < movie.getAction())
        {
            pass = false;
        }
        if (actor.getComedy() < movie.getComedy())
        {
            pass = false;
        }
        if (actor.getHorror() < movie.getHorror())
        {
            pass = false;
        }
        if (actor.getScifi() < movie.getScifi())
        {
            pass = false;
        }
        if (actor.getRomance() < movie.getRomance())
        {
            pass = false;
        }
        if (actor.getOther() < movie.getOther())
        {
            pass = false;
        }
        if(pass)
        {
            //If they pass, send them on the movie, create a task for it and notify the player
            actor.toMovie();
            Task t = new Task(ref actor, ref movie, 5, false);
            FindObjectOfType<TaskManager>().addTask(t);
            FindObjectOfType<NotificationManager>().addNotification(actor.getName() + " has succeeded on their audition!");
        }
        else
        {
            //If they fail, notify the player
            actor.returnhome();
            FindObjectOfType<NotificationManager>().addNotification(actor.getName() + " failed their audition.");
        }
    }

    //Completes a task+
    public void completeTask(Task task)
    {
        if(task.isAudition())
        {
            checkAudition(ref task.actor, task.getmovie());
        }
    }
}
