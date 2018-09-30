using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour {

    private List<Task> taskList;
    private List<Task> removeList;

    public void Start()
    {
        taskList = new List<Task>();
    }

    //Cycles through the task list looking for any complete tasks.
    public void Update()
    {
        removeList = new List<Task>();

        for(int i = 0; i < taskList.Count; i++)
        {
            if(System.DateTime.Now > taskList[i].getTime())
            {
                //If a task is past its complete time, completes the task.
                CompleteTask(taskList[i]);
                removeList.Add(taskList[i]);
            }
        }

        foreach(Task t in removeList)
        {
            taskList.Remove(t);
        }

    }

    //Completes a task+
    private void CompleteTask(Task task)
    {
        if (task.isAudition())
        {
            if(ScoreManager.Instance.checkAudition(ref task.actor, task.getmovie()))
            {
                //If they pass, send them on the movie, create a task for it and notify the player
                task.actor.toMovie();
                Task t = new Task(task.actor, task.getmovie(), 5, false);
                FindObjectOfType<TaskManager>().addTask(t);
                NotificationManager.Instance.addNotification(task.actor.getName() + " has succeeded on their audition!", task.actor, task.getmovie(), true, Notification.NotificationType.Audition);
            }
            else
            {
                //If they fail, notify the player
                task.actor.returnhome();
                NotificationManager.Instance.addNotification(task.actor.getName() + " failed their audition.", task.actor, task.getmovie(), false, Notification.NotificationType.Audition);
            }
        }
        else if (task.isMovie())
        {
            task.actor.returnhome();
            NotificationManager.Instance.addNotification(task.actor.getName() + " has completed their movie.", task.actor, task.getmovie(), true, Notification.NotificationType.Movie);
        }
    }

    //Adds a task to the tasklist.
    public void addTask(Task task)
    {
        taskList.Add(task);
    }

}
