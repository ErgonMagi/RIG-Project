using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActorIconIpad : MonoBehaviour, IPointerClickHandler
{

    public Image actorImage;
    public Image availabilityBubble;
    public Image actorBorder;
    public ActorStatsMenu actorStatsMenu;

    private Actor actor;

	public void SetActor(Actor actor)
    {
        this.actor = actor;
        actorImage.sprite = actor.getPicture();
        if(actor.getState() == Actor.ActorState.available)
        {
            availabilityBubble.color = Color.green;
        }
        else
        {
            availabilityBubble.color = Color.red;
        }
    }

    public Actor GetActor()
    {
        return actor;
    }

    public void OnPointerClick(PointerEventData pointer)
    {
        actorStatsMenu.SelectActor(this);
    }

    public void ToggleBorder(bool borderOn)
    {
        if(borderOn)
        {
            actorBorder.color = Color.yellow;
        }
        else
        {
            actorBorder.color = Color.white;
        }
    }
}
