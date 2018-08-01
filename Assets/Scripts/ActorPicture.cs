using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPicture : MonoBehaviour {

    private Actor actor;
    private bool selected;
    private Vector3 startPos;
    private CameraManager cameraManager;
    private GameObject lockedPosition;
    private Movie movie;

	// Use this for initialization
	void Start () {
        selected = false;
        startPos = this.transform.localPosition;
        cameraManager = CameraManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        
        //TODO: Add lerping to the movement.
        //If not selected, but has a lock position, set it to the lock position
        if(lockedPosition != null)
        {
            this.transform.position = lockedPosition.transform.position;
        }
	}

    //Sets the actor for this object to a
    public void setActor(ref Actor a)
    {
        actor = a;
        GetComponent<SpriteRenderer>().sprite = actor.getPicture();
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
        lockedPosition = lockPos;
        movie = lockPos.GetComponentInParent<AuditionSlot>().getMovie();
        this.transform.localScale = new Vector3(1.25f, 1.3f, 1);
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }

    //Unlocks the actor from its locked position
    public void unlockPos()
    {
        movie = null;
        lockedPosition = null;
        this.transform.localScale = new Vector3(2.5f, 2.5f, 1);
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
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
