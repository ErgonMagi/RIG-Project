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
        timer += Time.deltaTime;

        if(callsCounting)
        {
            if(timer > 10)
            {
                numCalls = 0;
                callsCounting = false;
            }
        }

        if (actors.Count + pendingActors < 20)
        {
            StartCoroutine(downloadActorData());
            pendingActors++;
        }

        if(actors.Count >= 5 && showOnce)
        {
            Debug.Log("Enough actors");
            showOnce = false;
        }
    }

    public Actor getActor()
    {
        Actor tempActor = actors[0];
        actors.Remove(actors[0]);
        return tempActor;
    }

    public int getNumActors()
    {
        return actors.Count;
    }

    IEnumerator downloadActorData()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Aceept-Encoding", "gzip");

        ActorData[] actorData = null;

        ActorsNamesList actorNamesList = new ActorsNamesList();
        bool findingName = true;
        string actorName = null;
        while(findingName)
        {
            findingName = false;
            actorName = actorNamesList.getName();
            for(int i = 0; i < takenActors.Count; i++)
            {
                if(takenActors[i] == actorName)
                {
                    //findingName = true;
                }
            }
            
        }
        takenActors.Add(actorName);
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
        Debug.Log(urlName);
        while(numCalls >= 20)
        {
            yield return null;
        }
        WWW www = new WWW("https://api.themoviedb.org/3/search/person?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US&query=" + urlName + "&page=1&include_adult=false", null, headers);
        callsCounting = true;
        numCalls++;

        while (!www.isDone)
        {
            yield return null;
        }

        secondSearchBytes += www.bytesDownloaded;

        Results result = JsonConvert.DeserializeObject<Results>(www.text);
        if(result == null)
        {
            Debug.Log("No results found for " + actorName);
        }
        actorData = result.results;
        if (actorData == null)
        {
            Debug.Log(actorName + " is not showing in tmdb");
        }
        float action, comedy, romance, scifi, horror, other;
        action = 1;
        comedy = 1;
        romance = 1;
        scifi = 1;
        horror = 1;
        other = 1;
        
        if(actorData.Length == 0)
        {
            Debug.Log("nothing in data");
        }

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

        float sumOfMovies = action + comedy + romance + horror + scifi + other;
        action = (float)action / sumOfMovies * 20;
        comedy = (float)comedy / sumOfMovies * 20;
        romance = (float)romance / sumOfMovies * 20;
        horror = (float)horror / sumOfMovies * 20;
        scifi = (float)scifi / sumOfMovies * 20;
        other = (float)other / sumOfMovies * 20;

        while (numCalls >= 20)
        {
            yield return null;
        }
        WWW img = new WWW("https://image.tmdb.org/t/p/w185" + actorData[0].profile_path, null, headers);
        callsCounting = true;
        numCalls++;

        while (!img.isDone)
        {
            yield return null;
        }

        imgBytes += img.bytesDownloaded;

        Sprite sprite = Sprite.Create(img.texture, new Rect(0, 0, img.texture.width, img.texture.height), new Vector2(0.5f,0.5f));


        Actor tempActor = new Actor(comedy, romance, action, horror, scifi, other, actorData[0].name, sprite);

        actors.Add(tempActor);
        pendingActors--;
        yield break;
    }

}
