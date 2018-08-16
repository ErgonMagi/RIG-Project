﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class sued for checking success or failure of movies/auditions

public class ScoreManager : MonoBehaviour {

    //Checks whether an actor passes an audition for a movie and adds a notification
    public void checkAudition(ref Actor actor, Movie movie)
    {
        //Check if in the movie
        string[] moviesList = actor.getMoviesStarredIn();
        for(int i = 0; i < moviesList.Length; i++)
        {
            if(moviesList[i] == movie.getTitle())
            {
                actor.toMovie();
                Task t = new Task(actor, movie, 5, false);
                FindObjectOfType<TaskManager>().addTask(t);
                NotificationManager.Instance.addNotification(actor.getName() + " has succeeded on their audition! They were in the movie!", actor, movie, Notification.NotificationType.Audition);
                return;
            }
        }


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
            Task t = new Task(actor, movie, 5, false);
            FindObjectOfType<TaskManager>().addTask(t);
            NotificationManager.Instance.addNotification(actor.getName() + " has succeeded on their audition!", actor, movie, Notification.NotificationType.Audition);
        }
        else
        {
            //If they fail, notify the player
            actor.returnhome();
            NotificationManager.Instance.addNotification(actor.getName() + " failed their audition.", actor, movie, Notification.NotificationType.Audition);
        }
    }

    //Completes a task+
    public void completeTask(Task task)
    {
        if(task.isAudition())
        {
            checkAudition(ref task.actor, task.getmovie());
        }
        else if(task.isMovie())
        {
            NotificationManager.Instance.addNotification(task.actor.getName() + " has completed their movie.", task.actor, task.getmovie(), Notification.NotificationType.Movie);
        }
    }
}
