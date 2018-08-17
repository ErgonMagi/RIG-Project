using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class sued for checking success or failure of movies/auditions

public class ScoreManager : Singleton<ScoreManager> {

    //Checks whether an actor passes an audition for a movie and adds a notification
    public bool checkAudition(ref Actor actor, Movie movie)
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
                return true;
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
            return true;
        }
        else
        {
            return false;
        }
    }

}
