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

    public void Update()
    {
        removeList = new List<Task>();

        foreach (Task t in taskList)
        {
            if(System.DateTime.Now > t.getTime())
            {
                t.complete();
                removeList.Add(t);
            }
        }

        foreach(Task t in removeList)
        {
            taskList.Remove(t);
        }

    }

    public void addTask(Task task)
    {
        taskList.Add(task);
    }

}
