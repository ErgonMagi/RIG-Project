using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MoviesCompletedNotification : MonoBehaviour {

    private Actor actor;
    private Movie movie;
    private string text;

    public TextMeshProUGUI textGUI;
    public TextMeshProUGUI currencyText;
    public TextMeshProUGUI XPText;
    public Image actorImage;
    public Image XPFill;
    public TextMeshProUGUI romStat, comStat, actStat, horStat, sciStat, othStat;
    public Image statSquare;

    private TextMeshProUGUI[] statsArray;

    private void Start()
    {
        statsArray = new TextMeshProUGUI[] { comStat, romStat, actStat, sciStat, horStat, othStat };
        foreach(TextMeshProUGUI t in statsArray)
        {
            t.text = "";
        }
        statSquare.enabled = false;
    }

    public void SetNotification(Notification noti)
    {
        actor = noti.getActor();
        movie = noti.getMovie();
        text = noti.getText();

        currencyText.text = noti.getMovie().getMovieCoinReward().ToString();
        XPText.text = noti.getMovie().getMovieXPReward().ToString();
        XPFill.fillAmount = actor.getExperience() / actor.getMaxExperience();
        statSquare.enabled = false;
        for (int i = 0; i < statsArray.Length; i++)
        {
            statsArray[i].text = "";
        }

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

    public void addExperience(float xp)
    {
        if(actor.getExperience() < xp)
        {
            StartCoroutine(levelUp());
        }
        else
        {
            XPFill.DOFillAmount(actor.getExperience() / actor.getMaxExperience(), 1.0f);
        }
    }



    /// <summary>
    /// Display increase in stats given by stat change
    /// </summary>
    /// <param name="statChange"> Stats listed in order: Comedy, Romance, Action, Scifi, Horror, Other </param>
    public IEnumerator levelUp()
    {
        XPFill.DOFillAmount(1, 1.0f);
        yield return new WaitForSeconds(1.0f);
        statSquare.enabled = true;
        int[] statChange = actor.getStatChange();
        for(int i = 0; i < statChange.Length; i++)
        {
            statsArray[i].text = "+" + statChange[i].ToString();
        }
        XPFill.fillAmount = 0;
        XPFill.DOFillAmount(actor.getExperience()/actor.getMaxExperience(), 1.0f);
    }
    public void Empty()
    {
        actor = null;
        //movie = null;
        text = null;
        currencyText.text = "";
        XPText.text = "";
        statSquare.enabled = false;
        for (int i = 0; i < statsArray.Length; i++)
        {
            statsArray[i].text = "";
        }
    }
}
