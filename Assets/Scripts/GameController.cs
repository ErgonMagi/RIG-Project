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
 

    private enum Gamestate
    {
        desk, mainMenu, statsMenu, movieMenu
    }

    private Gamestate gamestate;
    private CameraManager cam;
    private Gamestate compState;
    

    // Use this for initialization
    void Start () {
        gamestate = Gamestate.desk;
        cam = FindObjectOfType<CameraManager>();
        actorStatsMenu = FindObjectOfType<ActorStatsMenu>();
        movieMenu = FindObjectOfType<MovieMenu>();

        menus = new List<GameObject>();

        menus.Add(actorStatsMenu.gameObject);
        menus.Add(movieMenu.gameObject);
        menus.Add(mainMenu);

        compState = Gamestate.mainMenu;
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
                ScoreManager.Start();
                break;
            case Gamestate.statsMenu:
                if (actorStatsMenu.gameObject.transform.position.y > 75)
                    actorStatsMenu.gameObject.transform.position -= new Vector3(0, 100, 0);
                break;
            case Gamestate.desk:
                switch(compState)
                {
                    case Gamestate.mainMenu:
                        if (mainMenu.transform.position.y > 75)
                            mainMenu.transform.position -= new Vector3(0, 100, 0);
                        break;
                    case Gamestate.movieMenu:
                        if (movieMenu.gameObject.transform.position.y > 75)
                            movieMenu.gameObject.transform.position -= new Vector3(0, 100, 0);
                        break;
                    case Gamestate.statsMenu:
                        if (actorStatsMenu.gameObject.transform.position.y > 75)
                            actorStatsMenu.gameObject.transform.position -= new Vector3(0, 100, 0);
                        break;
                }
                break;

        }

    }

    public void toComputer()
    {
        cam.lerpToLoc(new Vector3(-5.85f, 1.343f, -3.447f), new Vector3(0, 90, 0), 1.0f);
        gamestate = Gamestate.mainMenu;
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
        compState = Gamestate.statsMenu;
        actorStatsMenu.gameObject.transform.position += new Vector3(0, -100, 0);
    }

    public void closeMenu()
    {
        gamestate = Gamestate.mainMenu;
        compState = Gamestate.mainMenu;
    }

    public void openMovieMenu()
    {
        gamestate = Gamestate.movieMenu;
        compState = Gamestate.movieMenu;
        movieMenu.gameObject.transform.position += new Vector3(0, -100, 0);
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

}
