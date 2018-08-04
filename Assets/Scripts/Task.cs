using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class used for creating timed events such as auditions/movies/training

public class Task {

    enum TaskType
    {
        audition,
        movie,
        training
    }

    public Actor actor;
    private Movie movie;
    private System.DateTime time;
    private TaskType taskType;

    //Constructor
    public Task(Actor actor, Movie movie, double time, bool isAudition)
    {
        this.actor = actor;
        this.movie = movie;
        this.time = System.DateTime.Now.AddSeconds(time);
        if(isAudition)
        {
            taskType = TaskType.audition;
        }
        else
        {
            taskType = TaskType.movie;
        }
    }

    //Returns the time the task will be completed
    public System.DateTime getTime()
    {
        return time;
    }

    //Returns the movie associated with the task
    public Movie getmovie()
    {
        return movie;
    }

    //Returns if the task is an audition
    public bool isAudition()
    {
        return taskType == TaskType.audition;
    }

    //Returns if the task is a movie
    public bool isMovie()
    {
        return taskType == TaskType.movie;
    }

    //Returns if the task is training
    public bool isTraining()
    {
        return taskType == TaskType.training;
    }
}
