using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorPicture : MonoBehaviour {

    private Actor actor;
    private bool selected;
    private Vector3 startPos;
    private CameraManager cameraManager;
    private Movie movie;
    public Transform actorScrollBar;

	// Use this for initialization
	void Start () {
        selected = false;
        startPos = this.transform.localPosition;
        cameraManager = CameraManager.Instance;
	}

    //Sets the actor for this object to a
    public void setActor(ref Actor a)
    {
        actor = a;
        GetComponent<Image>().sprite = actor.getPicture();
    }

    //Submits to the TaskManager which actor and movie this object has paired.
    public void submitActorMoviePair()
    {
        if(movie != null)
        {
            Task t = new Task(ref actor, ref movie, 2, true);
            FindObjectOfType<TaskManager>().addTask(t);
        }
        movie = null;
    }
 
    public void lockToGameobject(GameObject lockPos)
    {
        this.transform.localScale = new Vector3(1.25f, 1.3f, 1);
        this.transform.SetParent(lockPos.transform);
        movie = lockPos.GetComponentInParent<AuditionSlot>().getMovie();      
        this.transform.localPosition = new Vector3(0, 0, 0);
    }

    //Unlocks the actor from its locked position
    public void unlockPos()
    {
        this.transform.SetParent(actorScrollBar);
        actorScrollBar.GetComponent<ScrollBar>().addObject(gameObject.transform);
        movie = null;
        this.transform.localScale = new Vector3(2.5f, 2.5f, 1);
        this.transform.localPosition = new Vector3(0, 0, 0);
    }

    //Returns if their is an actor assigned to the picture.
    public bool isAssigned()
    {
        if(actor != null)
        {
            return true;
        }
        return false;
    }

    public bool isSetTomovie()
    {
        return movie != null;
    }
}
