using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager {

    static List<Tuple <Actor, Movie>> actorMoviePair;

    public static void Start()
    {
        actorMoviePair = new List<Tuple<Actor, Movie>>();
    }

    public static void calculateScores()
    {
        bool[] passList = new bool[actorMoviePair.Count];
        for(int i = 0; i < actorMoviePair.Count; i++)
        {
            bool pass = true;
            if (actorMoviePair[i].Item1.getAction() < actorMoviePair[i].Item2.getAction())
            {
                pass = false;
            }
            if (actorMoviePair[i].Item1.getComedy() < actorMoviePair[i].Item2.getComedy())
            {
                pass = false;
            }
            if (actorMoviePair[i].Item1.getHorror() < actorMoviePair[i].Item2.getHorror())
            {
                pass = false;
            }
            if (actorMoviePair[i].Item1.getScifi() < actorMoviePair[i].Item2.getScifi())
            {
                pass = false;
            }
            if (actorMoviePair[i].Item1.getRomance() < actorMoviePair[i].Item2.getRomance())
            {
                pass = false;
            }
            if (actorMoviePair[i].Item1.getOther() < actorMoviePair[i].Item2.getOther())
            {
                pass = false;
            }
            if(pass)
            {
                passList[i] = true;
            }
            else
            {
                passList[i] = false;
            }
        }

        //Print winners
        for(int i = 0; i < passList.Length; i++)
        {
            if(passList[i])
            {
                Debug.Log(actorMoviePair[i].Item1.getName() + " was successful in their application for a role in " + actorMoviePair[i].Item2.getTitle());

            }
            else
            {
                Debug.Log(actorMoviePair[i].Item1.getName() + " was unsuccesful in their application for a role in " + actorMoviePair[i].Item2.getTitle());
            }
        }

    }

    public static void setPair(Actor actor, Movie movie)
    {
        actorMoviePair.Add(Tuple.Create(actor, movie));
    }

}
