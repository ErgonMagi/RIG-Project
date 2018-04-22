using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Actor[] clients;
    private ActorManager actorManager;

	// Use this for initialization
	void Start () {
        clients = new Actor[5];
        actorManager = FindObjectOfType<ActorManager>();       
	}

    public void init()
    {
        for (int i = 0; i < 5; i++)
        {
            actorManager.generateActor(this, i);
        }
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
