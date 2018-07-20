using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ActorRequest {

    public Actor [] clients;

	// Use this for initialization
	void Start () {
        clients = FindObjectOfType<SaveLoad>().getActors();
	}

    public Actor getActor(int actorNum)
    {
        return clients[actorNum];
    }

    public void setActor(Actor actor, int arrayNum)
    {
        clients[arrayNum] = actor;
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
