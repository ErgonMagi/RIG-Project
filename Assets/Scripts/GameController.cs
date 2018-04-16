using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        cam.lerpToLoc(new Vector3(0, 1, -5.2f), new Vector3(0, 0, 0), 1.0f);
        cam.swapCamAfterLerp(computerCam);
    }

    public void fromComputer()
    {
        cam.lerpToLoc(new Vector3(0, 1, -10f), new Vector3(0, 0, 0), 1.0f);
    }
}
