using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ActorRequest {

    public Actor[] clients;

	// Use this for initialization
	void Start () {
        clients = new Actor[20];  
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
