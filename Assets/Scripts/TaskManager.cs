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
                FindObjectOfType<ScoreManager>().completeTask(taskList[i]);
                removeList.Add(taskList[i]);
            }
        }

        foreach(Task t in removeList)
        {
            taskList.Remove(t);
        }

    }

    //Adds a task to the tasklist.
    public void addTask(Task task)
    {
        taskList.Add(task);
    }

}
