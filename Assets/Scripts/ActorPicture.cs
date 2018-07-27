using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPicture : MonoBehaviour { //ClickableObject {

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
        cameraManager = FindObjectOfType<CameraManager>();
	}
	
	// Update is called once per frame
	void Update () {
        
        //TODO: Add lerping to the movement.

        //If the actor is selected, follow the mouse/finger
		/*if(selected)
        {
            this.transform.position = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));
        }
        //If not selected, but has a lock position, set it to the lock position
        else if(lockedPosition != null)
        {
            this.transform.position = lockedPosition.transform.position;
        }
        //If it is not selected and has no lock position, return it to the start pos
        else
        {
            this.transform.localPosition = startPos;
        }*/
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

    //Selects the object when it is clicked.
  /*  public void onClick()
    {
        selected = true;
    }

    //When the mouse is lifted
    private void OnMouseUp()
    {
        
        if (selected)
        {
            //Get the mouse position as a vector in the world
            Vector3 mousePos = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));

            Vector3 dirFromCam = new Vector3(0, 0, 1);
            if (!cameraManager.getCam().orthographic)
            {
                dirFromCam = mousePos - cameraManager.getCam().transform.position;
            }

            //Check to see if the actor was dropped on another actor, and if so, swap their positions.
            RaycastHit[] hitactor = Physics.RaycastAll(mousePos, dirFromCam, 15.0f, 1 << LayerMask.NameToLayer("Clickable"));

            foreach (RaycastHit h in hitactor)
            {
                if (h.collider.gameObject.GetComponent<ActorPicture>() != null)
                {
                    if (h.collider.gameObject != this.gameObject)
                    {
                        if (lockedPosition != null)
                        {
                            GameObject temp;
                            temp = lockedPosition;
                            lockedPosition = h.collider.gameObject.GetComponent<ActorPicture>().lockedPosition;
                            h.collider.gameObject.GetComponent<ActorPicture>().lockedPosition = temp;
                        }
                        else
                        {
                            lockedPosition = h.collider.gameObject.GetComponent<ActorPicture>().lockedPosition;
                            h.collider.gameObject.GetComponent<ActorPicture>().lockedPosition = null;
                        }
                    }
                }
            }

            //If it wasn't dropped on another actor, check if it was dropped somewhere it can lock to.
            checkForLockPos();
        }
        //Deselect the actor
        selected = false;
    }*/

    //Unlocks the actor from its locked position
    public void unlockPos()
    {
        lockedPosition = null;
    }

    //Checks the mouse position for a place to lock the actor to
    public void checkForLockPos()
    {
        Vector3 mousePos = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));

        Vector3 dirFromCam = new Vector3(0, 0, 1);
        if (!cameraManager.getCam().orthographic)
        {
            dirFromCam = mousePos - cameraManager.getCam().transform.position;
        }

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos, dirFromCam, 15.0f, 1 << LayerMask.NameToLayer("ActorSlot"));
        foreach (RaycastHit2D h in hit)
        {
            if (h.collider.gameObject.layer == LayerMask.NameToLayer("ActorSlot"))
            {
                lockedPosition = h.collider.gameObject;
                movie = h.collider.gameObject.GetComponentInParent<AuditionSlot>().getMovie();
            }
        }
        if (hit.Length == 0)
        {
            lockedPosition = null;
        }
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

    //Resets the picture to  starting position and no movie assigned (Note: leaves the actor assigned to the picture).
    public void Reset()
    {
        selected = false;
        this.transform.position = startPos;
        movie = null;
        lockedPosition = null;
    }
}
