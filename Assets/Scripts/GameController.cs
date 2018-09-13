using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************************
 * Controls the state of the game,
 * i.e. where the camera is and what 
 * the player can interact with.
 * *************************/


public class GameController : Singleton<GameController> {

    public Camera renderCam;


    public Camera deskCam;
    public Camera computerCam;
    public Camera fileCam;
    public AuditionScreen auditionScreen;
    public Canvas DesktopCanvas;
    public Canvas IpadCanvas;
    public Camera actorBoardCam;
    public GameObject actorBoard;
    public Camera lookAtIpadCamera;
    public Camera IpadCam;

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

    private bool menuBool;
    

    // Use this for initialization
    protected override void Awake () {
        base.Awake();
        gamestate = Gamestate.desk;
        cam = CameraManager.Instance;
        mainCamStartPos = Camera.main.transform.position;
        mainCamStartRot = Camera.main.transform.rotation.eulerAngles;
        compCamPos = new Vector3(-5.85f, 1.343f, -3.447f);
        compCamRot = new Vector3(0, 90, 0);

        compState = Gamestate.mainMenu;
        displayScoreboard = false;
        menuBool = true;
    }
	
	// Update is called once per frame
	void Update () {

    }

    //Moves to computer
    public void toComputer()
    {
        if(!cam.isLerping())
        {
            DesktopCanvas.enabled = true;
            cam.lerpToLoc(compCamPos, compCamRot, 1.0f);
            gamestate = Gamestate.movieMenu;
            cam.swapCamAfterLerp(computerCam);
            auditionScreen.getActors();
            DesktopCanvas.worldCamera = computerCam;
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

    public bool isAtTicket()
    {
        return gamestate == Gamestate.actorPurchaseTicket;
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
            DesktopCanvas.enabled = false;
            IpadCanvas.enabled = false;
            cam.swapCams(deskCam);
            actorBoard.GetComponent<Collider>().enabled = true;
            cam.lerpToLoc(mainCamStartPos, mainCamStartRot, 1.0f);
            gamestate = Gamestate.desk;
        }
    }

    //Moves to file
    public void toIpad()
    {
        if (!cam.isLerping())
        {
            IpadCanvas.enabled = true;
            cam.lerpToLoc(lookAtIpadCamera.transform.position, lookAtIpadCamera.transform.rotation.eulerAngles, 1.0f);
            gamestate = Gamestate.movieMenu;
            cam.swapCamAfterLerp(computerCam);
        }
    }

    //Moves back from file
    public void fromIpad()
    {
        if (!cam.isLerping())
        {
            IpadCanvas.enabled = false;
            cam.swapCams(deskCam);
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
