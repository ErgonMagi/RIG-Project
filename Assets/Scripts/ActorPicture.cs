using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPicture : MonoBehaviour, ClickableObject {

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
		if(selected)
        {
            this.transform.position = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));
        }
        else if(lockedPosition != null)
        {
            this.transform.position = lockedPosition.transform.position;
        }
        else
        {
            this.transform.localPosition = startPos;
        }
	}

    public void setActor(ref Actor a)
    {
        actor = a;
        GetComponent<SpriteRenderer>().sprite = actor.getPicture();
    }

    public void submitActorMoviePair()
    {
        if(movie != null)
        {
            Task t = new Task(ref actor, ref movie, 2, true);
            FindObjectOfType<TaskManager>().addTask(t);
        }
        movie = null;
    }

    public void onClick()
    {
        selected = true;
    }

    private void OnMouseUp()
    {

        if (selected)
        {
            Vector3 mousePos = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));

            Vector3 dirFromCam = new Vector3(0, 0, 1);
            if (!cameraManager.getCam().orthographic)
            {
                dirFromCam = mousePos - cameraManager.getCam().transform.position;
            }

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
            checkForLockPos();
        }
        selected = false;
    }

    public void unlockPos()
    {
        lockedPosition = null;
    }

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
                movie = h.collider.gameObject.GetComponentInParent<MovieProfile>().getMovie();
            }
        }
        if (hit.Length == 0)
        {
            lockedPosition = null;
        }
    }

    public bool isAssigned()
    {
        if(actor != null)
        {
            return true;
        }
        return false;
    }

    public void Reset()
    {
        selected = false;
        this.transform.position = startPos;
        movie = null;
        lockedPosition = null;
    }
}
