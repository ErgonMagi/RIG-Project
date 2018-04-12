using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private float lerpTime;
    private Vector3 targetRotation;
    private Vector3 targetPosition;
    private float t;
    private Vector3 startRotation;
    private Vector3 startPosition;
    private Camera currentCam;
    private Camera swapCam;


    private bool lerping;

	// Use this for initialization
	void Start () {
        lerping = false;
        currentCam = this.GetComponent<Camera>();
        swapCam = currentCam;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(lerping)
        {
            if(t >= lerpTime)
            {
                currentCam.transform.position = targetPosition;
                //this.transform.rotation = Quaternion.Euler(targetRotation);
                lerping = false;
            }

            currentCam.transform.position = startPosition + (t / lerpTime) * (targetPosition - startPosition);
            //this.transform.rotation = Quaternion.Euler(startRotation + (t / lerpTime) * (targetRotation - startRotation));

            t += Time.deltaTime;
        }
        else
        {
            swapCams(swapCam);
        }

	}

    public void lerpToLoc(Vector3 loc, Vector3 rot, float time)
    {
        targetPosition = loc;
        targetRotation = rot;
        lerpTime = time;

        startRotation = currentCam.transform.rotation.eulerAngles;
        startPosition = currentCam.transform.position;
        t = 0;

        lerping = true;
    }

    public void swapCams(Camera cam)
    {
        currentCam.enabled = false;
        currentCam = cam;
        currentCam.enabled = true;
        swapCam = cam;
    }

    public void swapCamAfterLerp(Camera cam)
    {
        swapCam = cam;
    }
}
