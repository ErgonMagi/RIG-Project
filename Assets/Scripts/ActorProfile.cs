using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorProfile : MonoBehaviour {

    public GameObject comedyBar, actionBar, romanceBar, horrorBar, scifiBar, otherBar;
    public Text actorNameText;
    public SpriteRenderer actorPictureSprite;

    private int comedy, action, romance, horror, scifi, other;
    private string actorName;
    private Sprite actorPicture;

    public void setActor(Actor actor)
    {
        comedy = actor.getComedy();
        romance = actor.getRomance();
        action = actor.getAction();
        horror = actor.getHorror();
        scifi = actor.getScifi();
        other = actor.getOther();
        actorName = actor.getName();
        actorPicture = actor.getPicture();

        updateProfile();
    }

    private void updateProfile()
    {
        comedyBar.transform.localScale = new Vector3(comedyBar.transform.localScale.x, (float)comedy / 30f, comedyBar.transform.localScale.z);
        actionBar.transform.localScale = new Vector3(actionBar.transform.localScale.x, (float)action / 30f, actionBar.transform.localScale.z);
        romanceBar.transform.localScale = new Vector3(romanceBar.transform.localScale.x, (float)romance / 30f, romanceBar.transform.localScale.z);
        horrorBar.transform.localScale = new Vector3(horrorBar.transform.localScale.x, (float)horror / 300f, horrorBar.transform.localScale.z);
        scifiBar.transform.localScale = new Vector3(scifiBar.transform.localScale.x, (float)scifi / 30f, scifiBar.transform.localScale.z);
        otherBar.transform.localScale = new Vector3(otherBar.transform.localScale.x, (float)other / 30f, otherBar.transform.localScale.z);
        actorNameText.text = actorName;
        actorPictureSprite.sprite = actorPicture;
    }
}
