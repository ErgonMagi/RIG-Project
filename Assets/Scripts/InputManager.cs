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
    private CameraManager cameraManager;
    private GameController gameController;
    private AuditionScreen auditionScreen;
    private Vector2 mouseScreenPos;


    // Use this for initialization
    void Start()
    {
        cameraManager = CameraManager.Instance;
        gameController = GameController.Instance;
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

            }

            //Runs on any frame the lmb is held down
            if (Input.GetMouseButton(0))
            {
                //Calculates how far the player has swiped in this frame as pixels
                swipeDir = mouseScreenPos - new Vector2(Input.mousePosition.x, Input.mousePosition.y); 

                //Check if player can swipe
                if (gameController.canFreeLook())
                {
                    //Rotate the camera around itself in the direction of swiping
                    cameraManager.getCam().transform.RotateAround(cameraManager.getCam().transform.position, transform.up, swipeDir.x * scrollSpeed);
                    cameraManager.getCam().transform.RotateAround(cameraManager.getCam().transform.position, cameraManager.getCam().transform.right, -swipeDir.y * scrollSpeed);
                }

                //Update the mouse position to the new positon.
                mouseScreenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            //Runs on any frame the lmb is not held down.
            if (!Input.GetMouseButton(0))
            {
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
        }
    }
}