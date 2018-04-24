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
        movieManager.generateMovie(this);
    }

    public void generateMovie(int movieNum)
    {
        movieManager.generateMovie(this, movieNum);
    }

    public void setMovie(Movie movie)
    {
        moviePicture = movie.getPicture();
        this.movie = movie;

        updateProfile();
    }

    private void updateProfile()
    {
        moviePictureSprite.sprite = moviePicture;
    }
}
