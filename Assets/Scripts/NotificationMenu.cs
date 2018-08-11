using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NotificationMenu : MonoBehaviour {

    private Transform myTransform;

    //public vars
    public float topY;
    public float showY;
    public float bottomY;
    public float transitionTime;

    //Provate vars
   

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        myTransform.position = new Vector3(myTransform.position.x, bottomY, myTransform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowNotificationMenu()
    {
        Debug.Log("showing menu");
        myTransform.DOMoveY(showY, transitionTime);
    }

    public void HideNotificationMenu()
    {
        myTransform.DOMoveY(topY, transitionTime);
        myTransform.DOMoveY(bottomY, 0).SetDelay(transitionTime);
    }

    [ContextMenu("show pos")]
    public void showpos()
    {
        Debug.Log(transform.position);
    }

}
