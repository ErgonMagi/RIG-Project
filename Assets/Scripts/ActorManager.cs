﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMDbLib.Objects.General;
using System.IO;

public class Results
{
    public ActorData [] results { get; set; }
}


public class Known_for
{
    public int[] genre_ids { get; set; }
}

public class ActorData
{
    public string name { get; set; }
    public Known_for [] known_for { get; set; }
}

public class ActorManager : MonoBehaviour {

    List<Actor> actors;

    public Sprite tempSprite;
    public ActorProfile ap;

    private float action, comedy, romance, scifi, horror, other;
    private List<string> actorNames;

	// Use this for initialization
	void Start () {
        
        actorNames = new List<string>();


        string filePath = Path.GetFullPath("Assets/ActorNames.txt");

        StreamReader reader = new StreamReader(filePath);

        while(!reader.EndOfStream)
        {
            actorNames.Add(reader.ReadLine());
        }

        reader.Close();
       
    }
	
    public Actor generateActor(int actorNum = -1)
    {
        if(actorNum == -1)
        {
            actorNum = (int)Random.Range(0, actorNames.Count);
            if(actorNum == actorNames.Count)
            {
                actorNum--;
            }
        }

        action = 1;
        comedy = 1;
        romance = 1;
        scifi = 1;
        horror = 1;
        other = 1;

        actors = new List<Actor>();
        string urlName = System.Uri.EscapeUriString(actorNames[actorNum]);
        WWW www = new WWW("https://api.themoviedb.org/3/search/person?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US&query=" + urlName + "&page=1&include_adult=false");

        while(!www.isDone)
        {
            Debug.Log("loading");
        }

        Results result = JsonConvert.DeserializeObject<Results>(www.text);
        ActorData [] actorData = result.results;

        for (int i = 0; i < actorData[0].known_for.Length; i++)
        {
            int [] genres = actorData[0].known_for[i].genre_ids;
            bool noMatch = true;
            for (int j = 0; j < genres.Length; j++)
            {
                //Action
                if (genres[j] == 28)
                {
                    action++;
                    noMatch = false;
                }
                //Comedy
                if (genres[j] == 35)
                {
                    comedy++;
                    noMatch = false;
                }
                //Romance
                if (genres[j] == 10749)
                {
                    romance++;
                    noMatch = false;
                }
                //Scifi
                if (genres[j] == 878)
                {
                    scifi++;
                    noMatch = false;
                }
                //Horror
                if (genres[j] == 27)
                {
                    horror++;
                    noMatch = false;
                }
            }
            if (noMatch)
            {
                other++;
            }

        }

        float sumOfMovies = action + comedy + romance + horror + scifi + other;
        action = (float)action / sumOfMovies * 20;
        comedy = (float)comedy / sumOfMovies * 20;
        romance = (float)romance / sumOfMovies * 20;
        horror = (float)horror / sumOfMovies * 20;
        scifi = (float)scifi / sumOfMovies * 20;
        other = (float)other / sumOfMovies * 20;

        Sprite spriteSearch = Resources.Load<Sprite>("Actor images/" + actorData[0].name);

        if (spriteSearch == null)
        {
            spriteSearch = tempSprite;
        }

        Actor tempActor = new Actor(comedy, romance, action, horror, scifi, other, actorData[0].name, spriteSearch);

        actors.Add(tempActor);

        return tempActor;
    }

}