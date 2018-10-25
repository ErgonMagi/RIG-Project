using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : Singleton<Player> {

    public List<Actor> clients;
    public TextMeshProUGUI currencyText;
    private int money;
    private float reputation, maxReputation;
    private int level;
    public delegate void UpdateAction();
    public static event UpdateAction actorUpdate;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
        clients = SaveLoad.Instance.getActors();
        money = SaveLoad.Instance.getMoney();
        reputation = 0;   //Update to load rep
        maxReputation = 1000f;
	}

    private void Start()
    {
        CurrencyManager.Instance.setStartCurrency(money);
        ReputationManager.Instance.setStartReputation(reputation, maxReputation);
    }

    public int getLevel()
    {
        return level;
    }

    public void SetLevel(int lvl)
    {
        level = lvl;
    }

    public Actor getActor(int actorNum)
    {
        if(actorNum >= clients.Count)
        {
            return null;
        }
        return clients[actorNum];
    }

    public void addMoney(int addedMoney)
    {
        money += addedMoney;
        CurrencyManager.Instance.UpdateCurrency();
    }

    public bool spendMoney(int spentMoney)
    {
        if(money >= spentMoney)
        {
            money -= spentMoney;
            CurrencyManager.Instance.UpdateCurrency();
            return true;
        }
        else
        {
            return false;
        }
    }

    public int getMoney()
    {
        return money;
    }

    public void AddReputation(float rep)
    {
        reputation += rep;
        ReputationManager.Instance.UpdateReputation();
    }

    public float GetReputation()
    {
        return reputation;
    }

    public void setActor(Actor actor, int arrayNum)
    {
        clients.Insert(arrayNum, actor);
        actorUpdate();
    }

    public void addActor(Actor actor)
    {
        clients.Add(actor);
        actorUpdate();
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
        actorUpdate();
    }
}
