using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.General;
using System.IO;

public class ActorManager : MonoBehaviour {

    TMDbClient client;
    List<Actor> actors;

    public Sprite tempSprite;
    public ActorProfile ap;

    private float action, comedy, romance, scifi, horror, other;
    private List<string> actorNames;

	// Use this for initialization
	void Start () {
        
        client = new TMDbClient("e2ffb845e5d5fca810eaf5054914f41b");
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
        List<SearchPerson> sp = client.SearchPersonAsync(actorNames[actorNum]).Result.Results;
        List<KnownForBase> known = sp[0].KnownFor;

        for (int i = 0; i < known.Count; i++)
        {
            List<Genre> genres = client.GetMovieAsync(known[i].Id).Result.Genres;
            bool noMatch = true;
            for (int j = 0; j < genres.Count; j++)
            {
                if (genres[j].Name == "Action")
                {
                    action++;
                    noMatch = false;
                }
                if (genres[j].Name == "Comedy")
                {
                    comedy++;
                    noMatch = false;
                }
                if (genres[j].Name == "Romance")
                {
                    romance++;
                    noMatch = false;
                }
                if (genres[j].Name == "Science Fiction")
                {
                    scifi++;
                    noMatch = false;
                }
                if (genres[j].Name == "Horror")
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

        Sprite spriteSearch = Resources.Load<Sprite>("Actor images/" + sp[0].Name);

        if (spriteSearch == null)
        {
            spriteSearch = tempSprite;
        }

        Actor tempActor = new Actor(comedy, romance, action, horror, scifi, other, sp[0].Name, spriteSearch);

        actors.Add(tempActor);

        return tempActor;
    }

}
