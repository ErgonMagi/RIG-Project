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
        //Counter to ensure there is 20 movies in the reserve or downloading
        if(movieList.Count + pendingMovies < 20)
        {
            if(pendingMovies < 10)
            {
                StartCoroutine(selectMovieWithPoster());
                pendingMovies++;
            }

        }
        //Counter to ensure not too many requests are made to TMDB
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

    //Returns the number of movies in the movie reserve list
    public int getNumMovies()
    {
        return movieList.Count;
    }

    //Coroutine to download a movie
    IEnumerator selectMovieWithPoster()
    {
        //Header data to accept compressed data
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Aceept-Encoding", "gzip");

        //Create the usedMovie list
        if (usedMovies == null)
        {
            usedMovies = new List<string>();
        }

        WWW www;
        WWW img;

        //Find a movie name that hasn't been used yet.
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

        //Split the movie name into a URL format
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

        //Wait until the request limit is not reached.
        while(numCalls >= 20)
        {
            yield return null;
        }
        numCalls++;
        callsCounting = true;

        //Request movie data from TMDB
        www = new WWW("https://api.themoviedb.org/3/search/movie?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US&query=" + urlName + "&page=1&include_adult=false", null, headers);

        //Wait for download to finish.
        while (!www.isDone)
        {
            yield return null;
        }

        //Convert json file to movie data
        var json = www.text;
        MovieResults results = JsonConvert.DeserializeObject<MovieResults>(json);
        MovieData [] j = results.results;

        //Define movie stats
        bool noMatch = true;
        int action = 0;
        int comedy = 0;
        int scifi = 0;
        int romance = 0;
        int horror = 0;
        int other = 0;
        int val = 4;

        //Check that data was downloaded correctly
        if(j == null)
        {
            Debug.Log("movie not found");
        }

        //Search through movie genres to find what stats it needs to be completed
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

        //Find movie poster sprite
        Sprite sprite;
        if (!Directory.Exists(Application.persistentDataPath + "/movieImages"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/movieImages");
        }
        if (File.Exists(Application.persistentDataPath + "/movieImages/" + movieName + ".png"))
        {
            //If it is in the persistent data folder, use it.
            byte[] imgData = File.ReadAllBytes(Application.persistentDataPath + "/movieImages/" + movieName + ".png");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgData);
            sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0));
        }
        else
        {
            //If its not in persistent data folder, download it
            //Wait until tmdb request limit not reached
            while (numCalls >= 20)
            {
                yield return null;
            }
            numCalls++;
            callsCounting = true;

            //Request made to TMDB for poster image
            img = new WWW("https://image.tmdb.org/t/p/w500" + j[0].poster_path, null, headers);

            //Wait til download is done
            while (!img.isDone)
            {
                yield return null;
            }

            //Convert downloaded texure to a sprite
            sprite = Sprite.Create(img.texture, new Rect(0, 0, img.texture.width, img.texture.height), new Vector2(0, 0));

            //Save the texture in the persistent data folder.
            byte[] imgData = img.texture.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/movieImages/" + movieName + ".png", imgData);
            
        }
        //Create movie object
        Movie tempMovie = new Movie(comedy, romance, action, horror, scifi, other, j[0].title, sprite);

        //Add movie to the reserve movie list
        movieList.Add(tempMovie);
        pendingMovies--;
    }

    //Returns the first movie from the reserve movie list and removes it from the list.
    public Movie getMovie()
    {
        Movie tempMovie = movieList[0];
        movieList.Remove(movieList[0]);
        return tempMovie;
    }

}
