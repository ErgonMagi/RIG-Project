using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AuditionSummary : MonoBehaviour {

    public Image actorImageRenderer;
    public Image movieImageRenderer;
    public TextMeshProUGUI price;

    public CancelActorAssignmentButton cancelButton;

    private AuditionScreen.Audition audition;

    private bool isEmpty = true;

    public void setAudition(AuditionScreen.Audition aud)
    {
        audition = aud;
        actorImageRenderer.sprite = audition.actor.getPicture();
        movieImageRenderer.sprite = audition.movie.getPicture();
        price.text = audition.movie.getAuditionPrice().ToString();
        isEmpty = false;
        cancelButton.showButton();
    }

    public void Empty()
    {
        audition = new AuditionScreen.Audition();
        price.text = "";
        isEmpty = true;
        cancelButton.hideButton();
    }

    public void setArrayNum(int arrayNum)
    {
        cancelButton.setArrayNum(arrayNum);
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

}
