using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeManager : Singleton<IncomeManager> {

    public int incomeIntervalInMins;

    private System.TimeSpan incomeinterval;

    private System.DateTime prevDateTime;

	// Use this for initialization
	protected override void Awake () {
        base.Awake();
	}

    private void Start()
    {
        incomeinterval = new System.TimeSpan(0, incomeIntervalInMins, 0);
        prevDateTime = SaveLoad.Instance.getPrevIncomeTime();
    }

    // Update is called once per frame
    void Update () {
        System.DateTime currentTime = System.DateTime.Now;

        while(currentTime > prevDateTime)
        {
            prevDateTime = prevDateTime.Add(incomeinterval);
            int income = 0;
            List<Actor> aList = Player.Instance.getActorsList();

            foreach(Actor a in aList)
            {
                income += a.getIncomeValue();
            }

            Player.Instance.addMoney(income);
        }
	}

    public System.DateTime getPrevIncomeTime()
    {
        return prevDateTime;
    }

    public System.TimeSpan getIncomeInterval()
    {
        return incomeinterval;
    }
}
