﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPicture : MonoBehaviour, ClickableObject {

    private Actor actor;
    private bool selected;
    private Vector3 startPos;
    private CameraManager cameraManager;
    private GameObject lockedPosition;
    private Tuple<Actor, Movie> actorMoviePair;
    private ScoreManager scoreManager;

	// Use this for initialization
	void Start () {
        selected = false;
        startPos = this.transform.position;
        cameraManager = FindObjectOfType<CameraManager>();
        scoreManager = FindObjectOfType<ScoreManager>();
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
	}

    public void setActor(Actor a)
    {
        actor = a;
        GetComponent<SpriteRenderer>().sprite = actor.getPicture();
    }

    public void submitActorMoviePair()
    {
        scoreManager.setPair(actorMoviePair.Item1, actorMoviePair.Item2);
    }

    public void onClick()
    {
        selected = true;
    }

    private void OnMouseUp()
    {
        selected = false;

        Vector3 mousePos = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));

        Vector3 dirFromCam = new Vector3(0, 0, 1);
        if (!cameraManager.getCam().orthographic)
        {
            dirFromCam = mousePos - cameraManager.getCam().transform.position;
        }

        RaycastHit[] hit = Physics.RaycastAll(mousePos, dirFromCam, 15.0f, 1 << LayerMask.NameToLayer("ActorSlot"));

        

        foreach(RaycastHit h in hit)
        {
            if (h.collider.gameObject.layer == LayerMask.NameToLayer("ActorSlot"))
            {
                lockedPosition = h.collider.gameObject;
                actorMoviePair = Tuple.Create(actor, h.collider.gameObject.GetComponentInParent<MovieProfile>().getMovie());
            }
        }
        if(hit.Length == 0)
        {
            lockedPosition = null;
        }

        this.transform.position = startPos;
    }

    public void Reset()
    {
        selected = false;
        this.transform.position = startPos;
        actorMoviePair = null;
        lockedPosition = null;
    }
}
