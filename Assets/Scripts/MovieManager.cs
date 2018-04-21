using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Genres
{
    public int id { get; set; }
    public string name { get; set; }
}

public class MovieData
{
    string backdrop_path { get; set; }
    public Genres[] genres { get; set; }
    public string poster_path { get; set; }
    public string name { get; set; }
}

public class MovieManager : MonoBehaviour {

    

    // Use this for initialization
    void Start () {

    }
	
    public void generateMovie(MovieProfile requestingProfile, int movieNum = -1)
    {
        StartCoroutine(selectMovieWithPoster(requestingProfile, movieNum));
    }

    IEnumerator selectMovieWithPoster(MovieProfile requestingProfile, int movieNum = -1)
    {
        WWW www;
        WWW img;
        MovieData j = new MovieData();

        bool findMovieWithPoster = true;
        while (findMovieWithPoster)
        {
            int rand = (int)Random.Range(0, 10000);
            www = new WWW("https://api.themoviedb.org/3/movie/" + rand + "?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US");

            while (!www.isDone)
            {
                yield return null;
            }
            var json = www.text;
            j = JsonConvert.DeserializeObject<MovieData>(json);
            if (j.poster_path != null)
            {
                findMovieWithPoster = false;
            }
        }
        bool noMatch = true;
        int action = 0;
        int comedy = 0;
        int scifi = 0;
        int romance = 0;
        int horror = 0;
        int other = 0;
        int val = 4;
        for(int i = 0;  i < j.genres.Length; i++)
        {
            //Action
            if (j.genres[i].id == 28)
            {
                action = val;
                noMatch = false;
            }
            //Comedy
            if (j.genres[i].id == 35)
            {
                comedy = val;
                noMatch = false;
            }
            //Romance
            if (j.genres[i].id == 10749)
            {
                romance = val;
                noMatch = false;
            }
            //Scifi
            if (j.genres[i].id == 878)
            {
                scifi = val;
                noMatch = false;
            }
            //Horror
            if (j.genres[i].id == 27)
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

        img = new WWW("https://image.tmdb.org/t/p/w500" + j.poster_path);
        while(!img.isDone)
        {
            yield return null;
        }

        Sprite sprite = Sprite.Create(img.texture, new Rect(0, 0, img.texture.width, img.texture.height), new Vector2(0, 0));

        Movie tempMovie = new Movie(comedy, romance, action, horror, scifi, other, j.name, sprite);

        requestingProfile.setMovie(tempMovie);

    }

}
