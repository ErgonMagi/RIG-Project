using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Task(ref Actor actor, ref Movie movie, double time, bool isAudition)
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

    public System.DateTime getTime()
    {
        return time;
    }

    public Movie getmovie()
    {
        return movie;
    }

    public bool isAudition()
    {
        return taskType == TaskType.audition;
    }

    public bool isMovie()
    {
        return taskType == TaskType.movie;
    }

    public bool isTraining()
    {
        return taskType == TaskType.training;
    }
}
