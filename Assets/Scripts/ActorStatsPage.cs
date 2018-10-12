using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ActorStatsPage : MonoBehaviour {

    public Image comedyBar, actionBar, romanceBar, horrorBar, scifiBar, otherBar;
    public TextMeshProUGUI comedyVal, romanceVal, actionVal, horrorVal, scifiVal, otherVal;
    public TextMeshProUGUI actorNameText, availabilityText;
    public Image actorPictureSprite;
    public Image XPFill;
    public Image availabilityBubble;
    public TextMeshProUGUI actorLevel;
    public int actorNum;

    public float comedy, action, romance, horror, scifi, other;
    private string actorName;
    private Sprite actorPicture;
    private Actor actor;
    private Player player;
    private List<string> actorNames;

    private GameController gc;

    private LevelManager lm;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        actor = null;

        gc = FindObjectOfType<GameController>();
        lm = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if(actor == null)
        {
            getActorFromPlayer();
        }       
    }

    public Actor getActor()
    {
        return actor;
    }

    public void getActorFromPlayer()
    {
        Actor temp = player.getActor(actorNum);
        if(temp != null)
            setActor(temp);
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
        this.actor = actor;

        XPFill.fillAmount = actor.getExperience() / actor.getMaxExperience();

        updateProfile();
    }

    private void updateProfile()
    {
        comedyBar.fillAmount = (int)comedy / 30f;
        actionBar.fillAmount = (int)action / 30f;
        romanceBar.fillAmount = (int)romance / 30f;
        horrorBar.fillAmount = (int)horror / 30f;
        scifiBar.fillAmount = (int)scifi / 30f;
        otherBar.fillAmount = (int)other / 30f;
        actorNameText.text = actorName;
        comedyVal.text = ((int)comedy).ToString();
        romanceVal.text = ((int)romance).ToString();
        horrorVal.text = ((int)horror).ToString();
        scifiVal.text = ((int)scifi).ToString();
        otherVal.text = ((int)other).ToString();
        actionVal.text = ((int)action).ToString();
        actorPictureSprite.sprite = actorPicture;
        string[] name = actorName.Split(' ');
        availabilityText.text = actor.getState().ToString();
        actorLevel.text = "Lv. " + actor.getLevel().ToString();
        if(actor.getState() == Actor.ActorState.available)
        {
            availabilityBubble.color = Color.green;
        }
        else
        {
            availabilityBubble.color = Color.red;
        }
    }
}
