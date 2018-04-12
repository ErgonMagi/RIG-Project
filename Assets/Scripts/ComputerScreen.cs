using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScreen : MonoBehaviour, ClickableObject {

    CameraManager cam;
    public Camera computerCam;

	// Use this for initialization
	void Start () {
        cam = FindObjectOfType<CameraManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
        

	}

    public void onClick()
    {
        cam.lerpToLoc(new Vector3(0, 1, -5.2f), new Vector3(0, 0, 0), 1.0f);
        //cam.swapCamAfterLerp(computerCam);
    }
}
