using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NotificationMenu : MonoBehaviour {

    private Transform myTransform;
    private float height;


    //public vars
    public float transitionTime;

    //Provate vars
   

	// Use this for initialization
	void Start () {
        myTransform = this.transform;
        height = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.height;
        myTransform.position = new Vector3(myTransform.position.x, -height, myTransform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowNotificationMenu()
    {
        Debug.Log("showing menu");
        myTransform.DOLocalMoveY(0, transitionTime);
    }

    public void HideNotificationMenu()
    {
        myTransform.DOLocalMoveY(-1.5f*height, transitionTime);
    }

    [ContextMenu("show pos")]
    public void showpos()
    {
        Debug.Log(transform.position);
    }

}
