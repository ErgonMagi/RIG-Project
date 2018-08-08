using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Audition slots are the gameobjects that hold potential auditions for the player.

public class AuditionSlot : MonoBehaviour {

    public Image movieImageRenderer;
    public Image actorImageRenderer;
    public CancelActorAssignmentButton cancelButton;
    private AuditionScreen.Audition audition;

    private int arrayNumber;
        
    public void setAudition(AuditionScreen.Audition a)
    {
        audition = a;
        movieImageRenderer.sprite = a.movie.getPicture();
        if(audition.actor != null)
        {
            actorImageRenderer.enabled = true;
            actorImageRenderer.sprite = audition.actor.getPicture();
            cancelButton.showButton();

        }
        else
        {
            actorImageRenderer.enabled = false;
            cancelButton.hideButton();
        }
    }

    public void setArrayNum(int i)
    {
        cancelButton.setArrayNum(i);
    }


    public bool hasMovie()
    {
        return audition.movie != null;
    }
    
    public void Empty()
    {
        audition = new AuditionScreen.Audition();
        cancelButton.hideButton();
    }
}
