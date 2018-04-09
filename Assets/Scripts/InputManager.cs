using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

public class InputManager : MonoBehaviour
{

    public float scrollSpeed;
    public float swipeDrag;
    public float swipeNullPoint;

    private Vector3 tapPosition;
    private Vector3 startTapPosition;
    private Vector3 newMousePosition;
    private Vector3 swipeVelocity;
    Vector3 swipeDir;

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
            tapPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            startTapPosition = tapPosition;
        }

        if(Input.GetMouseButton(0))
        {
            newMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            swipeDir = newMousePosition - tapPosition;
            tapPosition = newMousePosition;
            Camera.main.transform.Rotate(new Vector3(swipeDir.y * scrollSpeed, -swipeDir.x * scrollSpeed, 0));
        }

        if(!Input.GetMouseButton(0))
        {
            //Debug.Log(swipeDir.magnitude);
            if(swipeDir.magnitude*100 > swipeNullPoint)
            {
                Camera.main.transform.Rotate(new Vector3(swipeDir.y * scrollSpeed, -swipeDir.x * scrollSpeed, 0));
                swipeDir.Scale(new Vector3(swipeDrag, swipeDrag, 0));
            }
        }

    }
}