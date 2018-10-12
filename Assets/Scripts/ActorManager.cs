using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;

public class Results
{
    public ActorData [] results { get; set; }
}

public class Known_for
{
    public int[] genre_ids { get; set; }
}

public class Cast
{
    public int[] genre_ids { get; set; }
    public string title { get; set; }
}

public class ActorData
{
    public int id { get; set; }
    public string name { get; set; }
    public string profile_path { get; set; }
    public float popularity;
    public Known_for [] known_for { get; set; }
    public Cast [] cast { get; set; }
}

public class ActorManager : Singleton<ActorManager> {

    List<Actor> actors;

    public Sprite tempSprite;

    private List<string> takenActors;
    private ActorsNamesList actorNamesList;

    private int pendingActors;

    bool showOnce = true;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();

        actorNamesList = new ActorsNamesList();
        actorNamesList.init();

        takenActors = new List<string>();
        actors = new List<Actor>();
        pendingActors = 0;

    }

    private void Update()
    {
        //Counter to keep track of the number of actors in reserve and being downloaded
        if (actors.Count + pendingActors < 20)
        {
            StartCoroutine(downloadActorData());
            pendingActors++;
        }

        //Debugging to show when the game is ready to start
        if(actors.Count >= 5 && showOnce)
        {
            showOnce = false;
        }
    }

    //Returns the first actor in the actor reserve list
    public Actor getActor()
    {
        Actor tempActor = actors[0];
        actors.Remove(actors[0]);
        return tempActor;
    }

    //Returns how many actors are in the actor reserve list
    public int getNumActors()
    {
        return actors.Count;
    }

    //Coroutine used to start downloading an actors data
    IEnumerator downloadActorData()
    {

        //Loop to select an actor name that is not alreay in use.
        bool findingName = true;
        string actorName = null;
        int actorId = -1;
        while (findingName)
        {
            findingName = false;
            actorId = actorNamesList.getId();
            actorName = actorNamesList.getName(actorId);
            for (int i = 0; i < takenActors.Count; i++)
            {
                if (takenActors[i] == actorName)
                {
                    //findingName = true;
                }
            }

        }
        takenActors.Add(actorName);

        //Download the movies the actor is credited with
        //Coroutine pauses here until more requests can be made to TMDB

        ActorData credits = null;
        yield return StartCoroutine(TMDBRequest.Instance.FindActorCredits(actorId, data => {
            if (data != null)
            {
                credits = data;
            }      
            }));

        while(credits == null)
        {
            yield return null;
        }

        //Define the actor stats
        float action, comedy, romance, scifi, horror, other;
        action = 1;
        comedy = 1;
        romance = 1;
        scifi = 1;
        horror = 1;
        other = 1;

        if(credits.cast == null)
        {
            Debug.Log("no cast for " + actorName);
        }

        List<string> moviesList = new List<string>();

        //Searches through actors movies to create the actors stats
        for (int i = 0; i < credits.cast.Length; i++)
        {
            int[] genres = credits.cast[i].genre_ids;
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
            moviesList.Add(credits.cast[i].title);
        }

        //Normalise the actor data so it sums to 20 (Could change to sum to an amount based on the actors rank (normal, epic, legendary etc)
        float sumOfMovies = action + comedy + romance + horror + scifi + other;
        action = (float)action / sumOfMovies * 20;
        comedy = (float)comedy / sumOfMovies * 20;
        romance = (float)romance / sumOfMovies * 20;
        horror = (float)horror / sumOfMovies * 20;
        scifi = (float)scifi / sumOfMovies * 20;
        other = (float)other / sumOfMovies * 20;

        //Find the sprite of the actor
        Sprite sprite;

        if (!Directory.Exists(Application.persistentDataPath + "/actorImages"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/actorImages");
        }
        //If the sprite exists in the persistent data folder, use that sprite.
        if (File.Exists(Application.persistentDataPath + "/actorImages/" + actorName + ".png"))
        {
            byte[] imgData = File.ReadAllBytes(Application.persistentDataPath + "/actorImages/" + actorName + ".png");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgData);
            sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        else
        {
            //If it does not exist, download the sprite
            ActorData tempAct = null;
            yield return TMDBRequest.Instance.FindActor(actorId, data => {
                if (data != null)
                {
                    tempAct = data;
                }
            });

            while (tempAct == null)
            {
                yield return null;
            }

            Texture2D tex = null;
            yield return TMDBRequest.Instance.FindActorImage(tempAct.profile_path, img => {
                if (img != null)
                {
                    tex = img;
                }
            });

            //Convert downlaoded texture to a sprite
            sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            //Save the image in persistent data for future use.
            File.WriteAllBytes(Application.persistentDataPath + "/actorImages/" + actorName + ".png", tex.EncodeToPNG());
        }

        //Create actor object
        //ActorInit newInit = new ActorInit();
        //newInit.comedy = 1f;
        Actor.Init init = new Actor.Init();
        init.com = comedy;
        init.rom = romance;
        init.act = action;
        init.hor = horror;
        init.sci = scifi;
        init.other = other;
        init.name = actorName;
        init.pic = sprite;
        init.moviesActorIn = moviesList.ToArray();
        System.Random r = new System.Random();
        init.incomeVal = r.Next(5, 15);
        init.level = 1;

        Actor tempActor = new Actor(init);

        //Add actor to reserve actor list
        actors.Add(tempActor);
        pendingActors--;
        yield break;
    }
}
