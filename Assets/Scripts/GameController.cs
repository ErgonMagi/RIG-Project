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
    public GameObject mainMenu;

    private ActorStatsMenu actorStatsMenu;
    private MovieMenu movieMenu;
    private List<GameObject> menus;
    private ScoreManager scoreManager;
    public Vector3 mainCamStartPos = new Vector3(-6.581f, 1.23f, -3.388f);
    public Vector3 mainCamLookingAtCompScreenPos = new Vector3(-6.581f, 1.23f, -3.388f);

    private enum Gamestate
    {
        desk, mainMenu, statsMenu, movieMenu
    }

    private bool displayScoreboard;
    private Gamestate gamestate;
    private CameraManager cam;
    private Gamestate compState;
    

    // Use this for initialization
    void Start () {
        gamestate = Gamestate.desk;
        cam = FindObjectOfType<CameraManager>();
        actorStatsMenu = FindObjectOfType<ActorStatsMenu>();
        movieMenu = FindObjectOfType<MovieMenu>();
        scoreManager = FindObjectOfType<ScoreManager>();

        menus = new List<GameObject>();

        //menus.Add(actorStatsMenu.gameObject);
        menus.Add(movieMenu.gameObject);
        menus.Add(mainMenu);

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
            case Gamestate.mainMenu: 
                if(mainMenu.transform.position.y > 75)
                     mainMenu.transform.position -= new Vector3(0, 100, 0);
                break;
            case Gamestate.movieMenu:
                if (movieMenu.gameObject.transform.position.y > 75)
                    movieMenu.gameObject.transform.position -= new Vector3(0, 100, 0);
                scoreManager.Start();
                break;
            case Gamestate.statsMenu:
                if (actorStatsMenu.gameObject.transform.position.y > 75)
                    actorStatsMenu.gameObject.transform.position -= new Vector3(0, 100, 0);
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

    public void toComputer()
    {
        if(!cam.isLerping())
        {
            cam.lerpToLoc(new Vector3(-5.85f, 1.343f, -3.447f), new Vector3(0, 90, 0), 1.0f);
            gamestate = Gamestate.movieMenu;
            cam.swapCamAfterLerp(computerCam);
        }
    }

    public void fromComputer()
    {
        if (!cam.isLerping())
        {
            cam.swapCams(deskCam);
            cam.lerpToLoc(new Vector3(-6.581f, 1.23f, -3.388f), new Vector3(0, 90, 0), 1.0f);
            gamestate = Gamestate.desk;
        }
    }

    public void openStatsMenu()
    {
        if (!cam.isLerping())
        {
            gamestate = Gamestate.statsMenu;
            compState = Gamestate.statsMenu;
            actorStatsMenu.gameObject.transform.position += new Vector3(0, -100, 0);
        }
    }

    public void closeMenu()
    {
        if (!cam.isLerping())
        {
            gamestate = Gamestate.mainMenu;
            compState = Gamestate.mainMenu;
        }
    }

    public void openMovieMenu()
    {
        if (!cam.isLerping())
        {
            gamestate = Gamestate.movieMenu;
            compState = Gamestate.movieMenu;
            
        }
    }

    public void displayingScoreboard()
    {
        displayScoreboard = true;
    }

    public void hideScoreboard()
    {
        displayScoreboard = false;
    }

    public bool canFreeLook()
    {
        switch(gamestate)
        {
            case Gamestate.desk:
                return true;
        }
        return false;
    }

    public bool isInStatsMenu()
    {
        return gamestate == Gamestate.statsMenu;
    }

    public bool isInMovieMenu()
    {
        return gamestate == Gamestate.movieMenu;
    }

    public bool isAtDesk()
    {
        return gamestate == Gamestate.desk;
    }

    public void newWeek()
    {
        closeMenu();
        fromComputer();
        movieMenu.resetMovieMenu();
    }

    public bool isScoreboardShowing()
    {
        return displayScoreboard;
    }

}
