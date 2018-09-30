using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationSlot : MonoBehaviour {

    private Actor actor;
    private Movie movie;
    private string text;
    public TextMeshProUGUI repText;

    public TextMeshProUGUI textGUI;
    public Image actorImage;
    //public Image movieImage;

	public void SetNotification(Notification noti)
    {
        actor = noti.getActor();
        //movie = noti.getMovie();
        text = noti.getText();

        if(noti.isPassed())
        {
            repText.text = noti.getMovie().getAuditionRepReward().ToString();
        }
        else
        {
            repText.text = "0";
        }

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
