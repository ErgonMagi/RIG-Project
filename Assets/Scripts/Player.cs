using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

    public Actor [] clients;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        clients = SaveLoad.Instance.getActors();
	}

    public Actor getActor(int actorNum)
    {
        return clients[actorNum];
    }

    public void setActor(Actor actor, int arrayNum)
    {
        clients[arrayNum] = actor;
    }

    public void addActor(Actor actor)
    {
        for(int i = 0; i < clients.Length; i++)
        {
            if(clients[i] == null)
            {
                clients[i] = actor;
                return;
            }
        }
    }

    public Actor[] getActorsList()
    {
        return clients;
    }

    public void setActors(Actor [] actors)
    {
        clients = actors;
    }
}
