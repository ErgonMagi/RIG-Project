using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ActorRequest {

    private Actor[] clients;
    private ActorManager actorManager;

	// Use this for initialization
	void Start () {
        clients = new Actor[20];
        actorManager = FindObjectOfType<ActorManager>();       
	}

    public Actor getActor(int actorNum)
    {
        return clients[actorNum];
    }

    public void setActor(Actor actor, int arrayNum)
    {
        clients[arrayNum] = actor;
    }
}
