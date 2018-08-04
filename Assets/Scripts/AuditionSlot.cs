using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Audition slots are the gameobjects that hold potential auditions for the player.

public class AuditionSlot : MonoBehaviour {

    public Image movieImageRenderer;
    public Image actorImageRenderer;
    private AuditionScreen.Audition audition;
        
    public void setAudition(AuditionScreen.Audition a)
    {
        audition = a;
        movieImageRenderer.sprite = a.movie.getPicture();
        if(audition.actor != null)
        {
            actorImageRenderer.enabled = true;
            actorImageRenderer.sprite = audition.actor.getPicture();
        }
        else
        {
            actorImageRenderer.enabled = false;
        }
    }

    public bool hasMovie()
    {
        return audition.movie != null;
    }
    
    public void Empty()
    {
        audition = new AuditionScreen.Audition();
    }
   
}
