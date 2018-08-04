using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionScreen : MonoBehaviour {

    public struct Audition
    {
        public Movie movie;
        public Actor actor;
    }

    //Ui variables
    public ActorScrollBar actorScrollBar;
    public MovieScrollBar movieScrollBar;


    //Data variables
    public List<Actor> actorsList;
    private List<Audition> auditionList;

    //Player specific data
    public int MaxMoviesShown;


    private Player player;

    // Use this for initialization
    void Start()
    {
        player = Player.Instance;
        actorsList = new List<Actor>();
        auditionList = new List<Audition>();
        getActors();
        getMovies();
    }

    public void getActors()
    {
        actorsList = player.getActorsList();
        actorScrollBar.UpdateUI();
        movieScrollBar.UpdateUI();
    }

    public void getMovies()
    {
        for(int i = 0; i < MaxMoviesShown; i++)
        {
            Audition a = new Audition();
            a.movie = MovieManager.Instance.getMovie();
            a.actor = null;
            auditionList.Add(a);
        }
        movieScrollBar.UpdateUI();
    }

    public List<Actor> getActorsList()
    {
        return actorsList;
    }

    public List<Audition> getAuditionsList()
    {
        return auditionList;
    }

    public void AssignActorToAudition(int actorArrayIndex)
    {
        Audition newAudition = new Audition();
        int movieFocusNum = movieScrollBar.getfocusNum();
        newAudition.movie = auditionList[movieFocusNum].movie;
        newAudition.actor = actorsList[actorArrayIndex];

        if (auditionList[movieFocusNum].actor != null)
        {
            actorsList[actorArrayIndex] = auditionList[movieFocusNum].actor;
        }
        else
        {
            actorsList.Remove(actorsList[actorArrayIndex]);
        }


        auditionList[movieFocusNum] = newAudition;  
        
        actorScrollBar.UpdateUI();
        movieScrollBar.UpdateUI();

    }

    public void submitActors()
    {
        List<Audition> removeList = new List<Audition>();
        for(int i = 0; i < auditionList.Count; i++)
        {
            if (auditionList[i].actor != null)
            {
                Task t = new Task(auditionList[i].actor,  auditionList[i].movie, 2, true);
                FindObjectOfType<TaskManager>().addTask(t);
                removeList.Add(auditionList[i]);
            }
            
        }
    }
}
