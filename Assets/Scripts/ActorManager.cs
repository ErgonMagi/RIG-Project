using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMDbLib;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.People;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.General;

public class ActorManager : MonoBehaviour {

    TMDbClient client;
    List<Actor> actors;

    public Sprite tempSprite;
    public ActorProfile ap;

    private int action, comedy, romance, scifi, horror, other;

	// Use this for initialization
	void Start () {
        action = 1;
        comedy = 1;
        romance = 1;
        scifi = 1;
        horror = 1;
        other = 1;

        client = new TMDbClient("e2ffb845e5d5fca810eaf5054914f41b");
        actors = new List<Actor>();
        List<SearchPerson> sp = client.SearchPersonAsync("Alicia Vikander").Result.Results;
        List<KnownForBase> known = sp[0].KnownFor;

        for(int i = 0; i < known.Count; i++)
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
            if(noMatch)
            {
                other++;
            }
                
        }

        int sumOfMovies = action + comedy + romance + horror + scifi + other;
        action = (int)Mathf.Round((float)action / sumOfMovies * 20);
        comedy = (int)Mathf.Round((float)comedy / sumOfMovies * 20);
        romance = (int)Mathf.Round((float)romance / sumOfMovies * 20);
        horror = (int)Mathf.Round((float)horror / sumOfMovies * 20);
        scifi = (int)Mathf.Round((float)scifi / sumOfMovies * 20);
        other = (int)Mathf.Round((float)other / sumOfMovies * 20);

        Sprite spriteSearch = Resources.Load<Sprite>("Actor images/" + sp[0].Name);

        if(spriteSearch == null)
        {
            spriteSearch = tempSprite;
        }

        Actor tempActor = new Actor(comedy, romance, action, horror, scifi, other, sp[0].Name, spriteSearch);

        actors.Add(tempActor);

        ap.setActor(actors[0]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
