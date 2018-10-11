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

public class MovieManager : Singleton<MovieManager> {

    private List<Movie> movieList;
    private List<string> usedMovies;
    private int pendingMovies;
    private int numCalls = 0;
    private bool callsCounting = false;
    private float timer = 0;

    // Use this for initialization
    protected override void Awake () {
        base.Awake();

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
        //Create the usedMovie list
        if (usedMovies == null)
        {
            usedMovies = new List<string>();
        }

        //Find a movie name that hasn't been used yet.
        MovieNameList movieNameList = new MovieNameList();
        bool findingMovie = true;
        string movieName = null;
        while (findingMovie)
        {
            findingMovie = false;
            movieName = movieNameList.getName();
            for (int i = 0; i < usedMovies.Count; i++)
            {
                if (usedMovies[i] == movieName)
                {
                    //findingMovie = true;
                }
            }
        }
        usedMovies.Add(movieName);


        //Split the movie name into a URL format
        string[] splitName = movieName.Split(' ');
        string urlName = null;
        for (int i = 0; i < splitName.Length; i++)
        {
            urlName += splitName[i];
            if (i < splitName.Length - 1)
            {
                urlName += "%20";
            }
        }

        MovieData movieData = null;

        yield return StartCoroutine(TMDBRequest.Instance.FindMovie(urlName, data => {
            if (data != null)
            {
                movieData = data;
            }
        }));

        while (movieData == null)
        {
            yield return null;
        }

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
        if (movieData == null)
        {
            Debug.Log("movie not found");
        }

        //Search through movie genres to find what stats it needs to be completed
        for (int i = 0; i < movieData.genre_ids.Length; i++)
        {
            //Action
            if (movieData.genre_ids[i] == 28)
            {
                action = val;
                noMatch = false;
            }
            //Comedy
            if (movieData.genre_ids[i] == 35)
            {
                comedy = val;
                noMatch = false;
            }
            //Romance
            if (movieData.genre_ids[i] == 10749)
            {
                romance = val;
                noMatch = false;
            }
            //Scifi
            if (movieData.genre_ids[i] == 878)
            {
                scifi = val;
                noMatch = false;
            }
            //Horror
            if (movieData.genre_ids[i] == 27)
            {
                horror = val;
                noMatch = false;
            }
            val--;
        }
        if (noMatch)
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
            Texture2D img = null;
            yield return StartCoroutine(TMDBRequest.Instance.FindMovieImage(movieData.poster_path, data =>
            {
                if (data != null)
                {
                    img = data;
                }
            }));

            while (img == null)
            {
                yield return null;
            }
            //Convert downloaded texure to a sprite
            sprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0, 0));

            //Save the texture in the persistent data folder.
            byte[] imgData = img.EncodeToPNG();
            File.WriteAllBytes(Application.persistentDataPath + "/movieImages/" + movieName + ".png", imgData);

        }
        //Create movie object
        Movie.init m = new Movie.init()
        {
            comedy = comedy,
            romance = romance,
            action = action,
            horror = horror,
            scifi = scifi,
            other = other,
            title = movieData.title,
            picture = sprite,
            coinReward = 100,
            price = 20,
            repReward = 100,
            xpReward = 100,
        };
        Movie tempMovie = new Movie(m);

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
