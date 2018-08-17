using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player> {

    public List<Actor> clients;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        clients = SaveLoad.Instance.getActors();
	}

    public Actor getActor(int actorNum)
    {
        if(actorNum >= clients.Count)
        {
            return null;
        }
        return clients[actorNum];
    }

    public void setActor(Actor actor, int arrayNum)
    {
        clients.Insert(arrayNum, actor);
    }

    public void addActor(Actor actor)
    {
        clients.Add(actor);
    }

    public List<Actor> getActorsList()
    {
        return clients;
    }

    public List<Actor> getAvialableActors()
    {
        List<Actor> availableActors = new List<Actor>();

        for(int i = 0; i < clients.Count; i++)
        {
            if(clients[i].getState() == Actor.ActorState.available)
            {
                availableActors.Add(clients[i]);
            }
        }

        return availableActors;
    }

    public void setActors(List<Actor> actors)
    {
        clients = actors;
    }
}
