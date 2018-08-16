using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorPicture : MonoBehaviour {

    private Actor actor;
    public Image imageRenderer;
    private Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        enabled = false;
    }

    //Sets the actor for this object to a
    public void setActor(Actor a)
    {
        if(a == null)
        {
            Debug.Log("null actor passed");
        }
        actor = a;
        imageRenderer.sprite = actor.getPicture();
    }

    public bool hasActor()
    {
        return actor != null;
    }

    public void Empty()
    {
        actor = null;
    }

    public Transform getTransform()
    {
        return myTransform;
    }

    [ContextMenu("show pos")]
    public void showPos()
    {
        Debug.Log(this.transform.localPosition);
    }
}
