using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Audition slots are the gameobjects that hold potential auditions for the player.

public class AuditionSlot : MonoBehaviour {

    public SpriteRenderer moviePictureSprite;
    private Sprite moviePicture;
    private MovieManager movieManager;
    private Movie movie;


    //On start a random movie is assigned
    //Note: this will need to be made persistent.
    private void Start()
    {
        movieManager = MovieManager.Instance;
        generateMovie();
    }

    //Returns the movie associated with this profile.
    public Movie getMovie()
    {
        return movie;
    }

    //Assigns a new movie to the audition slot.
    public void generateMovie()
    {
        Movie tempMovie = movieManager.getMovie();

        moviePicture = tempMovie.getPicture();
        this.movie = tempMovie;

        moviePictureSprite.sprite = moviePicture;

    }
}
