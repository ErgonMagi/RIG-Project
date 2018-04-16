﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************
 * Controls the state of the game,
 * i.e. where the camera is and what 
 * the player can interact with.
 * *************************/


public class GameController : MonoBehaviour {

    public Camera computerCam;

    private enum Gamestate
    {
        desk, computer
    }

    private Gamestate gamestate;
    private CameraManager cam;
    

    // Use this for initialization
    void Start () {
        gamestate = Gamestate.computer;
        cam = FindObjectOfType<CameraManager>();
    }
	
	// Update is called once per frame
	void Update () {
		

	}

    public void toComputer()
    {
        cam.lerpToLoc(new Vector3(-5.85f, 1.343f, -3.447f), new Vector3(0, 90, 0), 1.0f);
        //cam.swapCamAfterLerp(computerCam);
    }

    public void fromComputer()
    {
        cam.lerpToLoc(new Vector3(-6.581f, 1.23f, -3.388f), new Vector3(0, 0, 0), 1.0f);
    }
}
