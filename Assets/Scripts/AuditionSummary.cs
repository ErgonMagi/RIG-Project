using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AuditionSummary : MonoBehaviour {

    public Image actorImageRenderer;
    public Image movieImageRenderer;

    public CancelActorAssignmentButton cancelButton;

    private AuditionScreen.Audition audition;

	public void setAudition(AuditionScreen.Audition aud)
    {
        audition = aud;
        actorImageRenderer.sprite = audition.actor.getPicture();
        movieImageRenderer.sprite = audition.movie.getPicture();
        cancelButton.showButton();
    }

    public void Empty()
    {
        audition = new AuditionScreen.Audition();
        cancelButton.hideButton();
    }

    public void setArrayNum(int arrayNum)
    {
        cancelButton.setArrayNum(arrayNum);
    }

}
