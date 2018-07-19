using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    static List<Tuple <Actor, Movie>> actorMoviePair;
    private Scoreboard scoreboard;

    public void Start()
    {
        actorMoviePair = new List<Tuple<Actor, Movie>>();
        scoreboard = FindObjectOfType<Scoreboard>();
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
        }
        else
        {
            actor.returnhome();
        }
    }

    public void completeTask(Task task)
    {
        if(task.isAudition())
        {
            checkAudition(ref task.actor, task.getmovie());
            FindObjectOfType<ReputationBar>().showNotification();
        }
    }
}
