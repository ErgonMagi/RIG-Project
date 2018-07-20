using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieProfile : MonoBehaviour {

    public SpriteRenderer moviePictureSprite;
    private Sprite moviePicture;
    private MovieManager movieManager;
    private Movie movie;


    private void Start()
    {
        movieManager = FindObjectOfType<MovieManager>();
        generateMovie();
    }

    public Movie getMovie()
    {
        return movie;
    }

    public void generateMovie()
    {
        Movie tempMovie = movieManager.getMovie();

        moviePicture = tempMovie.getPicture();
        this.movie = tempMovie;

        moviePictureSprite.sprite = moviePicture;

    }
}
