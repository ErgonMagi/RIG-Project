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
    public bool adult { get; set; }
    string backdrop_path { get; set; }
    public string belongs_to_collection { get; set; }
    public int budget { get; set; }
    public Genres[] genres { get; set; }
    public string poster_path { get; set; }
}

public class MovieManager : MonoBehaviour {

    WWW www;
    WWW img;
    MovieData j;
    private bool downloadStarted = true;

    // Use this for initialization
    void Start () {
        getMovie();
    }
	
    public MovieData getMovie()
    {
        bool findMovieWithPoster = true;
        while (findMovieWithPoster)
        {
           
            int rand = (int)Random.Range(0, 10000);
            www = new WWW("https://api.themoviedb.org/3/movie/" + rand + "?api_key=e2ffb845e5d5fca810eaf5054914f41b&language=en-US");
            while (!www.isDone)
            {
                Debug.Log("loading");
            }
            var json = www.text;
            j = JsonConvert.DeserializeObject<MovieData>(json);
            if(j.poster_path != null)
            {
                findMovieWithPoster = false;
            }
        }
        

        downloadStarted = false;
        return j;
    }

	// Update is called once per frame
	void Update () {

        if(downloadStarted == false)
        {
            img = new WWW("https://image.tmdb.org/t/p/w92" + j.poster_path);
            downloadStarted = true;
        }
        
        if (img != null && img.isDone)
        {
            Sprite sprite = Sprite.Create(img.texture, new Rect(0, 0, img.texture.width, img.texture.height), new Vector2(0, 0));

            this.GetComponent<SpriteRenderer>().sprite = sprite;
        }

    }
}
