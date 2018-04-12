using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.General;

public class InputManager : MonoBehaviour
{

    public float scrollSpeed;
    public float swipeDrag;
    public float swipeNullPoint;
    public float maxClickDuration;

    private Vector3 tapPosition;
    private Vector3 startTapPosition;
    private Vector3 newMousePosition;
    private Vector3 swipeVelocity;
    Vector3 swipeDir;
    private ClickableObject clickedObject;
    float clickTimer;

    TMDbClient client;

    // Use this for initialization
    void Start()
    {
        client = new TMDbClient("e2ffb845e5d5fca810eaf5054914f41b");

        Movie movie = client.GetMovieAsync(47964).Result;
        Debug.Log(movie.Title);
    }

    // Update is called once per frame
    void Update()
    {



        if(Input.GetMouseButtonDown(0))
        {
            clickTimer = 0.0f;

            tapPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            startTapPosition = tapPosition;

            Vector3 dirFromCam = tapPosition - Camera.main.transform.position;


            RaycastHit [] hit = Physics.RaycastAll(tapPosition, dirFromCam, 15.0f, 1 << LayerMask.NameToLayer("Clickable"));
            foreach(RaycastHit h in hit)
            {
                clickedObject = h.collider.gameObject.GetComponent<ClickableObject>();
                if(clickedObject != null)
                {
                    break;
                }
            }
            
        }

        if(Input.GetMouseButton(0))
        {
            clickTimer += Time.deltaTime;

            newMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            swipeDir = newMousePosition - tapPosition;
            tapPosition = newMousePosition;
            Camera.main.transform.Rotate(new Vector3(swipeDir.y * scrollSpeed, -swipeDir.x * scrollSpeed, 0));
        }

        if(!Input.GetMouseButton(0))
        {
            if(clickTimer < maxClickDuration && clickedObject != null)
            {
                clickedObject.onClick();
                clickedObject = null;
            }

            //Debug.Log(swipeDir.magnitude);
            if(swipeDir.magnitude*100 > swipeNullPoint)
            {
                Camera.main.transform.Rotate(new Vector3(swipeDir.y * scrollSpeed, -swipeDir.x * scrollSpeed, 0));
                swipeDir.Scale(new Vector3(swipeDrag, swipeDrag, 0));
            }
        }

    }
}