using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieMenu : MonoBehaviour {

    [SerializeField]
    public GameObject[] movieProfiles;

    [SerializeField]
    public GameObject[] actorProfiles;

    public float swipeSpeed;

    private Player player;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player>();
        StartCoroutine(allocateActors());
    }

    private void Update()
    {
        for (int i = 0; i < actorProfiles.Length; i++)
        {
            if(actorProfiles[i].GetComponent<ActorPicture>().isAssigned())
            {
                actorProfiles[i].SetActive(true);
            }
            else
            {
                actorProfiles[i].SetActive(false);
            }
        }
    }

    public void resetMovieMenu()
    {
        for(int i = 0; i < movieProfiles.Length; i++)
        {
            movieProfiles[i].GetComponent<MovieProfile>().generateMovie();
        }
        for (int i = 0; i < actorProfiles.Length; i++)
        {
            actorProfiles[i].GetComponent<ActorPicture>().Reset();
        }
    }

    IEnumerator allocateActors()
    {
        for (int i = 0; i < actorProfiles.Length; i++)
        {
            while(player.getActor(i) == null)
            {
                yield return null;
            }
            actorProfiles[i].GetComponent<ActorPicture>().setActor(player.getActor(i));
        }
    }

    public void submitActors()
    {
        for(int i = 0; i < actorProfiles.Length; i++)
        {
            actorProfiles[i].GetComponent<ActorPicture>().submitActorMoviePair();
        }
    }

    public void scroll(float scrollLength)
    {
        for (int i = 0; i < movieProfiles.Length; i++)
        {
            movieProfiles[i].transform.localPosition -= new Vector3(scrollLength * swipeSpeed, 0, 0);
            if (movieProfiles[i].transform.localPosition.x >= 12)
            {
                movieProfiles[i].transform.localPosition -= new Vector3(33f, 0, 0);
            }
            else if (movieProfiles[i].transform.localPosition.x <= -17)
            {
                movieProfiles[i].transform.localPosition += new Vector3(33f, 0, 0);
            }
        }
    }
}
