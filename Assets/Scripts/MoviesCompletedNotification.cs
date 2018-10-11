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
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI XPText;
    public Image actorImage;
    public Image XPFill;


    public void SetNotification(Notification noti)
    {
        actor = noti.getActor();
        movie = noti.getMovie();
        text = noti.getText();

        currencyText.text = noti.getMovie().getMovieCoinReward().ToString();
        XPText.text = noti.getMovie().getMovieXPReward().ToString();
        XPFill.fillAmount = actor.getExperience() / actor.getMaxExperience();

        textGUI.text = text;
        //movieImage.sprite = movie.getPicture();
        actorImage.sprite = actor.getPicture();
    }

    public Actor getActor()
    {
        return actor;
    }

    public Movie getMovie()
    {
        return movie;
    }

    public Image getXPFillImage()
    {
        return XPFill;
    }

    public void Empty()
    {
        actor = null;
        //movie = null;
        text = null;
        currencyText.text = "";
        XPText.text = "";
    }
}
