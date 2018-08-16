using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoviesCompletedNotification : MonoBehaviour {

    private Actor actor;
    private Movie movie;
    private string text;

    public TextMeshProUGUI textGUI;
    public Image actorImage;
    //public Image movieImage;

    public void SetNotification(Notification noti)
    {
        actor = noti.getActor();
        //movie = noti.getMovie();
        text = noti.getText();

        textGUI.text = text;
        //movieImage.sprite = movie.getPicture();
        actorImage.sprite = actor.getPicture();
    }

    public void Empty()
    {
        actor = null;
        //movie = null;
        text = null;
    }
}
