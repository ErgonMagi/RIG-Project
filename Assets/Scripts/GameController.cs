using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************
 * Controls the state of the game,
 * i.e. where the camera is and what 
 * the player can interact with.
 * *************************/


public class GameController : Singleton<GameController> {

    public Camera deskCam;
    public Camera computerCam;
    public Camera fileCam;
    public MovieMenu movieMenu;
    public Camera actorBoardCam;
    public GameObject actorBoard;

    private List<GameObject> menus;
    private Vector3 mainCamStartPos;
    private Vector3 mainCamStartRot;
    private Vector3 compCamPos;
    private Vector3 compCamRot;

    private bool cameraLocked;
    private bool playerCanClick;

    private enum Gamestate
    {
        desk, mainMenu, file, movieMenu, actorBoard, actorPurchaseTicket
    }

    private bool displayScoreboard;
    private Gamestate gamestate;
    private CameraManager cam;
    private Gamestate compState;
    

    // Use this for initialization
    protected override void Awake () {
        base.Awake();
        gamestate = Gamestate.desk;
        cam = CameraManager.Instance;
        mainCamStartPos = Camera.main.transform.position;
        mainCamStartRot = Camera.main.transform.rotation.eulerAngles;
        compCamPos = new Vector3(-5.85f, 1.343f, -3.447f);
        compCamRot = new Vector3(0, 90, 0);


        menus = new List<GameObject>();

        //menus.Add(actorStatsMenu.gameObject);
        menus.Add(movieMenu.gameObject);

        compState = Gamestate.mainMenu;
        displayScoreboard = false;
    }
	
	// Update is called once per frame
	void Update () {


        for(int i = 0; i < menus.Count; i++)
        {
            if(menus[i].transform.position.y < 75)
                menus[i].transform.position += new Vector3(0, 100, 0);
        }

        switch(gamestate)
        {
            case Gamestate.movieMenu:
                if (movieMenu.gameObject.transform.position.y > 75)
                    movieMenu.gameObject.transform.position -= new Vector3(0, 100, 0);
                break;
            case Gamestate.desk:
                switch(compState)
                {
                    case Gamestate.mainMenu:
                        break;
                    case Gamestate.movieMenu:
                        if (movieMenu.gameObject.transform.position.y > 75)
                            movieMenu.gameObject.transform.position -= new Vector3(0, 100, 0);
                        break;
                }
                break;

        }

    }

    //Moves to computer
    public void toComputer()
    {
        if(!cam.isLerping())
        {
            cam.lerpToLoc(compCamPos, compCamRot, 1.0f);
            gamestate = Gamestate.movieMenu;
            cam.swapCamAfterLerp(computerCam);
        }
    }


    public void toActorBoard()
    {
        if (!cam.isLerping())
        {
            cam.swapCams(deskCam);
            cam.lerpToLoc(actorBoardCam.transform.position, actorBoardCam.transform.rotation.eulerAngles, 1.0f);
            actorBoard.GetComponent<Collider>().enabled = false;
            gamestate = Gamestate.actorBoard;
        }
    }

    public void toActorTicket(GameObject camera)
    {
        CameraManager.Instance.lerpToLoc(camera.transform.position, camera.transform.rotation.eulerAngles, 1.0f);
        gamestate = Gamestate.actorPurchaseTicket;
    }


    //Moves back to desk
    public void fromComputer()
    {
        if (!cam.isLerping())
        {
            if(gamestate == Gamestate.actorPurchaseTicket)
            {
                toActorBoard();
                return;
            }
            cam.swapCams(deskCam);
            actorBoard.GetComponent<Collider>().enabled = true;
            cam.lerpToLoc(mainCamStartPos, mainCamStartRot, 1.0f);
            gamestate = Gamestate.desk;
        }
    }

    //Moves to file
    public void toFile()
    {
        if (!cam.isLerping())
        {
            cam.lerpToLoc(fileCam.transform.position, fileCam.transform.rotation.eulerAngles, 1.0f);
            gamestate = Gamestate.file;
        }
    }

    //Moves back from file
    public void fromFile()
    {
        if (!cam.isLerping())
        {
            cam.lerpToLoc(mainCamStartPos, mainCamStartRot, 1.0f);
            gamestate = Gamestate.desk;
        }
    }

    //Shows scoreboard
    public void displayingScoreboard()
    {
        displayScoreboard = true;
    }


    //Hides scoreboard
    public void hideScoreboard()
    {
        displayScoreboard = false;
    }

    //Returns whether freelook is enabled
    public bool canFreeLook()
    {
        if(cameraLocked)
        {
            return false;
        }
        switch(gamestate)
        {
            case Gamestate.desk:
                return true;
        }
        return false;
    }

    //Returns if at the actor board
    public bool isAtBoard()
    {
        return gamestate == Gamestate.actorBoard;
    }

    //Returns if at the computer
    public bool isAtComputer()
    {
        return gamestate == Gamestate.movieMenu;
    }

    //Returns if at file
    public bool isAtFile()
    {
        return gamestate == Gamestate.file;
    }

    //Returns if player is at the desk
    public bool isAtDesk()
    {
        return gamestate == Gamestate.desk;
    }

    //Resets scene for new week
    public void newWeek()
    {
        fromComputer();
    }

    //Returns is scoreboard is displaying
    public bool isScoreboardShowing()
    {
        return displayScoreboard;
    }

    //Locks the camera
    public void lockCamera()
    {
        cameraLocked = true;
    }

    //Unlocks the camera
    public void unlockCamera()
    {
        cameraLocked = false;
    }

    //returns if clicking is enabled
    public bool canClick()
    {
        return playerCanClick;
    }

    //Locks clicking
    public void lockClicking()
    {
        playerCanClick = false;
    }

    //Unlocks clicking
    public void unlockClicking()
    {
        playerCanClick = true;
    }
}
