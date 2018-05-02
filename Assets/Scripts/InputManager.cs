using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*****************************
The code in charge of getting the input
from the player and calling the appropriate 
objects
******************************/


public class InputManager : MonoBehaviour
{

    public float scrollSpeed;
    public float swipeDrag;
    public float swipeNullPoint;
    public float maxClickDuration;

    private Vector3 tapPosition;
    Vector2 swipeDir;
    private ClickableObject clickedObject;
    private CameraManager cameraManager;
    private GameController gameController;
    private ActorStatsMenu actorStatsMenu;
    private MovieMenu movieMenu;
    private Vector2 mouseScreenPos;
    private float swipeLength;
    private bool objectSelected;

    // Use this for initialization
    void Start()
    {

        cameraManager = FindObjectOfType<CameraManager>();
        gameController = FindObjectOfType<GameController>();
        actorStatsMenu = FindObjectOfType<ActorStatsMenu>();
        movieMenu = FindObjectOfType<MovieMenu>();
        objectSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController.isScoreboardShowing())
        {
            //Called when lmb is clicked
            if (Input.GetMouseButtonDown(0))
            {
                //Finds the position of the mouse in pixels and in world coordinates
                mouseScreenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                tapPosition = cameraManager.getCam().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.getCam().nearClipPlane));
                swipeLength = 0;

                //Draws Vector3 dirFromCam a ray from the current camera through the mouse to check for interactable objects
                Vector3 dirFromCam = new Vector3(0, 0, 1);
                if (!cameraManager.getCam().orthographic)
                {
                    dirFromCam = tapPosition - cameraManager.getCam().transform.position;
                }

                RaycastHit[] hit = Physics.RaycastAll(tapPosition, dirFromCam, 15.0f, 1 << LayerMask.NameToLayer("Clickable"));


                //For every object hit, check if it is interactables
                foreach (RaycastHit h in hit)
                {
                    clickedObject = h.collider.gameObject.GetComponent<ClickableObject>();
                    if (h.collider.gameObject.GetComponent<ActorPicture>() != null)
                    {
                        objectSelected = true;
                        clickedObject.onClick();
                    }
                    if (clickedObject != null)
                    {
                        break;                  //If an interactable object is found, it is "selected" and the loop ends.
                    }                           //The selectable object will be activated if the player does not swipe (See mouse release code)
                }

            }

            //Runs on any frame the lmb is held down
            if (Input.GetMouseButton(0))
            {
                //Calculates how far the player has swiped in this frame as pixels
                swipeDir = mouseScreenPos - new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                //Sums the total swipe length to check if a player is tapping or swiping
                swipeLength += swipeDir.magnitude;
                if (!objectSelected)
                {
                    if (gameController.canFreeLook())
                    {
                        //Rotate the camera around itself in the direction of swiping
                        cameraManager.getCam().transform.RotateAround(cameraManager.getCam().transform.position, transform.up, swipeDir.x * scrollSpeed);
                        cameraManager.getCam().transform.RotateAround(cameraManager.getCam().transform.position, cameraManager.getCam().transform.right, -swipeDir.y * scrollSpeed);
                    }
                    else if (gameController.isInStatsMenu())
                    {
                        actorStatsMenu.scroll(swipeDir.x);
                    }
                    else if (gameController.isInMovieMenu())
                    {
                        movieMenu.scroll(swipeDir.x);
                    }
                }
                //Update the mouse position to the new positon.
                mouseScreenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            //Runs on any frame the lmb is not held down.
            if (!Input.GetMouseButton(0))
            {
                if (!objectSelected)
                {
                    //If there was an object selected by the last click and the player did not swipe, activate the object and then deselect it.
                    if (clickedObject != null && swipeLength < 10)
                    {
                        clickedObject.onClick();
                        clickedObject = null;
                    }

                    //Swipe deceleration.
                    if (gameController.canFreeLook())
                    {
                        if (swipeDir.magnitude * 100 > swipeNullPoint)
                        {
                            cameraManager.getCam().transform.RotateAround(cameraManager.getCam().transform.position, transform.up, swipeDir.x * scrollSpeed);
                            cameraManager.getCam().transform.RotateAround(cameraManager.getCam().transform.position, cameraManager.getCam().transform.right, -swipeDir.y * scrollSpeed);

                            swipeDir.Scale(new Vector3(swipeDrag, swipeDrag, 0));
                        }
                    }
                }
                objectSelected = false;
                clickedObject = null;
            }
        }
    }
}