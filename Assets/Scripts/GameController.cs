using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************
 * Controls the state of the game,
 * i.e. where the camera is and what 
 * the player can interact with.
 * *************************/


public class GameController : MonoBehaviour {

    public Camera deskCam;
    public Camera computerCam;

    private ActorStatsMenu actorStatsMenu;

    private enum Gamestate
    {
        desk, computer, statsMenu
    }

    private Gamestate gamestate;
    private CameraManager cam;
    

    // Use this for initialization
    void Start () {
        gamestate = Gamestate.desk;
        cam = FindObjectOfType<CameraManager>();
        actorStatsMenu = FindObjectOfType<ActorStatsMenu>();
        actorStatsMenu.gameObject.transform.position += new Vector3(0, 100, 0);
    }
	
	// Update is called once per frame
	void Update () {
		

	}

    public void toComputer()
    {
        cam.lerpToLoc(new Vector3(-5.85f, 1.343f, -3.447f), new Vector3(0, 90, 0), 1.0f);
        gamestate = Gamestate.computer;
        cam.swapCamAfterLerp(computerCam);
    }

    public void fromComputer()
    {
        cam.swapCams(deskCam);
        cam.lerpToLoc(new Vector3(-6.581f, 1.23f, -3.388f), new Vector3(0, 90, 0), 1.0f);
        gamestate = Gamestate.desk;
    }

    public void openStatsMenu()
    {
        gamestate = Gamestate.statsMenu;
        actorStatsMenu.gameObject.transform.position += new Vector3(0, -100, 0);
    }

    public void closeStatesMenu()
    {
        gamestate = Gamestate.computer;
        actorStatsMenu.gameObject.transform.position += new Vector3(0, 100, 0);

    }

    public bool canFreeLook()
    {
        switch(gamestate)
        {
            case Gamestate.desk:
                return true;
            case Gamestate.computer:
                return false;
        }
        return false;
    }

    public bool isInStatsMenu()
    {
        return gamestate == Gamestate.statsMenu;
    }

}
