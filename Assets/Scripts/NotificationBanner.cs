using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationBanner : MonoBehaviour {

    private Vector3 downPos;
    private Vector3 upPos;

    enum NotiState
    {
        movingDown,
        movingUp,
        showing,
        hiding
    }
    private NotiState nState;
    private float t;
    public float moveTime;
    public float showTime;

	// Use this for initialization
	void Start () {
        downPos = this.transform.position;
        upPos = this.transform.position + new Vector3(0, 200, 0);
        nState = NotiState.hiding;

        this.transform.position = upPos;
	}
	
	// Update is called once per frame
	void Update () {

        t += Time.deltaTime;

        if(nState == NotiState.movingDown)
        {
            if(t < moveTime)
            {
                this.transform.position = upPos + (t / moveTime) * (downPos - upPos);
            }
            else
            {
                t = 0;
                this.transform.position = downPos;
                nState = NotiState.showing;
            }
        }
        else if(nState == NotiState.movingUp)
        {
            if (t < moveTime)
            {
                this.transform.position = downPos + (t / moveTime) * (upPos - downPos);
            }
            else
            {
                t = 0;
                this.transform.position = upPos;
                nState = NotiState.hiding;
            }
        }
        else if(nState == NotiState.showing)
        {
            if(t >= showTime)
            {
                t = 0;
                nState = NotiState.movingUp;
            }
        }
	}

    public void showNotification()
    {
        t = 0;
        nState = NotiState.movingDown;
    }

    public void setText(string text)
    {
        this.GetComponentInChildren<Text>().text = text;
    }

}
