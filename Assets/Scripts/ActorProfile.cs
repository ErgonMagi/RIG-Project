using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorProfile : MonoBehaviour {

    public GameObject comedyBar, actionBar, romanceBar, horrorBar, scifiBar, otherBar;
    public Text comedyVal, romanceVal, actionVal, horrorVal, scifiVal, otherVal;
    public Text actorNameText;
    public SpriteRenderer actorPictureSprite;

    private float comedy, action, romance, horror, scifi, other;
    private string actorName;
    private Sprite actorPicture;
    private ActorManager actorManager;
    private float t = 0;
    private bool init;

    private void Start()
    {
        actorManager = FindObjectOfType<ActorManager>();
        init = true;
    }

    private void Update()
    {
        t += Time.deltaTime;

        if(t > 1 && init)
        {
            init = false;
            getActor();           
        }
    }

    public void getActor()
    {
        actorManager.generateActor(this);
    }

    public void getActor(int actorNum)
    {
        actorManager.generateActor(this, actorNum);
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
        comedyBar.transform.localScale = new Vector3((int)comedy / 30f, comedyBar.transform.localScale.y,  comedyBar.transform.localScale.z);
        actionBar.transform.localScale = new Vector3((int)action / 30f, actionBar.transform.localScale.y,  actionBar.transform.localScale.z);
        romanceBar.transform.localScale = new Vector3((int)romance / 30f, romanceBar.transform.localScale.y,  romanceBar.transform.localScale.z);
        horrorBar.transform.localScale = new Vector3((int)horror / 30f, horrorBar.transform.localScale.y,  horrorBar.transform.localScale.z);
        scifiBar.transform.localScale = new Vector3((int)scifi / 30f, scifiBar.transform.localScale.y,  scifiBar.transform.localScale.z);
        otherBar.transform.localScale = new Vector3((int)other / 30f, otherBar.transform.localScale.y,  otherBar.transform.localScale.z);
        actorNameText.text = actorName;
        comedyVal.transform.localScale = new Vector3(1 / comedyBar.transform.localScale.x * 0.6f, 1 / comedyBar.transform.localScale.y * 0.04f, 1 / comedyBar.transform.localScale.z);
        romanceVal.transform.localScale = new Vector3(1 / romanceBar.transform.localScale.x *0.6f, 1 / romanceBar.transform.localScale.y * 0.04f, 1 / romanceBar.transform.localScale.z);
        actionVal.transform.localScale = new Vector3(1 / actionBar.transform.localScale.x * 0.6f, 1 / actionBar.transform.localScale.y * 0.04f, 1 / actionBar.transform.localScale.z);
        horrorVal.transform.localScale = new Vector3(1 / horrorBar.transform.localScale.x * 0.6f, 1 / horrorBar.transform.localScale.y * 0.04f, 1 / horrorBar.transform.localScale.z);
        scifiVal.transform.localScale = new Vector3(1 / scifiBar.transform.localScale.x * 0.6f, 1 / scifiBar.transform.localScale.y * 0.04f, 1 / scifiBar.transform.localScale.z);
        otherVal.transform.localScale = new Vector3(1 / otherBar.transform.localScale.x * 0.6f, 1 / otherBar.transform.localScale.y * 0.04f, 1 / otherBar.transform.localScale.z);
        comedyVal.transform.localPosition = new Vector3(8.3f / comedyBar.transform.localScale.x, 0, 0);
        romanceVal.transform.localPosition = new Vector3(8.3f / romanceBar.transform.localScale.x, 0, 0);
        actionVal.transform.localPosition = new Vector3(8.3f / actionBar.transform.localScale.x, 0, 0);
        horrorVal.transform.localPosition = new Vector3(8.3f / horrorBar.transform.localScale.x, 0, 0);
        scifiVal.transform.localPosition = new Vector3(8.3f / scifiBar.transform.localScale.x, 0, 0);
        otherVal.transform.localPosition = new Vector3(8.3f / otherBar.transform.localScale.x, 0, 0);
        comedyVal.text = ((int)comedy).ToString();
        romanceVal.text = ((int)romance).ToString();
        horrorVal.text = ((int)horror).ToString();
        scifiVal.text = ((int)scifi).ToString();
        otherVal.text = ((int)other).ToString();
        actionVal.text = ((int)action).ToString();
        actorPictureSprite.sprite = actorPicture;
    }
}
