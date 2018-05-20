using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorSelection : MonoBehaviour {

    Actor actor;

    Player p;

    private void Start()
    {
        p = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(actor != null)
        {
            Image [] imgs = GetComponentsInChildren<Image>();
            imgs[1].sprite = actor.getPicture();
            GetComponentInChildren<Text>().text = actor.getName();
        }
    }

    public void setActor(Actor act)
    {
        actor = act;
    }

    public void onPress()
    {
        p.setActor(actor, 0);
    }
}
