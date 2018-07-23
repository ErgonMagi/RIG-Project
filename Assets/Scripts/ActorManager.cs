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

public class ActorData
{
    public string name { get; set; }
    public string profile_path { get; set; }
    public float popularity;
    public Known_for [] known_for { get; set; }
}

public class ActorManager : MonoBehaviour {

    private int firstSearchBytes, secondSearchBytes, imgBytes;

    List<Actor> actors;

    public Sprite tempSprite;

    private List<string> takenActors;

    private int pendingActors;
    private int numCalls;

    private bool callsCounting = false;

    private float timer;

    bool showOnce = true;

	// Use this for initialization
	void Awake () {

        DontDestroyOnLoad(gameObject);

        takenActors = new List<string>();
        actors = new List<Actor>();
        pendingActors = 0;
        firstSearchBytes = 0;
        secondSearchBytes = 0;
        imgBytes = 0;
        numCalls = 0;

    }

    private void Update()
    {
        
        //Counter to keep track of number of requests to TMDB to stay under the limit of 40 per 10 seconds
        if(callsCounting)
        {
            timer += Time.deltaTime;
            if (timer > 10)
            {
                numCalls = 0;
                callsCounting = false;
            }
        }

        //Counter to keep track of the number of actors in reserve and being downloaded
        if (actors.Count + pendingActors < 20)
        {
            StartCoroutine(downloadActorData());
            pendingActors++;
        }

        //Debugging to show when the game is ready to start
        if(actors.Count >= 5 && showOnce)
        {
            Debug.Log("Enough actors");
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
        //Header info to request compressed data
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Aceept-Encoding", "gzip");

        ActorData[] actorData = null;

        //Loop to select an actor name that is not alreay in use.
        ActorsNamesList actorNamesList = new ActorsNamesList();
        bool findingName = true;
        string actorName = null;
        while (findingName)
        {
            findingName = false;
            actorName = actorNamesList.getName();
            for (int i = 0; i < takenActors.Count; i++)
            {
                if (takenActors[i] == actorName)
                {
                    //findingName = true;
                }
            }

        }
        takenActors.Add(actorName);

        //Break actor name into a url format
        string[] nameSplit = actorName.Split(' ');
        string urlName = null;
        for (int i = 0; i < nameSplit.Length; i++)
        {
            urlName += nameSplit[i];
            if (i < nameSplit.Length - 1)
            {
                urlName += "%20";
            }

        }
        
        //Coroutine pauses here until more requests can be made to TMDB
        while (numCalls >= 20)
        {
            yield return null;
        }
        callsCounting = true;
        numCalls++;

        //Request actor data from TMDB
        WWW www = new WWW("https://api.themoviedb.org/3/search/person?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US&query=" + urlName + "&page=1&include_adult=false", null, headers);
        
        //Pauses here until download is done
        while (!www.isDone)
        {
            yield return null;
        }

        //Convert the search results into actor data
        Results result = JsonConvert.DeserializeObject<Results>(www.text);
        if (result == null)
        {
            Debug.Log("No results found for " + actorName);
        }
        actorData = result.results;
        if (actorData == null)
        {
            Debug.Log(actorName + " is not showing in tmdb");
        }

        //Define the actor stats
        float action, comedy, romance, scifi, horror, other;
        action = 1;
        comedy = 1;
        romance = 1;
        scifi = 1;
        horror = 1;
        other = 1;

        //Check to make sure data loaded correctly
        if (actorData.Length == 0)
        {
            Debug.Log("nothing in data");
        }

        //Searches through actors movies to create the actors stats
        for (int i = 0; i < actorData[0].known_for.Length; i++)
        {
            int[] genres = actorData[0].known_for[i].genre_ids;
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
            //Coroutine pauses here until request limit is lowered
            while (numCalls >= 20)
            {
                yield return null;
            }
            callsCounting = true;
            numCalls++;

            //Request to TMDB for actor image
            WWW img = new WWW("https://image.tmdb.org/t/p/w185" + actorData[0].profile_path, null, headers);
        
            //Wait until download is done
            while (!img.isDone)
            {
                yield return null;
            }

            //Convert downlaoded texture to a sprite
            sprite = Sprite.Create(img.texture, new Rect(0, 0, img.texture.width, img.texture.height), new Vector2(0.5f, 0.5f));

            //Save the image in persistent data for future use.
            File.WriteAllBytes(Application.persistentDataPath + "/actorImages/" + actorName + ".png", img.texture.EncodeToPNG());
        }

        //Create actor object
        Actor tempActor = new Actor(comedy, romance, action, horror, scifi, other, actorData[0].name, sprite);

        //Add actor to reserve actor list
        actors.Add(tempActor);
        pendingActors--;
        yield break;
    }

}
