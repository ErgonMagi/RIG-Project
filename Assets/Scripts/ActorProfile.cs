using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorProfile : MonoBehaviour {

    public GameObject comedyBar, actionBar, romanceBar, horrorBar, scifiBar, otherBar;
    public Text actorNameText;
    public SpriteRenderer actorPictureSprite;

    private float comedy, action, romance, horror, scifi, other;
    private string actorName;
    private Sprite actorPicture;
    private ActorManager actorManager;
    private float t = 0;
    private bool init = true;

    private void Start()
    {
        actorManager = FindObjectOfType<ActorManager>();
    }

    private void Update()
    {
        t += Time.deltaTime;

        if(t > 1 && init)
        {
            getActor();
            init = false;
        }
    }

    public void getActor()
    {
        setActor(actorManager.generateActor());
    }

    public void getActor(int actorNum)
    {
        setActor(actorManager.generateActor(actorNum));
    }

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
        comedyBar.transform.localScale = new Vector3(comedyBar.transform.localScale.x, comedy / 30f, comedyBar.transform.localScale.z);
        actionBar.transform.localScale = new Vector3(actionBar.transform.localScale.x, action / 30f, actionBar.transform.localScale.z);
        romanceBar.transform.localScale = new Vector3(romanceBar.transform.localScale.x, romance / 30f, romanceBar.transform.localScale.z);
        horrorBar.transform.localScale = new Vector3(horrorBar.transform.localScale.x, horror / 300f, horrorBar.transform.localScale.z);
        scifiBar.transform.localScale = new Vector3(scifiBar.transform.localScale.x, scifi / 30f, scifiBar.transform.localScale.z);
        otherBar.transform.localScale = new Vector3(otherBar.transform.localScale.x, other / 30f, otherBar.transform.localScale.z);
        actorNameText.text = actorName;
        actorPictureSprite.sprite = actorPicture;
    }
}
