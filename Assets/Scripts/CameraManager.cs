﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************
 * Manages all the cameras in the game
 * and any transitions between them
 * *******************/

public class CameraManager : Singleton<CameraManager> {

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
	protected override void Awake () {
        base.Awake();

        lerping = false;
        currentCam = Camera.main;
        swapCam = currentCam;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(lerping)
        {

            if (t >= lerpTime)
            {
                currentCam.transform.position = targetPosition;
                currentCam.transform.rotation = Quaternion.Euler(targetRotation);
                lerping = false;
            }
            else
            {
                currentCam.transform.position = startPosition + (t / lerpTime) * (targetPosition - startPosition);
                currentCam.transform.rotation = Quaternion.Euler(startRotation + (t / lerpTime) * (targetRotation - startRotation));
            }

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

        while (targetRotation.x > 90)
        {
            targetRotation.x -= 360;
        }
        while (targetRotation.x < -90)
        {
            targetRotation.x += 360;
        }

        while (targetRotation.y > 90)
        {
            targetRotation.y -= 360;
        }
        while (targetRotation.y < -90)
        {
            targetRotation.y += 360;
        }

        while (targetRotation.z > 90)
        {
            targetRotation.z -= 360;
        }
        while (targetRotation.z < -90)
        {
            targetRotation.z += 360;
        }

        startRotation = currentCam.transform.rotation.eulerAngles;

        while (startRotation.x > 90)
        {
            startRotation.x -= 360;
        }
        while (startRotation.x < -90)
        {
            startRotation.x += 360;
        }

        while (startRotation.y > 90)
        {
            startRotation.y -= 360;
        }
        while (startRotation.y < -90)
        {
            startRotation.y += 360;
        }

        while (startRotation.z > 90)
        {
            startRotation.z -= 360;
        }
        while (startRotation.z < -90)
        {
            startRotation.z += 360;
        }

        startPosition = currentCam.transform.position;
        t = 0;

        lerping = true;
    }

    public void UpdateCamera(Camera newCam)
    {
        swapCam = newCam;
        currentCam = newCam;     
    }

    public void swapCams(Camera cam)
    {
        if(cam != currentCam)
        {
            cam.enabled = true;
            currentCam.enabled = false;
            currentCam = cam;
            swapCam = cam;
        }
    }

    public void swapCamAfterLerp(Camera cam)
    {
        swapCam = cam;
    }

    public Camera getCam()
    {
        return currentCam;
    }

    public bool isLerping()
    {
        return lerping;
    }
}

