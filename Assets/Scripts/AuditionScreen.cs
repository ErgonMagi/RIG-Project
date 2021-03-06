﻿using System;
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
    public ConfirmationPanel confirmationPanel;


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

    private void Update()
    {
        if(auditionList.Count ==0)
        {
            getMovies();
        }
        
    }

    //Gets the actors list from the player and tells the ui to update
    public void getActors()
    {
        actorsList = player.getAvialableActors();
        actorScrollBar.UpdateUI();
        movieScrollBar.UpdateUI();
    }

    //Currntly generates new movies but will need to change to get movies on a timed basis
    public void getMovies()
    {
        for(int i = auditionList.Count; i < MaxMoviesShown; i++)
        {
            Audition a = new Audition();
            a.movie = MovieManager.Instance.getMovie();
            a.actor = null;
            auditionList.Add(a);
        }
        movieScrollBar.UpdateUI();
    }

    //Unassigns an actor from an audition
    public void UnassignActor(int actorNum)
    {
        Audition newAudition = new Audition();

        newAudition.movie = auditionList[actorNum].movie;
        newAudition.actor = null;

        actorsList.Insert(actorScrollBar.getCurrentFocusnum(), auditionList[actorNum].actor);

        auditionList[actorNum] = newAudition;

        actorScrollBar.UpdateUI();
        movieScrollBar.UpdateUI();
        confirmationPanel.UpdateUI();
    }

    public void ShowConfirmationScreen()
    {
        actorScrollBar.UpdateUI();
        movieScrollBar.UpdateUI();
        confirmationPanel.UpdateUI();
        confirmationPanel.ShowConfirmationScreen();
    }

    public void HideConfirmationScreen()
    {
        actorScrollBar.UpdateUI();
        movieScrollBar.UpdateUI();
        confirmationPanel.UpdateUI();
        confirmationPanel.HideConfirmationScreen();
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
        confirmationPanel.UpdateUI();

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

        for(int i = 0; i < removeList.Count; i++)
        {
            auditionList.Remove(removeList[i]);
        }

        confirmationPanel.HideConfirmationScreen();

        getMovies();

        actorScrollBar.UpdateUI();
        movieScrollBar.UpdateUI();
        confirmationPanel.UpdateUI();
    }

    public int getSubmissionPrice()
    {
        int price = 0;
        for (int i = 0; i < auditionList.Count; i++)
        {
            if (auditionList[i].actor != null)
            {
                price += auditionList[i].movie.getAuditionPrice();
            }
        }
        return price;
    }
}
