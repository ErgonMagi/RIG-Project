using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorsNamesList {

    private Dictionary<int, string> actorInfo;
    private List<int> idsList;

    public string [] actorIds =
    {
        "227454,Alicia Vikander",
        "1245,Scarlett Johansson",
        "73968,Henry Cavill",
        "8784,Daniel Craig",
        "1230,John Goodman",
        "10730,Rowan Atkinson",
        "91606,Tom Hiddleston",
        "71580,Benedict Cumberbatch",
        "6968,Hugh Jackman",
        "1136406,Tom Holland",
        "6193,Leonardo DiCaprio",
        "21007,Jonah Hill",
        "62861,Andy Samberg",
        "776,Eddie Murphy",
        "5064,Meryl Streep",
        "517,Pierce Brosnan",
        "53256,Terry Crews",
        "29222,Zac Efron",
        "74568,Chris Hemsworth",
        "10859,Ryan Reynolds",
        "514,Jack Nicholson",
        "8638,Eddie Redmayne"
    };

    public void init()
    {
        actorInfo = new Dictionary<int, string>();
        idsList = new List<int>();
        foreach(string s in actorIds)
        {
            string[] splitString = s.Split(',');
            actorInfo.Add(int.Parse(splitString[0]), splitString[1]);
            idsList.Add(int.Parse(splitString[0]));
        }
    }

	public string getName(int actorId)
    {
        return actorInfo[actorId];
    }

    public int getId()
    {
        int actorNum = (int)UnityEngine.Random.Range(0, actorInfo.Count);
        return idsList[actorNum];
    }
}
