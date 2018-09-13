using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActorStatsMenu : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    public ActorStatsPage[] actorPages;
    public float swipeDist;

    public int centreActorNum;
    private bool swiping = false;
    Vector3 mousePosDown, mousePosUp;

	// Use this for initialization
	void Start () {
        centreActorNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerUp(PointerEventData pointer)
    {
        mousePosUp = Input.mousePosition;

        //Swipe right
        if(mousePosUp.x - mousePosDown.x > swipeDist)
        {
            if(centreActorNum < Player.Instance.getActorsList().Count-1)
            {
                centreActorNum++;
            }
        }

        //Swipe left
        if (mousePosUp.x - mousePosDown.x < -swipeDist)
        {
            if (centreActorNum > 0)
            {
                centreActorNum--;
            }
        }

        Debug.Log("Menu updated");

        UpdateMenu();
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        swiping = true;
        mousePosDown = Input.mousePosition;
        mousePosUp = Input.mousePosition;
    }

    private void UpdateMenu()
    {
        actorPages[1].setActor(Player.Instance.getActorsList()[centreActorNum]);
    }
}
