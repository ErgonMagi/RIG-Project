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
        getMovie();
    }

    public void getMovie()
    {
        movieManager.generateMovie(this);
    }

    public void getMovie(int movieNum)
    {
        movieManager.generateMovie(this, movieNum);
    }

    public void setMovie(Movie movie)
    {
        moviePicture = movie.getPicture();

        updateProfile();
    }

    private void updateProfile()
    {
        moviePictureSprite.sprite = moviePicture;
    }
}
