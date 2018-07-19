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

    private Actor actor;
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

    public void complete()
    {
        Debug.Log("Task complete");
    }

}
