using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class MovieResults
{
    public MovieData[] results { get; set; }
}

public class MovieData
{
    string backdrop_path { get; set; }
    public int[] genre_ids { get; set; }
    public string poster_path { get; set; }
    public string title { get; set; }
    public bool adult { get; set; }
    public string original_language { get; set; }
}

public class MovieManager : MonoBehaviour {

    private List<Movie> movieList;
    private List<string> usedMovies;
    private int pendingMovies;
    private int numCalls = 0;
    private bool callsCounting = false;
    private float timer = 0;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(gameObject);

        movieList = new List<Movie>();
        pendingMovies = 0;
    }

    private void Update()
    {
        if(movieList.Count + pendingMovies < 20)
        {
            if(pendingMovies < 10)
            {
                StartCoroutine(selectMovieWithPoster());
                pendingMovies++;
            }

        }
        if(callsCounting)
        {
            timer += Time.deltaTime;
            if(timer > 10)
            {
                timer = 0;
                callsCounting = false;
                numCalls = 0;
            }
        }
    }

    public int getNumMovies()
    {
        return movieList.Count;
    }

    IEnumerator selectMovieWithPoster()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Aceept-Encoding", "gzip");

        if (usedMovies == null)
        {
            usedMovies = new List<string>();
        }

        WWW www;
        WWW img;

        MovieNameList movieNameList = new MovieNameList();
        bool findingMovie = true;
        string movieName = null;
        while(findingMovie)
        {
            findingMovie = false;
            movieName = movieNameList.getName();
            for(int i = 0; i < usedMovies.Count; i++)
            {
                if(usedMovies[i] == movieName)
                {
                    //findingMovie = true;
                }
            }
        }

        usedMovies.Add(movieName);
        string [] splitName = movieName.Split(' ');
        string urlName = null;
        for(int i = 0; i < splitName.Length; i++)
        {
            urlName += splitName[i];
            if(i < splitName.Length -1)
            {
                urlName += "%20";
            }
        }

        while(numCalls >= 20)
        {
            yield return null;
        }
        numCalls++;
        callsCounting = true;
        www = new WWW("https://api.themoviedb.org/3/search/movie?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US&query=" + urlName + "&page=1&include_adult=false", null, headers);

        while (!www.isDone)
        {
            yield return null;
        }
        var json = www.text;
        MovieResults results = JsonConvert.DeserializeObject<MovieResults>(json);
        MovieData [] j = results.results;

        bool noMatch = true;
        int action = 0;
        int comedy = 0;
        int scifi = 0;
        int romance = 0;
        int horror = 0;
        int other = 0;
        int val = 4;
        if(j == null)
        {
            Debug.Log("movie not found");
        }
        for(int i = 0;  i < j[0].genre_ids.Length; i++)
        {
            //Action
            if (j[0].genre_ids[i] == 28)
            {
                action = val;
                noMatch = false;
            }
            //Comedy
            if (j[0].genre_ids[i] == 35)
            {
                comedy = val;
                noMatch = false;
            }
            //Romance
            if (j[0].genre_ids[i] == 10749)
            {
                romance = val;
                noMatch = false;
            }
            //Scifi
            if (j[0].genre_ids[i] == 878)
            {
                scifi = val;
                noMatch = false;
            }
            //Horror
            if (j[0].genre_ids[i] == 27)
            {
                horror = val;
                noMatch = false;
            }
            val--;
        }
        if(noMatch)
        {
            other = 4;
        }

        Sprite sprite;
        if (!Directory.Exists(Application.persistentDataPath + "/movieImages"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/movieImages");
        }
        if (File.Exists(Application.persistentDataPath + "/movieImages/" + movieName + ".png"))
        {
            byte[] imgData = File.ReadAllBytes(Application.persistentDataPath + "/movieImages/" + movieName + ".png");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgData);
            sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
        }
        else
        {
            while (numCalls >= 20)
            {
                yield return null;
            }
            numCalls++;
            callsCounting = true;
            img = new WWW("https://image.tmdb.org/t/p/w500" + j[0].poster_path, null, headers);
            while (!img.isDone)
            {
                yield return null;
            }

            sprite = Sprite.Create(img.texture, new Rect(0, 0, img.texture.width, img.texture.height), new Vector2(0, 0));

            byte[] imgData = img.texture.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/movieImages/" + movieName + ".png", imgData);
            
        }
        Movie tempMovie = new Movie(comedy, romance, action, horror, scifi, other, j[0].title, sprite);

        movieList.Add(tempMovie);
        pendingMovies--;
    }


    public Movie getMovie()
    {
        Debug.Log(movieList[0].getTitle() + " was removed.");
        Movie tempMovie = movieList[0];
        movieList.Remove(movieList[0]);
        return tempMovie;
    }

}
