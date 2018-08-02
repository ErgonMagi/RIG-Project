using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieMenu : MonoBehaviour {

    [SerializeField]
    public GameObject[] movieProfiles;

    [SerializeField]
    public GameObject[] actorProfiles;

    public ScrollBar actorScrollBar;
    public ScrollBar movieScrollBar;

    public GameObject assignBar;

    public float swipeSpeed;

    private Player player;
    private bool firstFrame;

    private bool actorAssignedThisDrag;

    // Use this for initialization
    void Start()
    {
        player = Player.Instance;
        updateVisibility();
        actorAssignedThisDrag = false;
    }

    private void updateVisibility()
    {
        for (int i = 0; i < actorProfiles.Length; i++)
        {
            if (actorProfiles[i].GetComponent<ActorPicture>().isAssigned())
            {
                actorProfiles[i].SetActive(true);
            }
            else
            {
                actorProfiles[i].SetActive(false);
            }
        }

        for (int i = 0; i < movieProfiles.Length; i++)
        {
            if (movieProfiles[i].GetComponent<AuditionSlot>().isAssigned())
            {
                movieProfiles[i].SetActive(true);
            }
            else
            {
                movieProfiles[i].SetActive(false);
            }
        }
    }
    
    private void Update()
    {
        if(!firstFrame && !GameController.Instance.isAtComputer())
        {
            firstFrame = true;
        }
        if (firstFrame && GameController.Instance.isAtComputer())
        {
            firstFrame = false;
            allocateActors();
            allocateMovies();
            updateVisibility();
        }
    }

    public void AssignActor()
    {
        if (GameController.Instance.isAtComputer())
        {
            if (actorScrollBar.getNumObjects() > 0 && actorScrollBar.getCurrentFocus() != null && actorScrollBar.getCurrentFocus().transform.position.x < 51f && actorScrollBar.getCurrentFocus().transform.position.x > 10f && !actorAssignedThisDrag)
            {
                actorScrollBar.resetYToSwipedObject(actorScrollBar.getCurrentFocus().transform);
                GameObject a = actorScrollBar.removeFocusObject();
                if (movieScrollBar.getCurrentFocus().GetComponent<AuditionSlot>().getActor() != null)
                {
                    movieScrollBar.getCurrentFocus().GetComponent<AuditionSlot>().getActor().GetComponent<ActorPicture>().unlockPos();
                }
                a.GetComponent<ActorPicture>().lockToGameobject(movieScrollBar.getCurrentFocus().GetComponent<AuditionSlot>().getLockPos());
                movieScrollBar.getCurrentFocus().GetComponent<AuditionSlot>().setActor(a);

            }
        }
    }

    private void allocateActors()
    { 
        for (int i = 0; i < actorProfiles.Length; i++)
        {
            if (player.clients[i] != null)
            {
                actorProfiles[i].GetComponent<ActorPicture>().setActor(ref player.clients[i]);
                if(!actorScrollBar.isInList(actorProfiles[i]))
                {
                    actorScrollBar.addObject(actorProfiles[i].transform);
                }

            }
        }
    }

    private void allocateMovies()
    {
        for (int i = 0; i < movieProfiles.Length; i++)
        {
            if (!movieScrollBar.isInList(movieProfiles[i]))
            {
                movieScrollBar.addObject(movieProfiles[i].transform);
            }
        }
    }

    public void submitActors()
    {
        for(int i = 0; i < actorProfiles.Length; i++)
        {
            actorProfiles[i].GetComponent<ActorPicture>().submitActorMoviePair();
        }
    }
}
