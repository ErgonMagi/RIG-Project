using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorPicture : MonoBehaviour, ClickableObject {

    private Actor actor;
    private bool selected;
    private Vector3 startPos;
    private CameraManager cameraManager;

	// Use this for initialization
	void Start () {
        selected = false;
        startPos = this.transform.position;
        cameraManager = FindObjectOfType<CameraManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(selected)
        {
            this.transform.position = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));
        }
	}

    public void setActor(Actor a)
    {
        actor = a;
        GetComponent<SpriteRenderer>().sprite = actor.getPicture();
    }

    public void onClick()
    {
        selected = true;
        Debug.Log("clicked");
    }

    private void OnMouseUp()
    {
        selected = false;
        this.transform.position = startPos;
    }
}
