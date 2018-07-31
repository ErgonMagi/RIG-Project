using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMDBRequest : Singleton<TMDBRequest> {

    Dictionary<string, string> headers = new Dictionary<string, string>();
    private int numCalls;
    private bool callsCounting;
    private float timer;

    // Use this for initialization
    protected override void Awake () {

        base.Awake();

        //Header info to request compressed data       
        headers.Add("Aceept-Encoding", "gzip");
        numCalls = 0;
        callsCounting = false;
    }

    private void Update()
    {
        if(callsCounting)
        {
            timer += Time.deltaTime;
            if(timer >= 10)
            {
                callsCounting = false;
                numCalls = 0;
                timer = 0;
            }
        }
    }

    public IEnumerator FindActor(int actorId, System.Action<ActorData> returnMethod)
    {
        while (numCalls >= 40)
        {
            yield return null;
        }
        callsCounting = true;
        numCalls++;
        WWW actor = new WWW("https://api.themoviedb.org/3/person/" + actorId + "?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US", null, headers);

        while (!actor.isDone)
        {
            yield return null;
        }

        ActorData actorData = JsonConvert.DeserializeObject<ActorData>(actor.text);

        returnMethod(actorData);
    }

    public IEnumerator FindActorCredits(int actorId, System.Action<ActorData> returnMethod)
    {
        while (numCalls >= 40)
        {
            yield return null;
        }
        callsCounting = true;
        numCalls++;
        WWW credits = new WWW("https://api.themoviedb.org/3/person/" + actorId + "/movie_credits?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US", null, headers);

        while(!credits.isDone)
        {
            yield return null;
        }

        ActorData actorData = JsonConvert.DeserializeObject<ActorData>(credits.text);

        returnMethod(actorData);
    }

    public IEnumerator FindActorImage(string profilePath, System.Action<Texture2D> returnMethod)
    {
        //Request to TMDB for actor image
        WWW img = new WWW("https://image.tmdb.org/t/p/w185" + profilePath, null, headers);

        while (!img.isDone)
        {
            yield return null;
        }

        returnMethod(img.texture);
    }

    public IEnumerator FindMovie(string urlName, System.Action<MovieData> returnMethod)
    {
        //Wait until the request limit is not reached.
        while (numCalls >= 40)
        {
            yield return null;
        }
        numCalls++;
        callsCounting = true;

        //Request movie data from TMDB
        WWW www = new WWW("https://api.themoviedb.org/3/search/movie?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US&query=" + urlName + "&page=1&include_adult=false", null, headers);

        //Wait for download to finish.
        while (!www.isDone)
        {
            yield return null;
        }

        //Convert json file to movie data
        var json = www.text;
        MovieResults results = JsonConvert.DeserializeObject<MovieResults>(json);
        MovieData[] j = results.results;

        returnMethod(j[0]);
    }

    public IEnumerator FindMovieImage(string posterPath, System.Action<Texture2D> returnMethod)
    {
        WWW img = new WWW("https://image.tmdb.org/t/p/w500" + posterPath, null, headers);

        
        //Wait til download is done
        while (!img.isDone)
        {
            yield return null;
        }

        returnMethod(img.texture);
    }
}


