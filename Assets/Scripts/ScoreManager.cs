using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public void Start()
    {

    }

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
            actor.toMovie();
            Task t = new Task(ref actor, ref movie, 5, false);
            FindObjectOfType<TaskManager>().addTask(t);
            FindObjectOfType<NotificationManager>().addNotification(actor.getName() + " has succeeded on their audition!");
        }
        else
        {
            actor.returnhome();
            FindObjectOfType<NotificationManager>().addNotification(actor.getName() + " failed their audition.");
        }
    }

    public void completeTask(Task task)
    {
        if(task.isAudition())
        {
            checkAudition(ref task.actor, task.getmovie());
        }
    }
}
